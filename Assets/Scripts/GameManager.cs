using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void manejarPausa() 
    {
        GameObject.Find("Jugador").GetComponent<PlayerMovement>().manejarPausa();
    }

    /* -------------------------------------------------------------------------------- */

    public void ganoJuego()
    {
        
    }
}
