using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedX = 6.0f;
    public float speedY = 350.0f;
    public float gravity = 4;

    bool tocandoPiso = false;

    private Rigidbody2D rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.gravityScale = gravity;

        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, rigidBody.velocity.y);

        if (tocandoPiso && Input.GetKey(KeyCode.Space))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speedY);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Vector3 normal = collision.GetContact(0).normal;
        bool tocaElPisoDeArriba = normal == Vector3.up;

        // Solo estoy tocando el piso si el objeto es mapa, y si es desde arriba
        if (tocaElPisoDeArriba && collision.gameObject.CompareTag("Mapa"))
        {
            tocandoPiso = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mapa"))
            tocandoPiso = false;
    }
}

