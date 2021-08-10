using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    bool doorOpen = false;

    public void abrirPuerta() {
        doorOpen = true;
        GetComponent<SpriteRenderer>().color = new Color(115,255,102);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doorOpen)
            Debug.Log("La puerta esta ABIERTA");
        else
            Debug.Log("La puerta esta CERRADA");
    }
}
