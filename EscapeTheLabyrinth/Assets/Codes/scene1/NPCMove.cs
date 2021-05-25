using System.Diagnostics;
using System.Security.AccessControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;

    private double speedAgent = 5;
    private UnityEngine.Vector3 startPoint;
    private UnityEngine.Vector3 finishPoint;
    private DateTime startTime;
    private bool failedStatus = false;
    private bool flag = true;

    // Cas ou l'IA n'arrive pas a atteindre l'arrivé
    public void setFailedStatus(bool newStatus)
    {
        failedStatus = newStatus;
    }

    // Retourne l'heure de depart de l'IA
    public DateTime getStartTime()
    {
        return startTime;
    }

    public NavMeshAgent getAgent()
    { 
        return agent;
    }

    void Awake()
    {
        // Set la vitesse de l'Agent
        agent.speed = (float) speedAgent;
        finishPoint = GameObject.FindGameObjectWithTag("Stop").transform.position;

        // definis le point de depart
        startPoint = GameObject.Find("Start").transform.position;
        // teleportation de l'agent sur le point de depart
        agent.Warp(startPoint);
        //UnityEngine.Debug.Log("Teleport");
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
                    // Initialisation du temps de depart
                    if(flag)
                    {
                        startTime = DateTime.Now;
                        //UnityEngine.Debug.Log(startTime);
                        flag = false;
                    }

                    // Deplacement de l'IA
                    agent.SetDestination(finishPoint); 
                    
                    // WIP | test si l'agent se deplace encore
                    // Si ce n'est pas le cas on set le failedStatus a false
                    if (!agent.pathPending)
                    {
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                            {
                                // Test | Si l'IA ne se deplace plus, on recupere un temps qui vaut 0
                                TimeSpan res = GameObject.
                                                FindGameObjectWithTag("Stop").
                                                GetComponent<FinishRun>().
                                                getResultTime();
                                
                                //UnityEngine.Debug.Log("Stopped");
                            }
                        } // END if (agent.remainingDistance <= agent.stoppingDistance)
                    } // END if (!agent.pathPending)
               }
            //Debug.Log(failedStatus);
        }else {
            // donne a l'IA une vitesse a 0 si le faildedStatus est true
            agent.velocity = Vector3.zero;
            //Debug.Log(failedStatus);
        }
    }

}