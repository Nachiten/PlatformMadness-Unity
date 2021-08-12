using UnityEngine;

public class KeyManager : MonoBehaviour
{
    string keyNumber;

    SoundManager soundManager;

    // Start is called before the first frame update
    void Awake()
    {
        string keyName = this.gameObject.name;

        // Obtengo el nombre de key a partir de su nombre
        keyNumber = keyName.Substring(3, keyName.Length - 3);

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            GameObject.Find("Door" + keyNumber).GetComponent<DoorManager>().abrirPuerta();

            soundManager.reproducirSonido(2);

            Destroy(gameObject);
        }
    }
}
