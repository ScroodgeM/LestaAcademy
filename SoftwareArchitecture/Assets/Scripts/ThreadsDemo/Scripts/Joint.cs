
using System;
using UnityEngine;

namespace WGADemo.ThreadsDemo.Scripts
{
    public class Joint : MonoBehaviour
    {
        public event Action<int> OnClick = i => { };

        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material selectedMaterial;
        [SerializeField] private Renderer renderer;

        private int index;
        private Vector3 position;
        private Vector3 preferredPosition;

        public int Index => index;
        public Vector3 Position => position;

        private void Awake()
        {
            // random position on start
            position = UnityEngine.Random.insideUnitSphere * 20f;
        }

        internal void Init(int index)
        {
            this.index = index;
        }

        internal void SetPreferredPosition(Vector3 preferredPosition)
        {
            this.preferredPosition = preferredPosition;
        }

        private void Update()
        {
            position = Vector3.Lerp(position, preferredPosition, Time.deltaTime * 3f);
        }

        private void LateUpdate()
        {
            transform.position = position;
        }

        private void OnMouseDown()
        {
            OnClick(index);
        }

        internal void SetSelected(bool selected)
        {
            renderer.sharedMaterial = selected ? selectedMaterial : normalMaterial;
        }
    }
}
