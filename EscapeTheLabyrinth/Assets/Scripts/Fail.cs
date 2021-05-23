using System;
using UnityEngine;
using UnityEngine.AI;

public class Fail : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GameObject.Find("Agent").GetComponent<NPCMove>().getAgent();
    }

    void OnTriggerEnter(Collider other) 
    {
        agent.GetComponent<NPCMove>().setFailedStatus(true);

        TimeSpan res = GameObject.
                        FindGameObjectWithTag("Stop").
                        GetComponent<FinishRun>().
                        getResultTime();
                                
        UnityEngine.Debug.Log(res);
    }
}
