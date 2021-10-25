using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    public float smoothTime = 0.3f;
    readonly Vector3 offset = new Vector3(0,0,-10);
    Vector3 velocity = new Vector3(0,0,0);

    private bool perdio, pausa;
    
    private void Start()
    {
        GameManager.pausarJuegoEvent += onPausarJuego;
        GameManager.perderJuegoEvent += onPerderJuego;
    }
    
    private void OnDestroy()
    {
        GameManager.perderJuegoEvent -= onPerderJuego;
        GameManager.pausarJuegoEvent -= onPausarJuego;
    }

    private void onPerderJuego()
    {
        perdio = true;
    }
    
    private void onPausarJuego()
    {
        pausa = !pausa;
    }

    /* -------------------------------------------------------------------------------- */
    
    void Awake()
    {
        target = GameObject.Find("Jugador").GetComponent<Transform>();
    }

    /* -------------------------------------------------------------------------------- */
    
    void LateUpdate()
    {
        if (perdio || pausa)
            return;
        
        Vector3 targetPosition = target.TransformPoint(offset);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
