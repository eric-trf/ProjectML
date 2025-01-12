using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.AI;

public class Animação : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    float IDAnim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //muda a animação de movimento
        if (!agent.hasPath) IDAnim = 0.0f;
        else if (agent.hasPath) IDAnim = 0.5f;
        anim.SetFloat("IDAnim", IDAnim);
        Debug.Log(IDAnim+", "+anim.GetFloat("IDAnim"));
    }   
}
