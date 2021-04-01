using System.Numerics;
using System;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    public NavMeshAgent agent;

    UnityEngine.Vector3 startPoint;
    UnityEngine.Vector3 finishPoint;

    void Awake()
    {
        startPoint  = GameObject.FindGameObjectWithTag("Start").transform.position;
        finishPoint = GameObject.FindGameObjectWithTag("Stop").transform.position;
        Debug.Log("AWAKE");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");
    }
    

    // Update is called once per frame
    void Update()
    {
        Debug.Log(startPoint);
        Debug.Log(finishPoint);
        agent.SetDestination(finishPoint);
        Debug.Log("UPDATE");
    }
}  