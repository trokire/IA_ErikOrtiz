using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAController : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    public Transform[] destinationPoints;
    public int destinationIndex = 0;

    void Start()
    {
        target = GameObject.Find("Target").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //si la distancia entre la IA y el target es menos de 5, sigue al target
        if(Vector3.Distance(transform.position, target.position) < 5f)
        {
            agent.destination = target.position;

            if(Vector3.Distance(transform.position, target.position) < 2f)
            {
                Debug.Log("Ataque");   
            }
        }
        else
        {
            agent.destination = destinationPoints[destinationIndex].position;

            if(Vector3.Distance(transform.position, destinationPoints[destinationIndex].position) < 0.5f)
            {
                //mientras el index sea inferior a la cantidad de puntos en el array pasa al siguiente punto
                if(destinationIndex < destinationPoints.Length - 1)
                {
                    destinationIndex++;
                }
                //si llegamos al maximo de puntos en el array nos pone el index a 0
                else
                {
                destinationIndex = 0;
                }
            }   
        }
    }
}