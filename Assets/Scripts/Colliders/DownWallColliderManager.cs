using UnityEngine;
using UnityEngine.Assertions;

public class DownWallColliderManager : MonoBehaviour
{
    CompositeCollider2D tileMapCollider;

    /* -------------------------------------------------------------------------------- */
    
    void Awake()
    {
        tileMapCollider = GameObject.Find("Tilemap_DownWall").GetComponent<CompositeCollider2D>();

        Assert.IsNotNull(tileMapCollider);

        tileMapCollider.isTrigger = true;
    }

    /* -------------------------------------------------------------------------------- */
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Activo collider
        tileMapCollider.isTrigger = false;
    }

    /* -------------------------------------------------------------------------------- */
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Desactivo collider
        tileMapCollider.isTrigger = true;
    }
}
