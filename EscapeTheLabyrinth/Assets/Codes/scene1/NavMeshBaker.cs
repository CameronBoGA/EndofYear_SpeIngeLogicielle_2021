using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    [SerializeField] NavMeshSurface[] navMeshSurfaces;

    private bool navBuilded = false;

    public bool getNavBuilded()
    {
        return navBuilded;
    }

    // Update is called once per frame
    void Update()
    {   
        for(int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
        if(!navBuilded) navBuilded = true;
    }
}