using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    float smoothTime = 0.6f;
    Vector3 offset;
    Vector3 velocity = new Vector3(5,5,5);
    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.Find("Jugador").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0,0,-10));

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
