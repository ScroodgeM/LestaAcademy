using System;
using UnityEngine;

namespace LestaAcademyDemo.ThreadsDemo.Scripts
{
    public class Link : MonoBehaviour
    {
        public event Action<(int, int)> OnCut = ii => { };

        private Joint joint1;
        private Joint joint2;

        public (int, int) Joints => (joint1.Index, joint2.Index);

        public void Init(Joint joint1, Joint joint2)
        {
            this.joint1 = joint1;
            this.joint2 = joint2;
        }

        private void LateUpdate()
        {
            transform.position = (joint1.Position + joint2.Position) * 0.5f;
            transform.rotation = Quaternion.LookRotation(joint2.Position - joint1.Position);
            transform.localScale = new Vector3(1f, 1f, (joint1.Position - joint2.Position).magnitude);
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButton(0) == true)
            {
                OnCut(Joints);
            }
        }
    }
}
