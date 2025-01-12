using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Medicoes : MonoBehaviour
{
    Cronometro cronometro;
    
    private string filePath; // Caminho do arquivo (pode ser modificado conforme a necessidade)

    //variaveis a serem escritas
    public bool capturado;
    public float tempo;

    double totalDistanceToPlayer;
    int numeroDistancias;
    double distanciaMedia;

    void Start()
    {
        // Definir o caminho do arquivo na pasta persistente de dados
        filePath = Application.persistentDataPath + "/gameData.txt";
        Debug.Log(filePath);

        // Verificar se o arquivo já existe
        if (!File.Exists(filePath))
        {
            // Criar o arquivo se ele não existir
            File.WriteAllText(filePath, "tempo; capturado; distancia media;\n");
        }

        cronometro = GetComponent<Cronometro>();
    }

    public void EscreverLinha() //escreve resultados da partida finalizada em uma nova linha do arquivo
    {
        // Preparar o conteúdo a ser escrito
        string content = $"{tempo}; {capturado}; {distanciaMedia};\n";

        // Escrever no arquivo
        File.AppendAllText(filePath, content);

        // Exibir no console para confirmar
        Debug.Log("Variáveis escritas no arquivo: " + content);
    }

    public void MedirDistancia(float distanceToPlayer)
    {
        totalDistanceToPlayer += distanceToPlayer;
        numeroDistancias ++;
    }

    public void Resetar()
    {
        totalDistanceToPlayer = 0f;
        numeroDistancias = 0;
    }

    public void ColetarMedidas(float aux)
    {
        tempo = aux;
        //alterar as variaveis medidas na partida finalizada;
        if (tempo == 0) capturado = false;
        if (tempo > 0) capturado = true;

        Debug.Log(totalDistanceToPlayer+" / "+numeroDistancias+" = "+ distanciaMedia);
        distanciaMedia = totalDistanceToPlayer/(double)numeroDistancias;

        //chamar funcao que gera linha no arquivo com as medidas da partida finalizada
        EscreverLinha();
    }

    void calcularResultadosFinais()
    {
        //le o arquivo com todas as medidas escritas até o momento e gera a ultima linha totalizando tudo

        //totalJogos = capturado+naoCapturado;
        //taxaSucesso = capturado/totalJogos;
    }
}
