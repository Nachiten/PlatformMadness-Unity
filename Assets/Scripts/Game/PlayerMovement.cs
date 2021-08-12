using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedX = 8f;
    public float speedY = 14f;
    public float gravity = 2.7f;

    float offsetX = 0.55f;
    float offsetY = 0.09f;

    LayerMask layer;

    Rigidbody2D rigidBody;

    Vector3 ultimaVelocidad = Vector3.zero;

    bool pausa = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        layer = LayerMask.GetMask("Mapa");
    }

    void FixedUpdate()
    {
        if (pausa)
            return;

        rigidBody.gravityScale = gravity;

        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, rigidBody.velocity.y);

        if (Input.GetKey(KeyCode.Space) && colisionaConPiso())
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
        
        return false;
    }

    

    public void manejarPausa() {
        pausa = !pausa;

        if (pausa)
        {
            // Entro en pausa y guardo estado anterior
            ultimaVelocidad = rigidBody.velocity;
            rigidBody.velocity = Vector3.zero;
            rigidBody.gravityScale = 0;
        }
        else 
        {
            // Salgo de pausa y restauro estado anterior
            rigidBody.velocity = ultimaVelocidad;
            rigidBody.gravityScale = gravity;
        }
    }
}

