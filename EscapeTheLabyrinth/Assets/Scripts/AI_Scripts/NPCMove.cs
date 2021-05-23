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
    public Manager prog;

    public void setFailedStatus(bool newStatus)
    {
        // Case if AI failed to go to EndPoint
        failedStatus = newStatus;
    }

    public NavMeshAgent getAgent()
    { 
        return agent;
    }
    public DateTime getStartTime()
    {
        //Get Start Time on AI
        return startTime;
    }
    
    // Update is called once per frame
    public void StartSkyNet()
    {
        //while(failedStatus != true) {
            agent.speed = (float) speedAgent;
            finishPoint = prog.EndPT.transform.position;
            startPoint = prog.StartPT.transform.position;
            //UnityEngine.Debug.Log("End pos:"+finishPoint);
            agent.Warp(startPoint);
            // IF true The AI failed
            if(!failedStatus)
            {
                if(GameObject.Find("NavMeshBaker").GetComponent<NavMeshBaker>().getNavBuilded()) 
                {
                        // Init Start Time //
                        if(flag)
                        {
                            startTime = DateTime.Now;
                            //UnityEngine.Debug.Log(startTime);
                            flag = false;
                        }

                        // AI Movement //
                        agent.SetDestination(finishPoint); 
                }
            }
            else {
                // Set AI speed to 0 if failed
                agent.velocity = Vector3.zero;
            }
        //}
    }
}