using UnityEngine;

public class UpWallColliderManager : MonoBehaviour
{
    CompositeCollider2D tileMapCollider;

    /* -------------------------------------------------------------------------------- */
    
    void Awake()
    {
        tileMapCollider = GameObject.Find("Tilemap_UpWall").GetComponent<CompositeCollider2D>();

        if (tileMapCollider == null)
        {
            Debug.LogError("[UpWallColliderManager] No se encuentra el tilemap asociado");
            return;
        }

        tileMapCollider.isTrigger = false;
    }

    /* -------------------------------------------------------------------------------- */
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        tileMapCollider.isTrigger = true;
    }

    /* -------------------------------------------------------------------------------- */
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        tileMapCollider.isTrigger = false;
    }
}
