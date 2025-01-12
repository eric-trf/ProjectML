using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Captura : MonoBehaviour
{
    SoundManager soundManager;
    Cronometro cronometro;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
        cronometro = GetComponent<Cronometro>();
    }

    public void CapturaFeita()
    {
        //toca som de captura aleatório (2*2 = 4 possibilidades)
        int random = UnityEngine.Random.Range(1,2);
        if (random == 1) soundManager.PlaySound(soundManager.berro);
        else if (random == 2) soundManager.PlaySound(soundManager.berro2);
        random = UnityEngine.Random.Range(1,2);
        if (random == 1) soundManager.PlaySound(soundManager.roupas);
        else if (random == 2) soundManager.PlaySound(soundManager.roupas2);

        //finaliza a cena
        cronometro.FinalizarTempo();
    }
}
