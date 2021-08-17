using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    public float smoothTime = 0.3f;
    readonly Vector3 offset = new Vector3(0,0,-10);
    Vector3 velocity = new Vector3(0,0,0);

    /* -------------------------------------------------------------------------------- */
    
    void Awake()
    {
        target = GameObject.Find("Jugador").GetComponent<Transform>();
    }

    /* -------------------------------------------------------------------------------- */
    
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(offset);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
