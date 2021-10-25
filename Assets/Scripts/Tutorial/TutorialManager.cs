using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TutorialManager : MonoBehaviour
{
    private readonly List<GameObject> textos = new List<GameObject>();
    
    float alturaCompleta;
    const float tiempoAnimacion = 0.6f;
    
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
    }

    public void mostrarTexto(int indexTexto)
    {
        GameObject objetoAMostrar = textos[indexTexto];
        
        Assert.IsNotNull(objetoAMostrar);
        
        if (!objetoAMostrar.activeSelf)
            ejecutarAnimacion(objetoAMostrar);
    }


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
