using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [HideInInspector]
    public bool playerPlaced;
    public GameObject wall;
    public GameObject player;

    // Tstingg
    public CubePlacer user;

    void Start()
    {
        playerPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseWall()
    {
        user.itemOption = CubePlacer.ItemList.Wall;
        //CubePlacer.mesh = wall.GetComponent<MeshFilter>().mesh;
    }
    
    public void ChoosePlayer()
    {
        user.itemOption = CubePlacer.ItemList.Player;
        //CubePlacer.mesh = player.GetComponent<MeshFilter>().mesh;
    }

    public void ChooseCreate()
    {
        user.manipulateOption = CubePlacer.LevelManipulation.Create;
        //user.mr.enabled = true;
        //rotUI.SetActive(false);     
    }
    public void ChooseDestroy()
    {
        user.manipulateOption = CubePlacer.LevelManipulation.Destroy;
        //user.mr.enabled = false;
        //rotUI.SetActive(false);
    }
}
