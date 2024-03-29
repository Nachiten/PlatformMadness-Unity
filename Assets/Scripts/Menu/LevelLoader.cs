﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LevelLoader : MonoBehaviour
{
    static GameObject levelLoader, panelCargaColor, restoPanelCarga;
    static Slider slider;
    static TMP_Text textoProgreso, textoNivel;

    static bool variablesAsignadas = false;

    int indexACargar;

    int indexActual;
    
    /* -------------------------------------------------------------------------------- */

    #region FuncionesInicio

    private void Awake()
    {
        indexActual = SceneManager.GetActiveScene().buildIndex;
        
        if (variablesAsignadas)
            return;
        
        try
        {
            // Asignar variables
            slider = GameObject.Find("Barra Carga").GetComponent<Slider>();
            levelLoader = GameObject.Find("Panel Carga");
            panelCargaColor = GameObject.Find("PanelColorCarga");
            restoPanelCarga = GameObject.Find("RestoPanelCarga");

            textoProgreso = GameObject.Find("TextoProgreso").GetComponent<TMP_Text>();
            textoNivel = GameObject.Find("Texto Cargando").GetComponent<TMP_Text>();

            if (slider == null)
            {
                throw new Exception("slider");
            }
            if (levelLoader == null)
            {
                throw new Exception("levelLoader");
            }
            if (panelCargaColor == null)
            {
                throw new Exception("panelCargaColor");
            }
            if (restoPanelCarga == null)
            {
                throw new Exception("restoPanelCarga");
            }
            if (textoProgreso == null)
            {
                throw new Exception("textoProgreso");
            }
            if (textoNivel == null)
            {
                throw new Exception("textoNivel");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[LevelLoader] Error al asignar variable: " + e.Message);
        }
    }

    /* -------------------------------------------------------------------------------- */

    void Start()
    {
        if (!variablesAsignadas)
        {
            levelLoader.SetActive(false);
            variablesAsignadas = true;
        }
        else
            quitarPanelCarga();
    }

    #endregion

    /* -------------------------------------------------------------------------------- */
    
    public void cargarNivel(int index)
    {
        indexACargar = index;
        textoNivel.text = "...";

        ponerPanelCarga();
    }

    /* -------------------------------------------------------------------------------- */

    // Iniciar Corutina para cargar nivel en background
    IEnumerator cargarAsincronizadamente()
    {
        // Iniciar carga de escena
        AsyncOperation operacion = SceneManager.LoadSceneAsync(indexACargar);

        operacion.allowSceneActivation = true;

        Debug.Log("[LevelLoader] Cargando escena: " + indexACargar);

        string nombreEscena = SceneManager.GetSceneByBuildIndex(indexACargar).name;

        textoNivel.text = "Cargando " + nombreEscena + " ...";

        // Mientras la operacion no este terminada
        while (!operacion.isDone)
        {
            // Generar valor entre 0 y 1
            float progress = Mathf.Clamp01(operacion.progress / 0.9f);
            // Modificar Slider
            slider.value = progress;
            // Modificar texto progreso
            textoProgreso.text = progress * 100f + "%";

            yield return null;
        }
    }

    /* -------------------------------------------------------------------------------- */

    const int ultimoNivelIndex = 3, ultimoTutorialIndex = 6;

    public void cargarSiguienteNivel()
    {
        switch (indexActual)
        {
            // Si estos en ultimo nivel, o ultimo tutorial regreso a inicio
            case ultimoNivelIndex:
            case ultimoTutorialIndex:
                cargarNivel(0);
                break;
            // Sino, sigo al siguiente nivel
            default:
                cargarNivel(indexActual + 1);
                break;
        }
    }

    public void salir() 
    {
        Debug.LogWarning("[LevelLoader] Cerrando juego");
        Application.Quit(); 
    }

    /* -------------------------------------------------------------------------------- */

    #region AnimacionPonerPanelCarga

    /* ----------------------------------------------------------------------------------------- */
    // ------------------------------ ANIMACION PONER PANEL CARGA ------------------------------ // 
    /* ----------------------------------------------------------------------------------------- */

    const float tiempoAnimacionColorPanel = 0.3f; // 0.3
    const float tiempoAnimacionRestoPanel = 0.2f; // 0.2

    void ponerPanelCarga()
    {
        restoPanelCarga.SetActive(false);

        LeanTween.value(levelLoader, 0, 0, 0f)
            .setOnUpdate(mostrarColorPanelAlfa)
            .setOnComplete(iniciarMostrarPanelCarga);  
    }

    void mostrarColorPanelAlfa(float value) 
    {
        panelCargaColor.GetComponent<Image>().color = new Color(0.149f, 0.149f, 0.149f, value);
    }

    void iniciarMostrarPanelCarga() 
    {
        levelLoader.SetActive(true);

        LeanTween.value(levelLoader, 0, 1, tiempoAnimacionColorPanel)
                .setOnUpdate(mostrarColorPanelAlfa)
                .setOnComplete(mostrarPanelCarga);
    }

    void mostrarPanelCarga()
    {
        LeanTween.scaleY(restoPanelCarga, 0, 0f).setOnComplete(mostrarRestoPanelCarga);
    }

    void mostrarRestoPanelCarga() 
    {
        restoPanelCarga.SetActive(true);
        Debug.Log("Se muestra el panel carga");
        LeanTween.scaleY(restoPanelCarga, 1, tiempoAnimacionRestoPanel).setOnComplete(completarCargaNivel);
    }

    private void OnDisable()
    {
        Debug.Log("onDisable");
    }


    void completarCargaNivel()
    {
        // No existe la escena
        if (indexACargar > SceneManager.sceneCountInBuildSettings - 1)
        {
            textoNivel.text = "No existe la escena que se quiere cargar. Por favor reinicie el juego";
        }
        // Si existe la escena
        else
        {
            Debug.Log("Cargando escena: " + indexACargar);
            StartCoroutine(cargarAsincronizadamente());
        }
    }

    #endregion

    #region AnimacionQuitarPanelCarga

    /* ---------------------------------------------------------------------------------------- */
    // ----------------------------- ANIMACION QUITAR PANEL CARGA ----------------------------- // 
    /* ---------------------------------------------------------------------------------------- */

    void quitarPanelCarga() 
    {
        LeanTween.scaleY(restoPanelCarga, 0, tiempoAnimacionRestoPanel).setOnComplete(ocultarColorPanelAlfa);
    }

    void ocultarColorPanelAlfa() 
    {
        LeanTween.value(levelLoader, 1, 0, tiempoAnimacionColorPanel)
            .setOnUpdate(mostrarColorPanelAlfa)
            .setOnComplete(esconderPanelCarga);
    }

    void esconderPanelCarga() { levelLoader.SetActive(false); }

    #endregion
}
