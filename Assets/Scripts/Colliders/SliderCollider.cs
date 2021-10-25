using UnityEngine;
using UnityEngine.Assertions;

public class SliderCollider : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;

    private bool perdio, pausa;
    
    void Awake()
    {
        playerRigidBody = GameObject.Find("Jugador").GetComponent<Rigidbody2D>();

        Assert.IsNotNull(playerRigidBody);
    }

    public float speed = 10;
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Jugador") || perdio || pausa)
            return;
        
        playerRigidBody.AddForce(new Vector2(speed,0));
    }
    
    private void Start()
    {
        GameManager.pausarJuegoEvent += onPausarJuego;
        GameManager.perderJuegoEvent += onPerderJuego;
    }
    
    private void OnDestroy()
    {
        GameManager.pausarJuegoEvent -= onPausarJuego;
        GameManager.perderJuegoEvent -= onPerderJuego;
    }

    private void onPerderJuego()
    {
        perdio = true;
    }
    
    private void onPausarJuego() 
    {
        pausa = !pausa;
    }
}
