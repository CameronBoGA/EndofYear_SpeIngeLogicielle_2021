using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Bake le NavMesh automatiquement
public class NavMeshBaker : MonoBehaviour
{
    //[SerializeField]
    public NavMeshSurface[] navMeshSurfaces;

    private bool navBuilded = false;

    // retourne true si le NavMesh a été généré
    public bool getNavBuilded()
    {
        return navBuilded;
    }

    // Update is called once per frame
    void Update()
    {   
        // génére le NavMesh pour n element (seul le floor suffit)
        for(int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
        if(!navBuilded) navBuilded = true;
    }
}