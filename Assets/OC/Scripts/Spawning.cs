using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Spawning : MonoBehaviour
{
    List<GameObject> spawnPoints;
    bool[] spawnPointsOccupied; //cada espaço inicia como falso
    private int pointNumber;

    public void Awake()
    {
        Debug.Log("Spawning iniciado");
        //adiciona todos os spawnpoints filhos do objeto com o script numa lista
        spawnPoints = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.tag == "Spawn Point")
            {
                spawnPoints.Add(child.gameObject);
            }
        }

        //inicializa o vetor de flags de ocupação do spawnpoint
        spawnPointsOccupied = new bool[spawnPoints.Count];
    }

    public Vector3 RandomSpawnPoint() //sorteia um spawn point, não desativa rb ou navmesh agent
    {
        //testando se há pontos de spawn vazios
        int count = 0;
        for (int iteration = 0; iteration < spawnPointsOccupied.Length; iteration++)
        {
            if (spawnPointsOccupied[iteration]) count++;
        }
        if (count == spawnPointsOccupied.Length)
        {
            Debug.Log("Todos pontos de nascimento ocupados");
        }

        //sorteando ponto de spawn dentro dos disponíveis
        int aux = 0;
        do
        {
            pointNumber = UnityEngine.Random.Range(1,spawnPoints.Count);

            //interromper o código para evitar crash
            aux++;
            if (aux>100) {Debug.Log("loop infinito"); break;}
        } while (spawnPointsOccupied[pointNumber]);

        //ocupa o spawn point para evitar que 2 personagens sejam colocados no mesmo local
        spawnPointsOccupied[pointNumber] = true;
        return spawnPoints[pointNumber].transform.localPosition;
    }

    public void ClearOccupiedSpawns()
    {
        //limpa os flags de spawnpoint ocupado
        for (int iteration = 0; iteration < spawnPointsOccupied.Length; iteration++)
        {
            spawnPointsOccupied[iteration] = false;
        }
        Debug.Log("pontos limpos");
    }

    public void SpawnObject(GameObject objeto) //desativa rb e/ou navmesh agent e o coloca num spawn point sorteado
    {
        //se o objeto tiver navmeshagent, desativar antes de mudar transform
        if (objeto.GetComponent<NavMeshAgent>() != null)
        {
            objeto.GetComponent<NavMeshAgent>().enabled = false;
        }

        //se o objeto tiver rigidbody, desativar antes de mudar transform
        if (objeto.GetComponent<Rigidbody>() != null)
        {
            objeto.GetComponent<Rigidbody>().Sleep();
        }

        //coloca o objeto num spawnpoint sorteado
        objeto.transform.localPosition = RandomSpawnPoint();

        //se o objeto tiver navmeshagent, reativar depois de mudar transform
        if (objeto.GetComponent<NavMeshAgent>() != null)
        {
            objeto.GetComponent<NavMeshAgent>().enabled = true;
        }

        //se o objeto tiver rigidbody, reativar depois de mudar transform
        if (objeto.GetComponent<Rigidbody>() != null)
        {
            objeto.GetComponent<Rigidbody>().WakeUp();
        }
    }

}