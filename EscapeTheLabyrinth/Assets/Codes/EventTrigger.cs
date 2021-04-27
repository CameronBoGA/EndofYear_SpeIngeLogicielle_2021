using System.Numerics;
using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]

public class EventTrigger : MonoBehaviour
{
    public UnityEvent onTrigger;
    public bool destroyAfterTrigger = false;

    void Awake()
    {
        if(onTrigger == null){
            onTrigger = new UnityEvent();
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Je t'aime alexys <3");
    }

    void OnTriggerEnter(Collider other)
    {
        onTrigger.Invoke();

        if(destroyAfterTrigger){
            Destroy(gameObject);
        }
    }
}
