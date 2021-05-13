//using System.Diagnostics;
//using System.Collections;
//using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class FailSimulation : MonoBehaviour
{
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) 
    {
        agent.GetComponent<NPCMove>().setFailedStatus(true);
        UnityEngine.Debug.Log("Fail");

        TimeSpan res = GameObject.
                        FindGameObjectWithTag("Stop").
                        GetComponent<FinishRun>().
                        getResultTime();
                                
        UnityEngine.Debug.Log(res);
    }
}
