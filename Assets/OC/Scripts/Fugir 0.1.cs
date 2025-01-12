/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fugir : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    NavMeshAgent agent;
    
    public float hearingRange;
    public float minTurn;
    float timer = -99; 
    int count = 1;
    bool turnLeft = false;
    bool dirBlocked = false;
    bool pauseNewPos = false;

    Vector3 dirAway;
    Vector3 newPos;
    Vector3 possiblePos;

    void Start ()
    {
        agent = GetComponent <NavMeshAgent>();
        turnLeft = System.Convert.ToBoolean(Random.Range(0,2)); //randomizes first turn
    }

    void Update ()
    {
        if (!pauseNewPos)
        {
            if (Vector3.Distance(playerTransform.position, transform.position) < hearingRange)
            {
                dirAway = transform.position - playerTransform.position;
                newPos = transform.position + dirAway;

                if(Physics.Raycast(transform.position, newPos, out RaycastHit hit, hearingRange)) 
                {
                    if (hit.transform.CompareTag("Wall")) 
                        dirBlocked = true;

                    else 
                        dirBlocked = false;
                }
                else
                    dirBlocked = false;

                if (dirBlocked && count == 1) //randomize next turn between right or left
                    turnLeft = System.Convert.ToBoolean(Random.Range(0,2));
                    Debug.Log(turnLeft);

                if (dirBlocked)
                {
                    possiblePos = Quaternion.Euler(0,minTurn*count,0)*newPos;
                    if(Physics.Raycast(transform.position, possiblePos, out RaycastHit hit2, hearingRange))
                    {
                        if (!hit2.transform.CompareTag("Wall")) 
                        {
                            dirBlocked = false;
                            count = 1;
                            newPos = possiblePos;
                            pauseNewPos = true;
                        }
                        else 
                            count += 1;
                    }
                    else 
                        count += 1;
                }

                agent.SetDestination(newPos);
            }

            else //outside hearingRange, stop moving
            {
                agent.SetDestination(transform.position);
            }
        }

        if (pauseNewPos)
        {
            if (timer == -99) //timer not running, bad coding and temporary fix
            {
                timer = 1f;
            }

            else if (timer > 0)
            {
                timer -= Time.deltaTime;
            }

            else if (timer < 0)
            {
                timer = -99;
                pauseNewPos = false;
            }
        }
    }
}
*/