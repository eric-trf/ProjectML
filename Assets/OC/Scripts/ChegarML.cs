using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
//using System.Numerics;

public class ChegarML : Agent
{
    [SerializeField] private Transform goalTransform;
    public float speed;
    Rigidbody rb;
    Vector3 startPosition;
    Vector3 moveDirection;

    void Start () 
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        //transform.position = startPosition; manter comentado enquanto script do cervo já seta esta posição
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(goalTransform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float horizontalMovement = actions.ContinuousActions[0];
        float verticalMovement = actions.ContinuousActions[1];
        moveDirection = new UnityEngine.Vector3(horizontalMovement, 0f, verticalMovement);
        //Vector3.Normalize(moveDirection);
        rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(1f); 
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-0.2f);
            EndEpisode();
        }
    }
}
