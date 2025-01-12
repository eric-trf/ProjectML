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

public class FugirMLNav : Agent
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnList;
    GameObject[] obstacles;
    Rigidbody rb;
    NavMeshAgent agent;
    Vector3 moveDirection;
    SoundManager soundManager;

    public float hearingRange;
    float distanceToPlayer;
    float previousDistanceToPlayer;
    bool wallNearby;

    public override void Initialize() 
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        obstacles = GameObject.FindGameObjectsWithTag("Wall");
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(player.transform.localPosition);

        var sortedObstacles = obstacles.OrderBy(obstacle => Vector3.Distance(transform.localPosition, obstacle.transform.localPosition)).Take(2);
        wallNearby = false;

        foreach (GameObject obstacle in sortedObstacles)
        {
            Vector3 relativePosition = obstacle.transform.localPosition - agent.transform.localPosition;
            sensor.AddObservation(relativePosition);

            float distanceToObstacle = Vector3.Distance(obstacle.transform.localPosition, agent.transform.localPosition);
            if (distanceToObstacle <= 2f)
            {
                SetReward(Math.Max(-10f/distanceToObstacle, -10f));
                wallNearby = true;
            }
        }
        
        distanceToPlayer = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if (distanceToPlayer < hearingRange || wallNearby)
        {
            RequestDecision();
        }
    }

    public void Update ()
    {
        distanceToPlayer = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if ((distanceToPlayer < hearingRange) || wallNearby)
        {
            RequestDecision();
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //if (Vector3.Distance(playerTransform.localPosition, transform.localPosition) < hearingRange)
        float horizontalMovement = actions.ContinuousActions[0];
        float verticalMovement = actions.ContinuousActions[1];
        moveDirection = new UnityEngine.Vector3(horizontalMovement, 0, verticalMovement);
        agent.SetDestination(transform.position+moveDirection*3);

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
        if (other.CompareTag("Cacador"))
        {
            SetReward(-100f); // Penalidade por ser pego
            //EndEpisode();
        }

        if (other.CompareTag("Wall"))
        {
            SetReward(-100f); // Penalidade por trombar em paredes
            //EndEpisode();
        }
    }
}
