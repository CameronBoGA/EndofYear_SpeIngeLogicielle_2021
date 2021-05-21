using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ralentis l'IA si elle touche la zone lente
public class SlowAreaHandler : MonoBehaviour
{
    public NavMeshAgent agent;

    public double lowSpeed = 2;

    private double speedAgent = 5;

    void OnTriggerEnter(Collider other) 
    {
        // Change la vitesse de l'IA
        agent.speed = (float) lowSpeed;

    }
 
   private void OnTriggerExit(Collider other)
   {
       agent.speed = (float) speedAgent;

   }
}