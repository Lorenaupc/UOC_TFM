using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour {

    public Transform[] points;
    internal int currentPoint;
    public float moveSpeed;

    void Start()
    {
        transform.position = points[0].position;
        currentPoint = 0;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.5f)
        {
            currentPoint++;
        }

        if (currentPoint >= points.Length)
        {
            currentPoint = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);
    }
}
