using UnityEngine;

public class KeyManager : MonoBehaviour
{
    string keyNumber;

    SoundManager soundManager;

    /* -------------------------------------------------------------------------------- */
    
    void Awake()
    {
        string keyName = gameObject.name;

        // Obtengo el nombre de key a partir de su nombre
        keyNumber = keyName.Substring(3, keyName.Length - 3);

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    /* -------------------------------------------------------------------------------- */
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Jugador")) return;
        
        GameObject.Find("Door" + keyNumber).GetComponent<DoorManager>().abrirPuerta();

        soundManager.reproducirSonido(2);

        Destroy(gameObject);
    }
}
