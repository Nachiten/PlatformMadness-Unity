using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class EditorTools : EditorWindow
{
    // Mostrar Ventana
    [MenuItem("Window/[EditorTools]")]
    public static void showWindow()
    {
        GetWindow<EditorTools>("EditorTools");
    }

    // --------------------------------------------------------------------------------

    int cantidadEscenas;

    private void OnEnable()
    {
        cantidadEscenas = SceneManager.sceneCountInBuildSettings;
    }

    // Codigo de la Ventana
    void OnGUI()
    {
        if (Application.isPlaying)
        {
            mostrarMenuJugando();
        }
        else
        {
            mostrarMenuNoJugando();
        }
    }
    
    void mostrarMenuNoJugando()
    {
        EditorGUILayout.LabelField("--- Ajustar Offset: ---");
        
            if (Selection.objects.Length <= 0)
            {
                EditorGUILayout.LabelField("No hay ningun objeto soportado seleccionado");
                return;
            }

            GameObject objectoSeleccionado;
            Vector3 posicionActual;
            
            try
            {
                objectoSeleccionado = (GameObject) Selection.objects[0];
                posicionActual = objectoSeleccionado.transform.position;
            }
            catch
            {
                EditorGUILayout.LabelField("No hay ningun objeto soportado seleccionado");
                return;
            }

            switch (objectoSeleccionado.tag)
            {
                case "DownWallCollider":
                    if (GUILayout.Button("Aplicar offset: DownWallCollider"))
                    {
                        const float offsetY = 1.5f;
                    
                        objectoSeleccionado.transform.position = new Vector3(posicionActual.x, posicionActual.y - offsetY, posicionActual.z);
                    }

                    break;
                case "UpWallCollider":
                    if (GUILayout.Button("Aplicar offset: UpWallCollider"))
                    {
                        const float offsetY = 0.6f;
                    
                        objectoSeleccionado.transform.position = new Vector3(posicionActual.x, posicionActual.y - offsetY, posicionActual.z);
                    }
                    break;
            
                case "WallStick":
                    const float offsetX = 0.562f;
                    
                    if (GUILayout.Button("Aplicar offset IZQUIERDA: WallStick"))
                    {
                        objectoSeleccionado.transform.position = new Vector3(posicionActual.x - offsetX, posicionActual.y, posicionActual.z);
                    }
                    if (GUILayout.Button("Aplicar offset DERECHA: WallStick"))
                    {
                        objectoSeleccionado.transform.position = new Vector3(posicionActual.x + offsetX, posicionActual.y, posicionActual.z);
                    }
                    break;
                default:
                    EditorGUILayout.LabelField("No hay ningun objeto soportado seleccionado");
                    return;
            }

            Debug.Log("Seleccionado: " + objectoSeleccionado.name);
    }

    void mostrarMenuJugando()
    {
        EditorGUILayout.LabelField("Viajar hacia escena:");

        mostrarMenuViajarAEscena();
        
        EditorGUILayout.LabelField("Borrar Todas las Keys:");

        if (GUILayout.Button("BORRAR TODO"))
        {
            PlayerPrefs.DeleteAll();

            if (SceneManager.GetActiveScene().buildIndex == 12)
            {
                GameObject.Find("GameManager").GetComponent<LevelLoader>().cargarNivel(12);
            }
        }
    }
    
    void mostrarMenuViajarAEscena() 
    {
        for (int i = 0;i < cantidadEscenas; i++)
        {
            string nombreEscena = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            if (GUILayout.Button("Ir a escena: [" + nombreEscena + "]"))
            {
                GameObject.Find("GameManager").GetComponent<LevelLoader>().cargarNivel(i);
            }
        }
    }
}

