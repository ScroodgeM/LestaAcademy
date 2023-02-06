//this empty line for UTF-8 BOM header
using UnityEngine;

namespace LestaAcademyDemo.ThreadsDemo.Scripts
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private Vector3 focusPoint;
        [SerializeField] private float scrollSensitivity;
        [SerializeField] private float zoomSensitivity;

        private float distance = 40f;
        private float angleX = 0f;
        private float angleY = 0f;

        private Vector3 lastKnownMousePosition;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1) == true)
            {
                lastKnownMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(1) == true)
            {
                Vector3 mouseDelta = Input.mousePosition - lastKnownMousePosition;
                lastKnownMousePosition = Input.mousePosition;

                angleX = Mathf.Clamp(angleX - scrollSensitivity * mouseDelta.y, -90f, 90f);
                angleY += scrollSensitivity * mouseDelta.x;
            }

            distance *= Mathf.Pow(1f + zoomSensitivity, -Input.mouseScrollDelta.y);
        }

        private void LateUpdate()
        {
            Quaternion rotation = Quaternion.Euler(angleX, angleY, 0f);

            transform.position = focusPoint + rotation * Vector3.back * distance;
            transform.rotation = rotation;
        }
    }
}
