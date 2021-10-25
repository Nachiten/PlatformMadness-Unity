using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const float speedX = 8f, speedY = 14f, gravity = 2.7f, offsetXCollider = 0.55f, offsetYCollider = 0.09f, 
        tiempoResistenciaStick = 0.85f, speedYStick = 0.5f;

    float tiempoCooldownStick = 0;
    
    bool pausa, tocandoParedStick, saltoEnActual, perdio;
    
    Rigidbody2D rigidBody;

    LayerMask layer;

    Vector2 ultimaVelocidad = Vector2.zero;
    
    /* -------------------------------------------------------------------------------- */

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
        rigidBody.velocity = Vector2.zero;
        rigidBody.gravityScale = 0;
    }
    
    private void onPausarJuego() {
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
    
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = gravity;
        layer = LayerMask.GetMask("Mapa");
    }

    /* -------------------------------------------------------------------------------- */
    
    void FixedUpdate()
    {
        if (pausa || perdio)
            return;

        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speedX, rigidBody.velocity.y);

        bool tocoTeclaDeSalto = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);

        if (tocoTeclaDeSalto && colisionaConPiso())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speedY);
        }
    }

    /* -------------------------------------------------------------------------------- */

    void Update()
    {
        if (pausa || perdio)
            return;
        
        if (tiempoCooldownStick <= 0 || rigidBody.gravityScale != 0)
            return;
        
        tiempoCooldownStick -= Time.deltaTime;
        
        if (tiempoCooldownStick <= 0)
        {
            rigidBody.gravityScale = gravity;
        }
    }

    /* -------------------------------------------------------------------------------- */
    
    private bool colisionaConPiso()
    {
        Transform thisTransform = transform;
        Vector3 scale = thisTransform.localScale;
        Vector3 position = thisTransform.position;
        
        Vector3 pointACollider = new Vector3(position.x + offsetXCollider, position.y - scale.y / 2);
        Vector3 pointBCollider = new Vector3(position.x - offsetXCollider, position.y - scale.y / 2 - offsetYCollider);

        Collider2D checkeoDePiso = Physics2D.OverlapArea(pointACollider, pointBCollider, layer);

        // Si esta tocando el piso, puede saltar
        if (checkeoDePiso != null) 
        {
            Debug.DrawLine(pointACollider, new Vector2(pointBCollider.x, pointACollider.y), Color.green, 5);
            Debug.DrawLine(pointACollider, new Vector2(pointACollider.x, pointBCollider.y), Color.green, 5);
            Debug.DrawLine(new Vector2(pointBCollider.x, pointACollider.y), pointBCollider, Color.green, 5);
            Debug.DrawLine(new Vector2(pointACollider.x, pointBCollider.y), pointBCollider, Color.green, 5);
            return true;
        }

        // Si no esta pegado a una pared sticky, no puede saltar
        if (!tocandoParedStick || saltoEnActual) 
            return false;
        
        // Esta pegado a una pared sticky, puede saltar
        rigidBody.gravityScale = gravity;
        saltoEnActual = true;
        return true;
    }
    
    /* -------------------------------------------------------------------------------- */
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("WallStick") || tocandoParedStick) 
            return;
        
        // Entro en wallstick
        rigidBody.gravityScale = 0;
        tocandoParedStick = true;
        saltoEnActual = false;
        tiempoCooldownStick = tiempoResistenciaStick;
        rigidBody.velocity = new Vector2(0, -speedYStick);
    }

    /* -------------------------------------------------------------------------------- */
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("WallStick")) 
            return;
        
        // Salio de wallstick
        tocandoParedStick = false;
        rigidBody.gravityScale = gravity;
    }
}

