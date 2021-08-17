using UnityEngine;

public class TutorialColliderManager : MonoBehaviour
{
    int colliderNumber;

    private TutorialManager codigoTutorial;
    
    void Awake()
    {
        string keyName = gameObject.name;

        // Obtengo el nombre de key a partir de su nombre
        keyName = keyName.Substring(16, keyName.Length - 16);

        colliderNumber = int.Parse(keyName);

        codigoTutorial = GameObject.Find("GameManager").GetComponent<TutorialManager>();

        //Debug.Log("Collider Number: " + colliderNumber + " | Name: " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D _)
    {
        codigoTutorial.mostrarTexto(colliderNumber);
    }
}
