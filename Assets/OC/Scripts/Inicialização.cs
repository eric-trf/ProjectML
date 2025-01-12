using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inicializacao : MonoBehaviour
{
    [SerializeField] GameObject cervo;
    [SerializeField] GameObject cacador;
    [SerializeField] GameObject spawnlist;
    Spawning spawning;

    void Start()
    {
        spawning = spawnlist.GetComponent<Spawning>();
        if (spawning == null) Debug.Log("spawning null");
        //spawning.Start();
        Inicializar();
    }

    public void Inicializar()
    {
        spawning.ClearOccupiedSpawns();
        spawning.SpawnObject(cervo);
        spawning.SpawnObject(cacador);
    }
        
}
