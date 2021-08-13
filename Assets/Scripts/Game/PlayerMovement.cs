using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedX = 8f;
    public float speedY = 14f;
    
    float offsetX = 0.55f;
    float offsetY = 0.09f;

    Rigidbody2D rigidBody;

    LayerMask layer;

    Vector2 ultimaVelocidad = Vector2.zero;
    public float gravity = 2.7f;

    bool pausa = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = gravity;
        layer = LayerMask.GetMask("Mapa");
    }

    void FixedUpdate()
    {
        if (pausa)
            return;

        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, rigidBody.velocity.y);

        bool tocoTeclaDeSalto = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);

        if (tocoTeclaDeSalto && colisionaConPiso())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speedY);
        }
    }

    private bool colisionaConPiso() 
    {
        Vector3 pointA = new Vector3(transform.position.x + offsetX, transform.position.y - transform.localScale.y / 2);
        Vector3 pointB = new Vector3(transform.position.x - offsetX, transform.position.y - transform.localScale.y / 2 - offsetY);

        Collider2D checkeoDePiso = Physics2D.OverlapArea(pointA, pointB, layer);

        if (checkeoDePiso != null) 
        {
            Debug.DrawLine(pointA, new Vector2(pointB.x, pointA.y), Color.green, 5);
            Debug.DrawLine(pointA, new Vector2(pointA.x, pointB.y), Color.green, 5);
            Debug.DrawLine(new Vector2(pointB.x, pointA.y), pointB, Color.green, 5);
            Debug.DrawLine(new Vector2(pointA.x, pointB.y), pointB, Color.green, 5);
            return true;
        }

        if (tocandoParedStick && !saltoEnActual)
        {
            rigidBody.gravityScale = gravity;
            saltoEnActual = true;
            return true;
        }

        return false;
    }

    /* -------------------------------------------------------------------------------- */
    
    public void manejarPausa() {
        pausa = !pausa;

        if (pausa)
        {
            // Entro en pausa y guardo estado anterior
            ultimaVelocidad = rigidBody.velocity;
            rigidBody.velocity = Vector2.zero;
            rigidBody.gravityScale = 0;
        }
        else 
        {
            // Salgo de pausa y restauro estado anterior
            rigidBody.velocity = ultimaVelocidad;
            rigidBody.gravityScale = gravity;
        }
    }

    /* -------------------------------------------------------------------------------- */

    bool tocandoParedStick = false;
    bool saltoEnActual = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WallStick") && !tocandoParedStick)
        {
            Debug.Log("Toco wallStick");
            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.zero;
            tocandoParedStick = true;
            saltoEnActual = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WallStick"))
        {
            tocandoParedStick = false;
            rigidBody.gravityScale = gravity;
        }
    }
}

