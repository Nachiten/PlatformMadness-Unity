using System;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    bool doorOpen = false;

    SpriteRenderer lockObject;
    SpriteRenderer lockBackground;
    GameObject doorClosedObject;
    SoundManager soundManager;

    void Awake()
    {
        foreach (Transform unHijo in transform)
        {
            if (unHijo.gameObject.name == "DoorClosed")
            {
                doorClosedObject = unHijo.gameObject;
            }
            if (unHijo.gameObject.name == "Lock")
            {
                lockObject = unHijo.gameObject.GetComponent<SpriteRenderer>();
            }
            if (unHijo.gameObject.name == "LockBackground")
            {
                lockBackground = unHijo.gameObject.GetComponent<SpriteRenderer>();
            }
        }

        try
        {
            if (doorClosedObject == null) {
                throw new Exception("doorClosedObject");
            }
            if (lockObject == null)
            {
                throw new Exception("lockObject");
            }
            if (lockBackground == null)
            {
                throw new Exception("lockBackground");
            }
        }
        catch (Exception e) {
            Debug.LogError("[DoorManager] Error al asignar variable: " + e.Message);
        }

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void abrirPuerta()
    {
        doorOpen = true;
        doorClosedObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (doorOpen)
        {
            Debug.Log("La puerta esta ABIERTA");
            // TODO | Pasar a siguiente nivel
        }
        else
        {
            Debug.Log("La puerta esta CERRADA");
            soundManager.reproducirSonido(3);
            if (!animacionEnEjecucion)
                ponerCandado();
        }
    }

    float animationTime = 0.3f;
    float animationsDelay = 0.6f;
    float alphaFinal = 0.35f;

    Color backgroundColor = new Color(200,200,200);

    bool animacionEnEjecucion = false;

    void ponerCandado() {
        animacionEnEjecucion = true;
        LeanTween.value(gameObject, 0, alphaFinal, animationTime).setOnUpdate(cambiarAlfaBackground).setOnComplete(quitarCandado);
        LeanTween.value(gameObject, 0, 1, animationTime).setOnUpdate(cambiarAlfaLock).setOnComplete(quitarCandado);
    }

    void cambiarAlfaBackground(float val) 
    {
        Color color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, val);

        lockBackground.color = color;
    }

    void cambiarAlfaLock(float val)
    {
        Color color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, val);

        lockObject.color = color;
    }

    void quitarCandado() 
    {
        LeanTween.value(gameObject, alphaFinal, 0, animationTime).setOnUpdate(cambiarAlfaBackground).setDelay(animationsDelay);
        LeanTween.value(gameObject, 1, 0, animationTime).setOnUpdate(cambiarAlfaLock).setDelay(animationsDelay).setOnComplete(terminarAnimacion);
    }

    void terminarAnimacion() 
    {
        animacionEnEjecucion = false;
    }
}
