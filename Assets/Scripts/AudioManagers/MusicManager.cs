using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] clipsMusica;

    AudioSource sourceMusica;

    /* -------------------------------------------------------------------------------- */

    void Awake() { 
        try
        {
            sourceMusica = GetComponent<AudioSource>();

            if (sourceMusica == null) {

                throw new Exception("sourceMusica");
            } 
        }
        catch (Exception e)
        {
            Debug.LogError("[MusicManager] Error al asignar variable: " + e.Message);
        }
    }

    /* -------------------------------------------------------------------------------- */

    public void reproducirMusica(int musica)
    {
        try {
            sourceMusica.Stop();

            sourceMusica.clip = clipsMusica[musica];
            sourceMusica.Play();
        }
        catch (Exception e)
        {
            Debug.LogError("[MusicManager] Error: " + e.Message);
        }
    }

    /* -------------------------------------------------------------------------------- */

    public void pararMusica() 
    {
        sourceMusica.Stop();
    }
}
