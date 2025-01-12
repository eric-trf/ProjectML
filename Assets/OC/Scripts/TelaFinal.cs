using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TelaFinal : MonoBehaviour
{
    public GameObject textPontos;

    void Start()
    {
        //arredonda para 2 casas decimais o tempo contado
        Cronometro.tempoFinal = (float)Math.Round(Cronometro.tempoFinal, 2);

        //gera o texto do tempo
        textPontos.GetComponent<TextMeshProUGUI>().text = Cronometro.tempoFinal.ToString()+" segundos";
    }

    public void CarregarCenaIA() 
    {
        SceneManager.LoadScene(1);
    }
    
    public void CarregarCenaML() 
    {
        SceneManager.LoadScene(2);
    }

    public void CarregarMenu() 
    {
        SceneManager.LoadScene(0);
    }
}
