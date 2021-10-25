using System;
using UnityEngine;
using UnityEngine.Assertions;

public class SliderCollider : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private GameManager gameManager;
    
    private bool perdio, pausa;
    private const float xSpeed = 350f, ySpeed = 150f;
    private const float maxXSpeed = 50f, maxYSpeed = 6.5f;

    public enum Orientacion
    {
        Derecha,
        Izquierda,
        Arriba,
        Abajo
    };
    
    public Orientacion oritorientacionSlider = Orientacion.Derecha;

    void Awake()
    {
        playerRigidBody = GameObject.Find("Jugador").GetComponent<Rigidbody2D>();
        Assert.IsNotNull(playerRigidBody);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Jugador") || perdio || pausa)
            return;

        float yForce = 0;
        float xForce = 0;

        switch (oritorientacionSlider)
        {
            case Orientacion.Derecha:
                xForce = 1;
                break;
            case Orientacion.Izquierda:
                xForce = -1;
                break;
            case Orientacion.Arriba:
                yForce = 1;
                break;
            case Orientacion.Abajo:
                yForce = -1;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (Mathf.Abs(playerRigidBody.velocity.y) > maxYSpeed)
        {
            yForce = 0;
        }

        playerRigidBody.AddForce(new Vector2(xForce * xSpeed,yForce * ySpeed));
    }
    
    private void Start()
    {
        gameManager = GameManager.instance;
        Assert.IsNotNull(gameManager);
        
        gameManager.pausarJuegoEvent += onPausarJuego;
        gameManager.perderJuegoEvent += onPerderJuego;
    }
    
    private void OnDestroy()
    {
        gameManager.pausarJuegoEvent -= onPausarJuego;
        gameManager.perderJuegoEvent -= onPerderJuego;
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
