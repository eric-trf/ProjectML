/* obsoleto
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour
{
    Rigidbody rb;
    Vector3 spawnPos;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void GetSpawnPos ()
    {
        //spawnPos = transform.position;
    }

    public void GoToSpawn ()
    {
        rb.Sleep();
        GetComponent<NavMeshAgent>().enabled = false;
        //transform.position = spawnPos;
        transform.localPosition = new Vector3(UnityEngine.Random.Range(-40f, 40f), 1f, UnityEngine.Random.Range(-40f, 40f));
        GetComponent<NavMeshAgent>().enabled = true;
        rb.WakeUp();
    }
}
*/