using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class EditorTools : EditorWindow
{
    // Mostrar Ventana
    [MenuItem("Window/[EditorTools]")]
    public static void ShowWindow()
    {
        GetWindow<EditorTools>("EditorTools");
    }

    // --------------------------------------------------------------------------------

    int cantidadEscenas;

    private void OnEnable()
    {
        cantidadEscenas = SceneManager.sceneCountInBuildSettings;
    }

    // Codigo de la Vetana
    void OnGUI()
    {
        if (!Application.isPlaying)
        {
            EditorGUILayout.LabelField("----------------------------------------------------------------------");
            EditorGUILayout.LabelField("------ Debes comenzar a jugar para ver las opciones de este menu.  ------");
            EditorGUILayout.LabelField("----------------------------------------------------------------------");
            return;
        }

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
        for (int i = 0; i < cantidadEscenas; i++)
        {
            string nombreEscena = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            if (GUILayout.Button("Ir a escena: [" + nombreEscena + "]"))
            {
                GameObject.Find("GameManager").GetComponent<LevelLoader>().cargarNivel(i);
            }
        }
    }
}

