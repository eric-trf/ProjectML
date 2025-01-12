using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void CarregarCenaIA() 
    {
        SceneManager.LoadScene(1);
    }
    
    public void CarregarCenaML() 
    {
        SceneManager.LoadScene(2);
    }

    public void Fechar()
    {
        Application.Quit();
    }
}
