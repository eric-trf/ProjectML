using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cronometro : MonoBehaviour
{
    public static float tempoFinal;
    private float tempo;
    public float tempoInicial;

    // Start is called before the first frame update
    void Start()
    {
        tempo = tempoInicial;
    }

    // Update is called once per frame
    void Update()
    {
        tempo -= Time.deltaTime;
        if (tempo <= 0)
        {
            tempo = 0;
            FinalizarTempo();
        }
    }

    public void FinalizarTempo()
    {
        tempoFinal = tempoInicial - tempo;
        SceneManager.LoadScene(3);
    }
}
