using System.Diagnostics;
using System.Security.AccessControl;
using System;
//using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    private double speedAgent = 5;
    public UnityEngine.AI.NavMeshAgent agent;
    private UnityEngine.Vector3 finishPoint;
    private DateTime startTime;
    private bool failedStatus = false;
    private bool flag = true;

    

    public void setFailedStatus(bool newStatus)
    {
        failedStatus = newStatus;
    }

    public DateTime getStartTime()
    {
        return startTime;
    }

    void Awake()
    {
        agent.speed = (float) speedAgent;
        finishPoint = GameObject.FindGameObjectWithTag("Stop").transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {   
        // Si le failedStatus est true, l'IA ne se deplace pas
        if(!failedStatus)
        {
            if(GameObject.Find("NavMeshBaker").
               GetComponent<NavMeshBaker>().
               getNavBuilded()) 
               {
                    if(flag)
                    {
                        startTime = DateTime.Now;
                        flag = false;
                    }

                    agent.SetDestination(finishPoint); 
                    
                    if (!agent.pathPending)
                    {
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                            {
                                TimeSpan res = GameObject.
                                                FindGameObjectWithTag("Stop").
                                                GetComponent<FinishRun>().
                                                getResultTime();
                                
                                UnityEngine.Debug.Log(res);
                            }
                        } // END if (agent.remainingDistance <= agent.stoppingDistance)
                    } // END if (!agent.pathPending)
               }
            //Debug.Log(failedStatus);
        }else {
            agent.velocity = Vector3.zero;
            //Debug.Log(failedStatus);
        }
    }

}