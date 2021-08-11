using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedX = 6.0f;
    public float speedY = 350.0f;
    public float gravity = 4f;

    private Rigidbody2D rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidBody.gravityScale = gravity;

        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, rigidBody.velocity.y);

        if (Input.GetKey(KeyCode.Space) && colisionaConPiso())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speedY);
        }
    }

    private bool colisionaConPiso() {

        float offsetX = 0.26f;
        float offsetY = 0.25f;

        Vector2 origenRayoIzquierda = new Vector3(transform.position.x - transform.localScale.x / 2 + offsetX, transform.position.y);
        Vector2 origenRayoDerecha = new Vector3(transform.position.x + transform.localScale.x / 2 - offsetX, transform.position.y);
        Vector2 direccionRayo = transform.TransformDirection(Vector2.down);
        float rayDistance = transform.localScale.y / 2 + offsetY;

        // Create bitmask
        int layer = 1 << 6;

        // This would cast rays only against colliders in layer 6.
        // But instead I want collide against everything except layer 6. The ~ operator does this, it inverts a bitmask.
        // (layer 6 is the player)
        layer = ~layer;

        RaycastHit2D hitIzquierda = Physics2D.Raycast(origenRayoIzquierda, direccionRayo, rayDistance, layer);
        RaycastHit2D hitDerecha = Physics2D.Raycast(origenRayoDerecha, direccionRayo, rayDistance, layer);

        bool tocoRayoIzquierda = hitIzquierda.collider != null && hitIzquierda.collider.gameObject.CompareTag("Mapa");
        bool tocoRayoDerecha = hitDerecha.collider != null && hitDerecha.collider.gameObject.CompareTag("Mapa");

        if (tocoRayoIzquierda)
            Debug.DrawRay(origenRayoIzquierda, direccionRayo * rayDistance, Color.green, 3);
        else
            Debug.DrawRay(origenRayoIzquierda, direccionRayo * rayDistance, Color.red, 3);

        if (tocoRayoDerecha)
            Debug.DrawRay(origenRayoDerecha, direccionRayo * rayDistance, Color.green, 3);
        else
            Debug.DrawRay(origenRayoDerecha, direccionRayo * rayDistance, Color.red, 3);
        

        return tocoRayoIzquierda || tocoRayoDerecha;
    }
}

