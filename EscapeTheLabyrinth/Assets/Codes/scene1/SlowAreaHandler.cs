using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowAreaHandler : MonoBehaviour
{
    public NavMeshAgent agent;

    public double lowSpeed = 2;

    private double speedAgent = 5;

    void OnTriggerEnter(Collider other) 
    {
        agent.speed = (float) lowSpeed;

    }
 
   private void OnTriggerExit(Collider other)
   {
       agent.speed = (float) speedAgent;

   }
}