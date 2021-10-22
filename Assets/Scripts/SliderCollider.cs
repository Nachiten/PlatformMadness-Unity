using UnityEngine;
using UnityEngine.Assertions;

public class SliderCollider : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    
    void Awake()
    {
        playerRigidBody = GameObject.Find("Jugador").GetComponent<Rigidbody2D>();

        Assert.IsNotNull(playerRigidBody);
    }

    public float speed = 10;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Jugador"))
            return;
        
        Debug.Log("[Trigger] Aplicando velocidad");
        
        playerRigidBody.velocity = new Vector2(speed,0);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Jugador"))
            return;
        
        Debug.Log("[Collision] Aplicando velocidad");
        
        playerRigidBody.AddForce(new Vector2(speed,0));
    }
}
