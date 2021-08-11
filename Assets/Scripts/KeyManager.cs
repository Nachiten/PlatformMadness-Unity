using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    string keyNumber;

    // Start is called before the first frame update
    void Awake()
    {
        string keyName = this.gameObject.name;

        // Obtengo el nombre de key a partir de su nombre
        keyNumber = keyName.Substring(3, keyName.Length - 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            GameObject.Find("Door" + keyNumber).GetComponent<DoorManager>().abrirPuerta();
            Destroy(gameObject);
        }
    }
}
