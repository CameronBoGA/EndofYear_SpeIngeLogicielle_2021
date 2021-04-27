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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        onTrigger.Invoke();

        if(destroyAfterTrigger){
            Destroy(gameObject);
        }
    }
}
