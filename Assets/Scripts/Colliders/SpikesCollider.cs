using UnityEngine;

public class SpikesCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Jugador"))
            return;
        
        GameManager.instance.perderJuego();
    }
}
