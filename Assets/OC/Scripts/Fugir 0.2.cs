using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fugir : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    NavMeshAgent agent;
    
    public float wallSafeDistance; //distancia de detecção parede a frente
    public float hearingRange; //distancia de reação ao caçador
    public float minTurn; //incremento em graus do calculo da curva
    int count = 1; //conta incrementos no calculo da curva
    float timer = -99; //tempo entre decisões

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
        //se o calculo de posiçao estiver pausado, conta o tempo e retorna
        if (pauseNewPos)
        {
            if (timer == -99) //timer not running
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
            return;
        }    


        //se o jogador estiver fora da hearing range, para de mover e retorna
        if (Vector3.Distance(playerTransform.position, transform.position) > hearingRange)
        {
            agent.ResetPath();
            return;
        }

        //calculo do movimento
        dirAway = transform.position - playerTransform.position;
        newPos = transform.position + dirAway;

        //testa se o caminho escolhido está livre
        if(Physics.Raycast(transform.position, newPos, out RaycastHit hit, wallSafeDistance)) 
        {
            if (hit.transform.CompareTag("Wall")) dirBlocked = true;

            else 
            {
                dirBlocked = false;
                agent.SetDestination(newPos);
                return; //se o caminho não está obstruido por um obstáculo, não é necessario calcular curva
            }
        }

        else 
        {
            dirBlocked = false;
            agent.SetDestination(newPos);
            return; //se o caminho está livre, não é necessario calcular curva
        }

        //calculo da curva
        //escolhe aleatoriamente se a próxima curva iniciara o calculo para direita ou esquerda
        if (dirBlocked && count == 1) turnLeft = System.Convert.ToBoolean(Random.Range(0,2));

        //calcula a curva
        if (!turnLeft) possiblePos = Quaternion.Euler(0,minTurn*count,0)*newPos;
        if (turnLeft) possiblePos = Quaternion.Euler(0,-minTurn*count,0)*newPos;
        
        //testa se o possível caminho esta obstruido
        if(Physics.Raycast(transform.position, possiblePos, out RaycastHit hit2, wallSafeDistance))
        {
            if (!hit2.transform.CompareTag("Wall")) //se não estiver obstruido por um obstaculo, está limpo
            {
                CurvaFinalizada();
            }

            else 
                count += 1; //aumenta a contagem e no proximo frame tentara uma curva mais fechada
        }

        else CurvaFinalizada();

        agent.SetDestination(newPos); //se não estiver obstruido, está limpo
    }

    void CurvaFinalizada()
    {
        dirBlocked = false; //proximo update nao calculará mais a curva
        count = 1; //reseta a contagem de iterações para o próx calc de curva
        newPos = possiblePos; //destino é a posicao calculada
        pauseNewPos = true; //pausa calculo de proxima rota para que personagem não ande 1 frame e pare para calcular outra curva
    }   
}

/*problemas conhecidos:
1)
caso o chão a frente do personagem seja uma subida ingrime, ele não vai reconhecer um obstáculo 
a frente um pouco acima dele, devido o raio acertar o chão
- possível correção: jogar um leque de raios partindo de si e em angulos verticais diferentes
em cada iteração

2)
caso o personagem seja muito rápido, após uma curva, ele pode chegar ao seu destino e o timer
ainda estar rodando, resultando no personagem ficar parado
- possível correção: checar se o personagem já chegou em seu destino com
if (agent.remainingDistance <= agent.stoppingDistance)
e zerar o timer caso sim
*/

