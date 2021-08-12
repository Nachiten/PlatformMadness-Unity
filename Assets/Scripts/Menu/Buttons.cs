using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    int nivelACargar = 1;

    static PopUpsMenu codigoPopUpsMenu;
    static ManejarMenu codigoManejarMenu;
    static SoundManager codigoSoundManager;

    /* -------------------------------------------------------------------------------- */

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /* -------------------------------------------------------------------------------- */

    // Setup que se hace por unica vez
    void Awake()
    {
        codigoSoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        codigoPopUpsMenu = GameObject.Find("Pop Up").GetComponent<PopUpsMenu>();

        try { 
            if (codigoSoundManager == null) {
                throw new Exception("codigoSoundManager");
            }

            if (codigoPopUpsMenu == null)
            {
                throw new Exception("codigoSoundManager");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[Buttons] Error al asignar variable: " + e.Message);
        }
    }

    /* -------------------------------------------------------------------------------- */

    // Se llama cuando una nueva escena se carga
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        setupInicial();
    }

    /* -------------------------------------------------------------------------------- */

    // Setup que se hace en cada nueva escena cargada
    void setupInicial()
    {
        try
        {
            codigoManejarMenu = GameObject.Find("GameManager").GetComponent<ManejarMenu>();

            if (codigoManejarMenu == null)
            {
                throw new Exception("codigoManejarMenu");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[Buttons] Error al asignar variable: " + e.Message);
        }
    }

    #region Botones

    /* --------------------------------------------------------------------------------- */
    /* ------------------------------------ BOTONES ------------------------------------ */
    /* --------------------------------------------------------------------------------- */

    public void botonComenzar()
    {
        reproducirSonidoClickBoton();

        if (SceneManager.GetActiveScene().buildIndex == 0)
            loadLevel(nivelACargar);
        else
            codigoManejarMenu.manejarMenu();
    }

    public void botonOpciones()
    {
        reproducirSonidoClickBoton();

        codigoManejarMenu.manejarOpciones();
    }

    public void botonSalir()
    {
        reproducirSonidoClickBoton();

        codigoPopUpsMenu.abrirPopUp(3);
    }

    public void botonVolverAInicio()
    {
        reproducirSonidoClickBoton();

        loadLevel(0);
    }

    public void botonBorrarProgreso()
    {
        reproducirSonidoClickBoton();

        codigoPopUpsMenu.abrirPopUp(0);
    }

    public void botonCreditos()
    {
        reproducirSonidoClickBoton();

        codigoManejarMenu.manejarCreditos();
    }

    public void botonSeleccionarNivel()
    {
        reproducirSonidoClickBoton();

        // TODO | Agregar viaje hasta seleccionar nivel
        Debug.LogWarning("[Buttons] Falta implementar esta funcionalidad.");
    }

    #endregion

    /* ------------------------------------------------------------------------------------ */
    /* ------------------------------------ AUXILIARES ------------------------------------ */
    /* ------------------------------------------------------------------------------------ */

    public void loadLevel(int index) 
    {
        LevelLoader codigoLevelLoader = GameObject.Find("GameManager").GetComponent<LevelLoader>();

        codigoLevelLoader.cargarNivel(index); 
    }

    /* ------------------------------------------------------------------------------------ */

    void reproducirSonidoClickBoton() { codigoSoundManager.reproducirSonido(1); }
}