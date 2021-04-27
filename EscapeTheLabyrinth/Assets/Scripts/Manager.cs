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
    [HideInInspector]
    public bool saveLoadMenuOpen = false;
    public Animator saveUIAnimation;
    public Animator loadUIAnimation;
    public InputField levelNameSave;
    public InputField levelNameLoad;
    public Text levelMessage;
    public Animator messageAnim;
    private bool saveLoadPositionIn = false;
    private LevelEditor level;
    // Testing
    public CubePlacer user;

    void Start()
    {
        CreateEditor();
    }

     LevelEditor CreateEditor()
    {
        level = new LevelEditor();
        level.editorObjects = new List<EditorObject.Data>();
        return level;
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
        Debug.Log("Inside Choose Load");
        if (saveLoadPositionIn == false)
        {
            Debug.Log("Pannel is not on screen");
            loadUIAnimation.SetTrigger("SaveLoadIn");
            saveLoadPositionIn = true;
            saveLoadMenuOpen = true;
        }
        else
        {
            Debug.Log("Pannel is true");
            loadUIAnimation.SetTrigger("SaveLoadOut");
            saveLoadPositionIn = false;
            saveLoadMenuOpen = false;
        }
    }

   public void SaveLevel()
    {
        EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
        foreach (EditorObject obj in foundObjects)
            level.editorObjects.Add(obj.data);
        
        string json = JsonUtility.ToJson(level);
        string folder = Application.dataPath + "/LevelData/";
        string levelFile = "";

        if (levelNameSave.text == "")
            levelFile = "new_level.json";
        else
            levelFile = levelNameSave.text + ".json";
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        string path = Path.Combine(folder, levelFile);

        if (File.Exists(path))
            File.Delete(path);
        File.WriteAllText(path, json); 
        saveUIAnimation.SetTrigger("SaveLoadOut");
        saveLoadPositionIn = false;
        saveLoadMenuOpen = false;
        levelNameSave.text = "";
        levelNameSave.DeactivateInputField();
        levelMessage.text = levelFile + " saved to LevelData folder.";
        messageAnim.Play("MessageFade", 0, 0);
    }

    public void LoadLevel()
    {
        string folder = Application.dataPath + "/LevelData/";
        string levelFile = "";
        if (levelNameLoad.text == "")
            levelFile = "new_level.json";
        else
            levelFile = levelNameLoad.text + ".json";
        
        string path = Path.Combine(folder, levelFile);

        if (File.Exists(path))
        {
            Debug.Log("Vadim, level found");
            EditorObject[] foundObjects = 
                    FindObjectsOfType<EditorObject>();
            foreach (EditorObject obj in foundObjects)
            Destroy(obj.gameObject);
            playerPlaced = false;
            
            string json = File.ReadAllText(path);
            level = JsonUtility.FromJson<LevelEditor>(json);
            CreateFromFile();
        }
        else
        {
            Debug.Log("Blyat, error 404");
            loadUIAnimation.SetTrigger("SaveLoadOut");
            saveLoadPositionIn = false;
            saveLoadMenuOpen = false;
            levelMessage.text = levelFile + " could not be found!";
            messageAnim.Play("MessageFade", 0, 0);
            levelNameLoad.DeactivateInputField();
        }
    }

    void CreateFromFile()
    {
        GameObject newObj;
        for (int i = 0; i < level.editorObjects.Count; i++)
        {
            if (level.editorObjects[i].objectType == EditorObject.ObjectType.Wall)
                {
                    newObj = Instantiate(wall, 
                                    transform.position, Quaternion.identity);
                    newObj.transform.position = level.editorObjects[i].pos;
                    //newObj.transform.rotation = level.editorObjects[i].rot;
                    newObj.layer = 9;
                    EditorObject eo = newObj.AddComponent<EditorObject>();
                    eo.data.pos = newObj.transform.position;
                    //eo.data.rot = newObj.transform.rotation;
                    eo.data.objectType = EditorObject.ObjectType.Wall;
                }
            else if (level.editorObjects[i].objectType == EditorObject.ObjectType.Player)
                {
                    newObj = Instantiate(player, 
                                    transform.position, Quaternion.identity);
                    newObj.layer = 9;
                    newObj.transform.position = level.editorObjects[i].pos;
                    //newObj.transform.rotation = level.editorObjects[i].rot;
                    playerPlaced = true;
                    EditorObject eo = newObj.AddComponent<EditorObject>();
                    eo.data.pos = newObj.transform.position;
                    //eo.data.rot = newObj.transform.rotation;
                    eo.data.objectType = EditorObject.ObjectType.Player;
                }
            }
        levelNameLoad.text = "";
        levelNameLoad.DeactivateInputField();
        
        loadUIAnimation.SetTrigger("SaveLoadOut");
        saveLoadPositionIn = false;
        saveLoadMenuOpen = false;
        
        levelMessage.text = "Level loading...done.";
        messageAnim.Play("MessageFade", 0, 0);
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
    
    public void ChooseLaunchAi()
    {
        Debug.Log("AI Lauched, Terminator Programm Initiated");
    }
}
