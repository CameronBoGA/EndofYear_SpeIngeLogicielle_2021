using System.Numerics;
using System;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    public NavMeshAgent agent;
    UnityEngine.Vector3 finishPoint;
    private bool debug = false;

    void Awake()
    {
        
        finishPoint = GameObject.FindGameObjectWithTag("Stop").transform.position;
        if(debug == true) Debug.Log("AWAKE");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(debug == true) Debug.Log("START");
    }
    

    // Update is called once per frame
    void Update()
    {
        
        if(debug == true) Debug.Log(finishPoint);
        agent.SetDestination(finishPoint);
        if(debug == true) Debug.Log("UPDATE");
    }
}