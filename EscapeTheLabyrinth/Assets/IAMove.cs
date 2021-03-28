using System.Numerics;
using System;
using UnityEngine;
using UnityEngine.AI;

public class IAMove : MonoBehaviour
{
    public NavMeshAgent agent;

    UnityEngine.Vector3 startPoint;
    UnityEngine.Vector3 finishPoint;

    // Start is called before the first frame update
    void Start()
    {
        startPoint  = GameObject.Find("/Objective/Start").transform.position;
        finishPoint = GameObject.FindGameObjectWithTag("Stop").transform.position;
    }
    

    // Update is called once per frame
    void Update()
    {
        Debug.Log("START" + startPoint);
        Debug.Log(finishPoint);
        agent.SetDestination(finishPoint);  
    }
}