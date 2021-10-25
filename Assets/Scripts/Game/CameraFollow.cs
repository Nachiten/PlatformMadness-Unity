using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour
{
    public float smoothTime = 0.3f;
    
    private Transform target;
    
    private readonly Vector3 offset = new Vector3(0,0,-10);
    private Vector3 velocity = new Vector3(0,0,0);

    private bool perdio, pausa;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        Assert.IsNotNull(gameManager);
        
        gameManager.pausarJuegoEvent += onPausarJuego;
        gameManager.perderJuegoEvent += onPerderJuego;
    }
    
    private void OnDestroy()
    {
        gameManager.perderJuegoEvent -= onPerderJuego;
        gameManager.pausarJuegoEvent -= onPausarJuego;
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
        Assert.IsNotNull(target);
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
