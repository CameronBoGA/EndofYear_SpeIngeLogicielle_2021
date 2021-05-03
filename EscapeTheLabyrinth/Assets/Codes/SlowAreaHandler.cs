using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowAreaHandler : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    int slowSpeed = 2;


    private bool debug = true;
    
    void OnTriggerEnter(Collider other) 
    {
        agent.speed = 2;

        if(debug == true) Debug.Log ("Inside " + other.gameObject.name);
    }
 
   private void OnTriggerExit(Collider other)
   {
       if(gameObject.tag != "Caltrop") agent.speed = 5;

       if(debug == true) Debug.Log ("Outside " + other.gameObject.name);
   }

}
