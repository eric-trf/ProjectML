/* obsloleto
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
//using System.Numerics;

public class FugirML : Agent
{
    [SerializeField] private Transform playerTransform;
    Rigidbody rb;
    Vector3 startPosition;
    Vector3 playerStartPosition;
    Vector3 moveDirection;
    public float hearingRange;
    public float speed;

    public override void Initialize() 
    {
        //rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        Debug.Log("deer start position: "+startPosition);
        playerStartPosition = playerTransform.position;
        Debug.Log("player start position: "+playerStartPosition);
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode begun");
        transform.position = startPosition;
        playerTransform.position = playerStartPosition;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(playerTransform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //if (Vector3.Distance(playerTransform.position, transform.position) < hearingRange)
            float horizontalMovement = actions.ContinuousActions[0];
            float verticalMovement = actions.ContinuousActions[1];
            moveDirection.x = horizontalMovement;
            moveDirection.y = 0f;
            moveDirection.z = verticalMovement;
            //Vector3.Normalize(moveDirection);
            rb.MovePosition(transform.position + speed * Time.deltaTime * moveDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            SetReward(-1f); 
            Debug.Log("inimigo contactado");
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-0.2f);
            Debug.Log("parede contactada");
        }
    }
}
*/