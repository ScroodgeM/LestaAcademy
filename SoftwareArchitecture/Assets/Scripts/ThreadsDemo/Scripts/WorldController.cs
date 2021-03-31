
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace WGADemo.ThreadsDemo.Scripts
{
    public class WorldController : MonoBehaviour
    {
        [SerializeField] private Text statisticsText;
        [SerializeField] private int calculationsPerSecond;
        [SerializeField] private Joint jointPrefab;
        [SerializeField] private float repulsionTarget;
        [SerializeField] private Link linkPrefab;
        [SerializeField] private float linkTarget;
        [SerializeField] private int linkWeight;
        [SerializeField] private int initialJointsCount;
        [SerializeField] private bool useThread;

        private readonly List<Joint> joints = new List<Joint>();
        private readonly List<Link> links = new List<Link>();

        private ForceCalculator forceCalculator;
        private readonly ForceCalculator.Input inputCache = new ForceCalculator.Input()
        {
            positions = new List<Vector3>(),
            links = new List<(int, int)>(),
        };
        private bool calculationInProgress = false;
        private uint totalCalculationsCount;

        private long lastCalculationTick;
        private int? selectedJointIndex;

        private Thread thread;
        private readonly object lockObject = new object();
        private bool readyForCalculation = false;
        private bool readyForClaimResults = false;

        private void Awake()
        {
            forceCalculator = new ForceCalculator(linkTarget, linkWeight, repulsionTarget);

            for (int i = 0; i < initialJointsCount; i++)
            {
                Joint joint = Instantiate(jointPrefab);
                joint.Init(i);
                joint.OnClick += Joint_OnClick;
                joints.Add(joint);
            }

            for (int i = 1; i < initialJointsCount; i++)
            {
                int other = UnityEngine.Random.Range(0, i);
                CreateLink(i, other);
            }

            thread = new Thread(ThreadWork);
            thread.Start();
        }

        private void OnDestroy()
        {
            thread.Abort();
        }

        private void Joint_OnClick(int jointIndex)
        {
            if (selectedJointIndex.HasValue == false)
            {
                joints[jointIndex].SetSelected(true);
                selectedJointIndex = jointIndex;
            }

            if (selectedJointIndex.HasValue == true && selectedJointIndex.Value != jointIndex)
            {
                CreateLink(selectedJointIndex.Value, jointIndex);

                joints[selectedJointIndex.Value].SetSelected(false);
                selectedJointIndex = null;
            }
        }

        private void CreateLink(int joint1, int joint2)
        {
            Link link = Instantiate(linkPrefab);
            link.Init(joints[joint1], joints[joint2]);
            link.OnCut += Link_OnClick;
            links.Add(link);

            void Link_OnClick((int, int) joints)
            {
                links.Remove(link);
                Destroy(link.gameObject);
            }
        }

        private void Update()
        {
            if (calculationInProgress == false)
            {
                long nowTick = (long)(Time.timeAsDouble * calculationsPerSecond);

                if (nowTick > lastCalculationTick)
                {
                    calculationInProgress = true;

                    lastCalculationTick = nowTick;

                    PrepareCalculationInput();

                    if (useThread == true)
                    {
                        lock (lockObject)
                        {
                            readyForCalculation = true;
                        }
                    }
                    else
                    {
                        forceCalculator.Calculate();
                        this.readyForClaimResults = true;
                    }
                }
            }

            bool readyForClaimResults;

            lock (lockObject)
            {
                readyForClaimResults = this.readyForClaimResults;
            }

            if (readyForClaimResults)
            {
                ClaimResults();

                lock (lockObject)
                {
                    this.readyForClaimResults = false;
                }

                calculationInProgress = false;
                totalCalculationsCount++;
            }

            statisticsText.text = $"FPS: {Time.frameCount / Time.realtimeSinceStartup:0.0} // CalcPS: {totalCalculationsCount / Time.realtimeSinceStartup: 0.0}";
        }

        private void PrepareCalculationInput()
        {
            inputCache.positions.Clear();

            foreach (Joint joint in joints)
            {
                inputCache.positions.Add(joint.Position);
            }

            inputCache.links.Clear();

            foreach (Link link in links)
            {
                inputCache.links.Add(link.Joints);
            }

            forceCalculator.SetInput(inputCache);
        }

        private void ClaimResults()
        {
            List<Vector3> preferredPositions = forceCalculator.GetOutput().preferredPositions;

            for (int i = 0; i < joints.Count; i++)
            {
                joints[i].SetPreferredPosition(preferredPositions[i]);
            }
        }

        private void ThreadWork()
        {
            while (true)
            {
                Thread.Sleep(10);

                bool readyForCalculation;

                lock (lockObject)
                {
                    readyForCalculation = this.readyForCalculation;
                }

                if (readyForCalculation)
                {
                    forceCalculator.Calculate();

                    lock (lockObject)
                    {
                        this.readyForCalculation = false;
                        this.readyForClaimResults = true;
                    }
                }
            }
        }
    }
}
