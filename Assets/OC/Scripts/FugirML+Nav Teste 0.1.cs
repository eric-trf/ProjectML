/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.AI;
using System.Linq;
using System.Runtime.CompilerServices;
using System;
//using System.Numerics;

public class FugirMLNav : Agent
{
    [SerializeField] private GameObject player;
    GameObject[] obstacles;
    Spawn spawn;
    Rigidbody rb;
    NavMeshAgent agent;
    Vector3 startPosition;
    Vector3 playerStartPosition;
    Vector3 moveDirection;
    public float hearingRange;
    float previousDistanceToPlayer;
    float[] previousDistanceToObstacle;

    public override void Initialize() 
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        //startPosition = transform.position;
        spawn = player.GetComponent<Spawn>();
        spawn.GetSpawnPos();
        obstacles = GameObject.FindGameObjectsWithTag("Wall");
    }

    public override void OnEpisodeBegin()
    {
        agent.enabled = false;
        //transform.position = startPosition;
        transform.localPosition = new Vector3(UnityEngine.Random.Range(-40f, 40f), 1f, UnityEngine.Random.Range(-40f, 40f));
        agent.enabled = true;
        spawn.GoToSpawn();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(player.transform.localPosition);

        var sortedObstacles = obstacles.OrderBy(obstacle => Vector3.Distance(transform.localPosition, obstacle.transform.localPosition)).Take(2);
        
        foreach (GameObject obstacle in sortedObstacles)
        {
            Vector3 relativePosition = obstacle.transform.localPosition - agent.transform.localPosition;
            sensor.AddObservation(relativePosition);

            float distanceToObstacle = Vector3.Distance(obstacle.transform.localPosition, agent.transform.localPosition);
            if (distanceToObstacle <= 10f)
            {
                SetReward(Math.Max(-10f/distanceToObstacle, -10f));
            }
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //if (Vector3.Distance(playerTransform.localPosition, transform.localPosition) < hearingRange)
        float horizontalMovement = actions.ContinuousActions[0];
        float verticalMovement = actions.ContinuousActions[1];
        moveDirection = new UnityEngine.Vector3(horizontalMovement, 0, verticalMovement);
        agent.SetDestination(transform.position+moveDirection*3);

        float distanceToPlayer = Vector3.Distance(transform.localPosition, player.transform.localPosition);

        // Atribua recompensas com base na distância
        if (distanceToPlayer > previousDistanceToPlayer)
        {
            SetReward(1f); // Recompensa por se afastar
        }
        else if (distanceToPlayer <= previousDistanceToPlayer)
        {
            SetReward(-1f); // Penalidade por se aproximar
        }

        if (distanceToPlayer > 5f)
        {
            SetReward(0.1f);
        }

        previousDistanceToPlayer = distanceToPlayer; // Atualize a distância anterior
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Debug.Log("inimigo contactado");
            SetReward(-100f); // Penalidade por ser pego
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            Debug.Log("parede contactada");
            SetReward(-100f); // Penalidade por trombar em paredes
            //EndEpisode();
        }
    }
}
*/