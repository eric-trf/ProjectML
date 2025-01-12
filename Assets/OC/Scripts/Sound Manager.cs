using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource sfxSource;

    [Header("Ambience Clip")]
    public AudioClip vento;
    public AudioClip grilo;
    public AudioClip passaros;
    
    [Header("SFX Clip")]
    public AudioClip roupas;
    public AudioClip roupas2;
    public AudioClip berro;
    public AudioClip berro2;
    public AudioClip grama;

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
