using System;
using UnityEngine;

public class SpikesCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Jugador"))
            return;
        
        Debug.Log("Tocaste un pincho!!");
    }
}
