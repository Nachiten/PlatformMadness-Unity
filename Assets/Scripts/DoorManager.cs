using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    bool doorOpen = false;

    GameObject doorClosedObject;

    void Awake()
    {
        foreach (Transform unHijo in transform)
        {
            if (unHijo.gameObject.name == "DoorClosed")
            {
                doorClosedObject = unHijo.gameObject;
            }
        }
    }

    public void abrirPuerta() {
        doorOpen = true;
        doorClosedObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doorOpen)
        {
            Debug.Log("La puerta esta ABIERTA");
            // TODO | Reproducir sonido de puerta abierta
        }
        else 
        {
            Debug.Log("La puerta esta CERRADA");
            // TODO | Reproducir sonido de puerta bloqueada
        }
            
    }
}
