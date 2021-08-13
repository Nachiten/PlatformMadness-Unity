using UnityEngine;

public class UpWallColliderManager : MonoBehaviour
{
    static int collisionsCounter = 0;

    CompositeCollider2D tileMapCollider;

    void Awake()
    {
        tileMapCollider = GameObject.Find("Tilemap_UpWall").GetComponent<CompositeCollider2D>();

        if (tileMapCollider == null)
        {
            Debug.LogError("[UpWallColliderManager] No se encuentra el tilemap asociado");
            return;
        }

        tileMapCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tileMapCollider.isTrigger = false;
        collisionsCounter++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionsCounter--;
        if (collisionsCounter == 0) 
            tileMapCollider.isTrigger = true;
    }
}
