
using System.Collections.Generic;
using UnityEngine;

namespace WGADemo.ThreadsDemo.Scripts
{
    public class ForceCalculator
    {
        public struct Input
        {
            public List<Vector3> positions;
            public List<(int, int)> links;
        }

        public struct Output
        {
            public List<Vector3> preferredPositions;
        }

        private readonly float linkTarget;
        private readonly int linkWeight;
        private readonly float repulsionTarget;

        private Input input;
        private Output output;

        public void SetInput(Input input) => this.input = input;
        public Output GetOutput() => output;

        public ForceCalculator(float linkTarget, int linkWeight, float repulsionTarget)
        {
            this.linkTarget = linkTarget;
            this.linkWeight = linkWeight;
            this.repulsionTarget = repulsionTarget;
        }

        public void Calculate()
        {
            if (output.preferredPositions == null)
            {
                output.preferredPositions = new List<Vector3>();
            }

            output.preferredPositions.Clear();

            for (int i = 0; i < input.positions.Count; i++)
            {
                output.preferredPositions.Add(GetRepulsionForce(i));
            }
        }

        private Vector3 GetRepulsionForce(int index)
        {
            Vector3 myPosition = input.positions[index];

            Vector3 preferredPosition = Vector3.zero;
            int preferredPositionDivider = 0;

            for (int i = 0; i < input.positions.Count; i++)
            {
                if (i == index)
                {
                    continue;
                }

                bool linkExists = false;

                foreach ((int, int) link in input.links)
                {
                    if (
                        (link.Item1 == index && link.Item2 == i)
                        ||
                        (link.Item2 == index && link.Item1 == i)
                        )
                    {
                        preferredPosition += GetPreferredPositionFromLink(myPosition, input.positions[i], linkTarget) * linkWeight;
                        preferredPositionDivider += linkWeight;
                        linkExists = true;
                    }
                }

                if (linkExists == false && TryGetPreferredPositionFromRepulsion(myPosition, input.positions[i], repulsionTarget, out Vector3 _preferredPosition))
                {
                    preferredPosition += _preferredPosition;
                    preferredPositionDivider++;
                }
            }

            if (preferredPositionDivider == 0)
            {
                return myPosition;
            }

            return preferredPosition / (float)preferredPositionDivider;
        }

        private static Vector3 GetPreferredPositionFromLink(Vector3 me, Vector3 other, float targetDistance)
        {
            Vector3 offsetToOther = other - me;

            float distance = offsetToOther.magnitude;

            return other - offsetToOther * (targetDistance / distance);
        }

        private static bool TryGetPreferredPositionFromRepulsion(Vector3 me, Vector3 other, float targetDistance, out Vector3 preferredPosition)
        {
            preferredPosition = me;

            Vector3 offsetToOther = other - me;

            float distance = offsetToOther.magnitude;

            if (distance > targetDistance) { return false; }

            preferredPosition = other - offsetToOther * (targetDistance / distance);
            return true;
        }
    }
}
