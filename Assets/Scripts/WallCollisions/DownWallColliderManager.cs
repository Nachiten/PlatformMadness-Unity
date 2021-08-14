using UnityEngine;

public class DownWallColliderManager : MonoBehaviour
{
    static int collisionsCounter = 0;

    CompositeCollider2D tileMapCollider;

    /* -------------------------------------------------------------------------------- */
    
    void Awake()
    {
        tileMapCollider = GameObject.Find("Tilemap_DownWall").GetComponent<CompositeCollider2D>();

        if (tileMapCollider == null)
        {
            Debug.LogError("[DownWallColliderManager] No se encuentra el tilemap asociado");
            return;
        }

        tileMapCollider.isTrigger = true;
    }

    /* -------------------------------------------------------------------------------- */
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        tileMapCollider.isTrigger = false;
        Debug.Log("Activando collider");
        collisionsCounter++;
    }

    /* -------------------------------------------------------------------------------- */
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionsCounter--;
        if (collisionsCounter != 0) return;
        
        Debug.Log("Desactivando collider");
        tileMapCollider.isTrigger = true;
    }
}
