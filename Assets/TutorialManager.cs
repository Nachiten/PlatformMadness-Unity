using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    List<GameObject> textos = new List<GameObject>();
    
    void Awake()
    {
        int index = 0;

        GameObject objectoEncontrado;

        while ((objectoEncontrado = GameObject.Find("TextoTutorial" + index)) != null)
        {
            objectoEncontrado.SetActive(false);
            textos.Add(objectoEncontrado);
            index++;
        }
        
        
        //Debug.Log("Cantidad de texto tutorial: " + textos.Count);
    }

    public void mostrarTexto(int indexTexto)
    {
        GameObject objetoAMostrar = textos[indexTexto];
        
        if (!objetoAMostrar.activeSelf)
            ejecutarAnimacion(objetoAMostrar);
    }

    float alturaCompleta;
    const float tiempoAnimacion = 0.6f;
    
    void ejecutarAnimacion(GameObject objeto)
    {
        alturaCompleta = objeto.transform.localScale.y;

        LeanTween.scaleX(objeto, 0, 0).setOnComplete(_ => terminarAnimacion(objeto));
    }

    void terminarAnimacion(GameObject objeto)
    {
        objeto.SetActive(true);
        LeanTween.scaleX(objeto, alturaCompleta, tiempoAnimacion);
    }
}
