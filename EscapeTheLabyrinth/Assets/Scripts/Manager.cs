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
    
    public void ChooseLaunchAi()
    {
        Debug.Log("AI Lauched, Terminator Programm Initiated");
    }
}
