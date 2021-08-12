using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] clipsSonido;

    AudioSource sourceSonido;

    /* -------------------------------------------------------------------------------- */

    private void Awake() { 
        try
        {
            sourceSonido = GetComponent<AudioSource>();

            if (sourceSonido == null)
            {
                throw new Exception("No se pudo asignar variable: sourceSonido");
            }

            if (clipsSonido == null || clipsSonido.Length == 0) {
                throw new Exception("No hay ningun clip de sonido");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[SoundManager] Error: " + e.Message);
        }
    }

    public void reproducirSonido(int sonido)
    {
        try
        {
            sourceSonido.clip = clipsSonido[sonido];

            if (!sourceSonido.isPlaying)
                sourceSonido.Play();
        }
        catch (Exception e) {
            Debug.LogError("[SoundManager] Error: " + e.Message);
        }
        
    }
}