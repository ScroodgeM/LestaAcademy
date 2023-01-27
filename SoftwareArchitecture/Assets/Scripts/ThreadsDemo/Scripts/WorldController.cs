
//#define USE_THREAD

#if USE_THREAD
using System.Threading;
#endif

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LestaAcademyDemo.ThreadsDemo.Scripts
{
    public class WorldController : MonoBehaviour
    {
#if USE_THREAD
        private enum State { Idle, ReadyForCalculation, CalculationDone }
#endif

        [SerializeField] private Text statisticsText;
        [SerializeField] private Joint jointPrefab;
        [SerializeField] private float repulsionTarget;
        [SerializeField] private Link linkPrefab;
        [SerializeField] private float linkTarget;
        [SerializeField] private int linkWeight;
        [SerializeField] private int initialJointsCount;

        private readonly List<Joint> joints = new List<Joint>();
        private readonly List<Link> links = new List<Link>();

        private ForceCalculator forceCalculator;
        private readonly ForceCalculator.Input inputCache = new ForceCalculator.Input()
        {
            positions = new List<Vector3>(),
            links = new List<(int, int)>(),
        };

        private long nextPrintStatisticTime;
        private double threeFramesAgoTime;
        private double threeCalculationsAgoTime;

        private int? selectedJointIndex;

#if USE_THREAD
        private Thread thread;
        private readonly object lockObject = new object();
        private State state = State.Idle;
#endif

        private void Awake()
        {
            Application.targetFrameRate = 60;

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

#if USE_THREAD
            thread = new Thread(ThreadWork);
            thread.Start();
#endif
        }

        private void OnDestroy()
        {
#if USE_THREAD
            thread.Abort();
#endif
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
            long time = (long)Time.timeAsDouble;
            if (time >= nextPrintStatisticTime)
            {
                statisticsText.text = $"FPS: {3 / (Time.timeAsDouble - threeFramesAgoTime):0.0} // CalcPS: {3 / (Time.timeAsDouble - threeCalculationsAgoTime): 0.0}";
                nextPrintStatisticTime = time + 1;
            }

#if USE_THREAD
            State state;

            lock (lockObject)
            {
                state = this.state;
            }

            if (state == State.CalculationDone || state == State.Idle)
            {
                if (state == State.CalculationDone)
                {
                    ClaimResults();
                    threeCalculationsAgoTime += (Time.timeAsDouble - threeCalculationsAgoTime) * 0.333;
                }

                PrepareCalculationInput();

                lock (lockObject)
                {
                    this.state = State.ReadyForCalculation;
                }
            }
#else
            PrepareCalculationInput();
            forceCalculator.Calculate();
            ClaimResults();
            threeCalculationsAgoTime += (Time.timeAsDouble - threeCalculationsAgoTime) * 0.333;
#endif

            threeFramesAgoTime += (Time.timeAsDouble - threeFramesAgoTime) * 0.333;
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

#if USE_THREAD
        private void ThreadWork()
        {
            while (true)
            {
                Thread.Sleep(10);

                State state;

                lock (lockObject)
                {
                    state = this.state;
                }

                if (state == State.ReadyForCalculation)
                {
                    forceCalculator.Calculate();

                    lock (lockObject)
                    {
                        this.state = State.CalculationDone;
                    }
                }
            }
        }
#endif
    }
}
