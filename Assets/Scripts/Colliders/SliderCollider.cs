using System;
using UnityEngine;
using UnityEngine.Assertions;

public class SliderCollider : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private GameManager gameManager;
    
    private bool perdio, pausa;
    private const float xSpeed = 350f, ySpeed = 150f, gravity = 2.7f;
    private const float maxXSpeed = 50f, maxYSpeed = 6.5f;
    
    private static int amountOfTriggers;
    
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
        amountOfTriggers = 0;
        
        playerRigidBody = GameObject.Find("Jugador").GetComponent<Rigidbody2D>();
        Assert.IsNotNull(playerRigidBody);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        amountOfTriggers++;
        playerRigidBody.gravityScale = 0;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        amountOfTriggers--;
        if (amountOfTriggers <= 0)
            playerRigidBody.gravityScale = gravity;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Jugador") || perdio || pausa)
            return;

        int yForce = 0, xForce = 0;

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

        if (yForce == 1 && playerRigidBody.velocity.y > maxYSpeed || yForce == -1 && playerRigidBody.velocity.y < -maxYSpeed)
        {
            yForce = 0;
        }

        playerRigidBody.AddForce(new Vector2(xForce * xSpeed,yForce * ySpeed));
    }
    
    private void Start()
    {
        gameManager = GameManager.instance;
        Assert.IsNotNull(gameManager);
        
        gameManager.pauseGameEvent += onPauseGame;
        gameManager.lostGameEvent += onLostGame;
    }
    
    private void OnDestroy()
    {
        gameManager.pauseGameEvent -= onPauseGame;
        gameManager.lostGameEvent -= onLostGame;
    }

    private void onLostGame()
    {
        perdio = true;
        playerRigidBody.gravityScale = 0;
        playerRigidBody.velocity = Vector2.zero;
    }
    
    private void onPauseGame() 
    {
        pausa = !pausa;
    }
}
