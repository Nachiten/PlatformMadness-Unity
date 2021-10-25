using UnityEngine;
using UnityEngine.Assertions;

public class DoorManager : MonoBehaviour
{
    public bool esPuertaAlSiguienteNivel = false;
    
    SpriteRenderer lockObject, lockBackground;
    GameObject doorClosedObject;
    SoundManager soundManager;

    BoxCollider2D colliderObject;

    LevelLoader levelLoader;

    const float animationTime = 0.3f, animationsDelay = 0.6f, alphaFinal = 0.35f;

    readonly Color backgroundColor = new Color(200,200,200);

    bool animacionEnEjecucion = false;

    void Awake()
    {
        foreach (Transform unHijo in transform)
        {
            switch (unHijo.gameObject.name)
            {
                case "DoorClosed":
                    doorClosedObject = unHijo.gameObject;
                    break;
                case "Lock":
                    lockObject = unHijo.gameObject.GetComponent<SpriteRenderer>();
                    break;
                case "LockBackground":
                    lockBackground = unHijo.gameObject.GetComponent<SpriteRenderer>();
                    break;
            }
        }
        
        colliderObject = GetComponent<BoxCollider2D>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        levelLoader = GameObject.Find("GameManager").GetComponent<LevelLoader>();
        
        Assert.IsNotNull(colliderObject);
        Assert.IsNotNull(soundManager);
        Assert.IsNotNull(levelLoader);
        
        colliderObject.isTrigger = false;
    }

    /* -------------------------------------------------------------------------------- */
    
    public void abrirPuerta()
    {
        doorClosedObject.SetActive(false);
        colliderObject.isTrigger = true;
    }

    /* -------------------------------------------------------------------------------- */
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!esPuertaAlSiguienteNivel)
            return;
        
        Debug.Log("La puerta esta ABIERTA");
        levelLoader.cargarSiguienteNivel();
    }

    /* -------------------------------------------------------------------------------- */
    
    private void OnCollisionEnter2D()
    {
        Debug.Log("La puerta esta CERRADA");
        soundManager.reproducirSonido(3);
        if (!animacionEnEjecucion)
            ponerCandado();
    }

    /* -------------------------------------------------------------------------------- */
    
    #region AnimacionPonerCandado

    /* ----------------------------------------------------------------------------------- */
    // ----------------------------- ANIMACION PONER CANDADO ----------------------------- //
    /* ----------------------------------------------------------------------------------- */
    
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

    #endregion

    #region AnimacionQuitarCandado
    
    /* ------------------------------------------------------------------------------------ */
    // ----------------------------- ANIMACION QUITAR CANDADO ----------------------------- //
    /* ------------------------------------------------------------------------------------ */
    
    void quitarCandado() 
    {
        LeanTween.value(gameObject, alphaFinal, 0, animationTime).setOnUpdate(cambiarAlfaBackground).setDelay(animationsDelay);
        LeanTween.value(gameObject, 1, 0, animationTime).setOnUpdate(cambiarAlfaLock).setDelay(animationsDelay).setOnComplete(terminarAnimacion);
    }

    void terminarAnimacion() 
    {
        animacionEnEjecucion = false;
    }
    
    #endregion
}
