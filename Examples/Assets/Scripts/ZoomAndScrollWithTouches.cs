using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndScrollWithTouches : MonoBehaviour
{
    void Update()
    {
        //zoom
        if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(1);
            Touch t1 = Input.GetTouch(0);

            Vector2 t0posOld = t0.position - t0.deltaPosition;
            Vector2 t1posOld = t1.position - t1.deltaPosition;

            Vector2 t0posNew = t0.position;
            Vector2 t1posNew = t1.position;

            float distanceOld = (t1posOld - t0posOld).magnitude;
            float distanceNew = (t1posNew - t0posNew).magnitude;
            float zoomMultiplier = distanceNew / distanceOld;

            //mode 1
            GetComponent<Camera>().orthographicSize /= zoomMultiplier;

            //mode 2
            GetComponent<Camera>().fieldOfView /= zoomMultiplier;

            //mode 3
            transform.localScale *= zoomMultiplier;
        }

        //scroll
        Vector2 averageTouchDelta = Vector2.zero;
        foreach (Touch touch in Input.touches)
        {
            averageTouchDelta += touch.deltaPosition;
        }
        averageTouchDelta /= Input.touchCount;

        if (averageTouchDelta != Vector2.zero)
        {
            //mode 1 - move in XY space
            transform.position += (Vector3)averageTouchDelta;

            //mode 2 - rotate
            transform.rotation *= Quaternion.Euler(averageTouchDelta.y, -averageTouchDelta.x, 0f);
        }
    }
}
