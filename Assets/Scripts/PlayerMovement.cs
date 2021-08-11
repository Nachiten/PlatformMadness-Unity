using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedX = 6.0f;
    public float speedY = 350.0f;
    public float gravity = 4f;
    public LayerMask layer;

    float offsetX = 0.55f;
    float offsetY = 0.09f;

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
        Vector3 pointA = new Vector3(transform.position.x + offsetX, transform.position.y - transform.localScale.y / 2);
        Vector3 pointB = new Vector3(transform.position.x - offsetX, transform.position.y - transform.localScale.y / 2 - offsetY);

        Collider2D checkeoDePiso = Physics2D.OverlapArea(pointA, pointB, layer);

        if (checkeoDePiso != null) {
            Debug.DrawLine(pointA, new Vector2(pointB.x, pointA.y), Color.green, 5);
            Debug.DrawLine(pointA, new Vector2(pointA.x, pointB.y), Color.green, 5);
            Debug.DrawLine(new Vector2(pointB.x, pointA.y), pointB, Color.green, 5);
            Debug.DrawLine(new Vector2(pointA.x, pointB.y), pointB, Color.green, 5);
            return true;
        }
        
        return false;
    }
}

