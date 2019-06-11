using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

    public Vector2 maxPosition;
    public Vector2 minPosition;
    private float smooth;
    public Transform target;

    private void Start()
    {
        smooth = 0.5f;
    }

    private void LateUpdate()
    {
        if (target != null && transform.position != target.position) {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smooth);
        }
    }
}
