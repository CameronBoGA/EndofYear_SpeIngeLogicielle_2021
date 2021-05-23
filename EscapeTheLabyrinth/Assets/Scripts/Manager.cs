using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [HideInInspector]
    public bool playerPlaced = false;
    public GameObject wall;
    public GameObject player;
    public GameObject mud;
    public GameObject caltrop;
    public GameObject StartPT;
    public GameObject EndPT; 
    public CubePlacer user;
    

     /* User Interface */
    [HideInInspector]
    public bool saveLoadMenuOpen = false;
    public Animator saveUIAnimation;
    public Animator loadUIAnimation;
    public InputField levelNameSave;
    public InputField creatorNameSave;
    public InputField levelNameLoad;
    public Animator messageAnim;
    public bool saveLoadPositionIn = false;
    public Text levelMessage;

    void Start()
    {
        //Manager _manager = Singleton._instance.manager;
    }

    public void ChooseSave()
    {
        if (saveLoadPositionIn == false)
        {
            saveUIAnimation.SetTrigger("SaveLoadIn");
            saveLoadPositionIn = true;
            saveLoadMenuOpen = true;
        }

        else
        {
            saveUIAnimation.SetTrigger("SaveLoadOut");
            saveLoadPositionIn = false;
            saveLoadMenuOpen = false;
        }
    }
    
    public void ChooseLoad()
    {
        if (saveLoadPositionIn == false)
        {
            loadUIAnimation.SetTrigger("SaveLoadIn");
            saveLoadPositionIn = true;
            saveLoadMenuOpen = true;
        }

        else
        {
            loadUIAnimation.SetTrigger("SaveLoadOut");
            saveLoadPositionIn = false;
            saveLoadMenuOpen = false;
        }
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
    }

    public void ChooseDestroy()
    {
        user.manipulateOption = CubePlacer.LevelManipulation.Destroy;
        //user.mr.enabled = false;
    }
    public void ChooseEnd()
    {
        user.itemOption = CubePlacer.ItemList.End;
        //CubePlacer.mesh = player.GetComponent<MeshFilter>().mesh;
    }
    public void ChooseMud()
    {
        user.itemOption = CubePlacer.ItemList.Mud;
        //CubePlacer.mesh = player.GetComponent<MeshFilter>().mesh;
    }
    public void ChooseCaltrop()
    {
        user.itemOption = CubePlacer.ItemList.Caltrop;
        //CubePlacer.mesh = player.GetComponent<MeshFilter>().mesh;
    }
}
