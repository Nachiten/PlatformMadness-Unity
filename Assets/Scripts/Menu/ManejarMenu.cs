using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Assertions;

public class ManejarMenu : MonoBehaviour
{
    #region Variables

    // Flag de ya asigne las variables
    static bool variablesAsignadas = false;

    // GameObjects
    static GameObject menu, opciones, creditos, panelMenu;

    // Manager de las animaciones
    static LeanTweenManager tweenManager;

    // Textos varios
    static TMP_Text textoBoton;

    // Flags varios
    bool menuActivo = false, opcionesActivas = false, creditosActivos = false;

    // Strings utilizados
    const string continuar = "CONTINUAR", comenzar = "COMENZAR";

    // Index de escena actual
    int index;

    #endregion

    /* -------------------------------------------------------------------------------- */

    #region FuncionStart

    private void Awake()
    {
        if (variablesAsignadas)
            return;
        
        menu = GameObject.Find("Menu");
        panelMenu = GameObject.Find("PanelMenu");
        opciones = GameObject.Find("MenuOpciones");
        creditos = GameObject.Find("MenuCreditos");

        textoBoton = GameObject.Find("TextoBotonComenzar").GetComponent<TMP_Text>();

        tweenManager = GameObject.Find("Canvas Menu").GetComponent<LeanTweenManager>();
        
        Assert.IsNotNull(menu);
        Assert.IsNotNull(panelMenu);
        Assert.IsNotNull(opciones);
        Assert.IsNotNull(creditos);
        
        Assert.IsNotNull(textoBoton);
        
        Assert.IsNotNull(tweenManager);

        variablesAsignadas = true;
    }

    /* -------------------------------------------------------------------------------- */

    void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;

        // No estoy en el menu principal
        if (index != 0)
        {
            textoBoton.text = continuar;

            // Oculto las cosas de una patada pq se esta mostrando la pantalla de carga
            menu.SetActive(false);
            panelMenu.SetActive(false);
        }
        // Estoy en el menu principal
        else
        {
            textoBoton.text = comenzar;

            menu.SetActive(true);
            panelMenu.SetActive(true); 
            menuActivo = true;
        }

        opciones.SetActive(false);
        creditos.SetActive(false);
    }

    #endregion

    /* -------------------------------------------------------------------------------- */

    #region FuncionUpdate

    void Update()
    {
        if (index == 0) return;

        bool animacionEnEjecucion = GameObject.Find("Canvas Menu").GetComponent<LeanTweenManager>().animacionEnEjecucion;

        if (Input.GetKeyDown("escape") && !animacionEnEjecucion)
            manejarMenu();
    }

    #endregion

    /* -------------------------------------------------------------------------------- */

    public void manejarMenu() 
    {
        menuActivo = !menuActivo;

        if (menuActivo)
        {
            // Abro el menu
            menu.SetActive(true);
            tweenManager.abrirMenuPausa();
        }
        else 
        {
            // Cierro el menu
            tweenManager.cerrarMenuPausa();
        }

        // Cierro las opciones si estaban activas en ambos casos
        if (!opcionesActivas) 
            return;
        
        tweenManager.cerrarOpciones();
        opcionesActivas = false;
    }

    /* -------------------------------------------------------------------------------- */

    public void manejarOpciones()
    {
        opcionesActivas = !opcionesActivas;

        if (opcionesActivas)
            tweenManager.abrirOpciones();
        else
            tweenManager.cerrarOpciones();
        
    }

    /* -------------------------------------------------------------------------------- */
    public void manejarCreditos() 
    {
        creditosActivos = !creditosActivos;

        if (creditosActivos)
            tweenManager.abrirCreditos();
        
        else
            tweenManager.cerrarCreditos();
    }
}
