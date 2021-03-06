using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    TMP_Text textoNivel;
    
    /* -------------------------------------------------------------------------------- */

    #region singleton
    
    private void inicializarSingleton()
    {
        // Si ya hay un singleton que no soy yo
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
    }
    
    #endregion

    private void Awake()
    {
        inicializarSingleton();
        
        textoNivel = GameObject.Find("TextoNivel").GetComponent<TMP_Text>();
    }

    /* -------------------------------------------------------------------------------- */
    
    void Start()
    {
        string nombreNivel = SceneManager.GetActiveScene().name;
        
        textoNivel.text = nombreNivel;
    }

    /* -------------------------------------------------------------------------------- */

    public event Action pauseGameEvent, lostGameEvent;
    
    public void pausarJuego() 
    {
        pauseGameEvent?.Invoke();
    }
    
    public void perderJuego()
    {
        lostGameEvent?.Invoke();
    }
}
