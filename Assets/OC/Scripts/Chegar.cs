using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chegar : MonoBehaviour
{
    [SerializeField] private Transform goalTransform;
    NavMeshAgent agent;
    Vector3 dirTowards;
    public float decisionPause;
    float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
        timer += Time.deltaTime;
        float currentDistance = Vector3.Distance(transform.localPosition, goalTransform.localPosition);

        if (currentDistance > 11f)
        {
            if (timer > decisionPause)
            {
                dirTowards = goalTransform.localPosition - transform.localPosition;
                dirTowards = Vector3.Normalize(dirTowards);
                agent.SetDestination(transform.position+dirTowards*5);
                timer = 0f;
            }
        }

        if (currentDistance <= 11f)
        {
            agent.SetDestination(goalTransform.position);
        }
        
    }
}