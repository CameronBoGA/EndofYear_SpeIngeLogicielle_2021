using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class apiHandler : MonoBehaviour
{
    /* URL for maps*/
    private readonly string baseURL = "http://127.0.0.1:8080/api/maps";
    
    /*Row retrieved from Database*/
    private string[] rows;

    /* Level manipulation for Json */
    private LevelEditor level;
    public bool playerPlaced = false;
    public GameObject wall;
    public GameObject player;
    private string mapDB;

    /* Misc Animations and UI */
    public bool saveLoadMenuOpen = false;
    public Animator saveUIAnimation;
    public Animator loadUIAnimation;
    public Text levelMessage;
    public InputField levelNameSave;
    public InputField creatorNameSave;
    public InputField levelNameLoad;
    private bool saveLoadPositionIn = false;
    public Animator messageAnim;

    private void Start()
    {
        CreateEditor();
    }

    LevelEditor CreateEditor()
    {
        level = new LevelEditor();
        level.editorObjects = new List<EditorObject.Data>();
        return level;
    }

    public void SaveLevel()
    {
        /* Init */
        string creatorName;
    
        /*Creating a json with all objects defined in Editor Object*/
        EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
        foreach (EditorObject obj in foundObjects)
            level.editorObjects.Add(obj.data);
        string json = JsonUtility.ToJson(level);
        string folder = Application.dataPath + "/LevelData/";
        string leveltitle = "";

        /* Setting the Name of The map*/
        if (levelNameSave.text == "")
            leveltitle = "new_level.json";
        else
            leveltitle = levelNameSave.text + ".json";
        
        /* Setting the Name of The creator*/
        if (creatorNameSave.text == "") {
            creatorName = "Bob";
            levelMessage.text = "As your name is empty it has been set to Bob";
        } else
            creatorName = creatorNameSave.text;

        /* Animation for Panel Handling */
        saveUIAnimation.SetTrigger("SaveLoadOut");
        saveLoadPositionIn = false;
        saveLoadMenuOpen = false;
        
        /* Resetting Variables */
        levelNameSave.text = "";
        levelNameSave.DeactivateInputField();
        
        /* Final Message of interaction and animation handling */
        messageAnim.Play("MessageFade", 0, 0);
        StartCoroutine(saveMapOnDB(leveltitle, creatorName, json));
    }

    IEnumerator saveMapOnDB(string leveltitle, string creatorName, string jsonString)
    {
        //string jsonString = File.ReadAllText(Application.dataPath+"/LevelData/Demo.json");
        
        /* Adding rows to form which will be sent to DB */
        WWWForm formdata = new WWWForm();
        formdata.AddField("title", leveltitle);
        formdata.AddField("creator", creatorName);
        formdata.AddField("map_layout", jsonString);
        formdata.AddField("result", "No attempt as of yet");

        UnityWebRequest www = UnityWebRequest.Post(baseURL, formdata);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            levelMessage.text = "Map upload complete!";
        }
    }

    IEnumerator loadMapOnDB(string levelname)
    {

        /* Must find a specific map on db */

        using (UnityWebRequest mapInfoRequest = UnityWebRequest.Get(baseURL))
        {
            yield return mapInfoRequest.SendWebRequest();
            if (mapInfoRequest.result == UnityWebRequest.Result.ConnectionError) // Error
            {
                levelMessage.text = levelname + " could not be found!";
                loadUIAnimation.SetTrigger("SaveLoadOut");
                saveLoadPositionIn = false;
                saveLoadMenuOpen = false;
                messageAnim.Play("MessageFade", 0, 0);
                levelNameLoad.DeactivateInputField();
                Debug.LogError(mapInfoRequest.error);
                yield break;
            }
            else // Success
            {
                Debug.Log("Received: " + mapInfoRequest.downloadHandler.text);
                mapDB = mapInfoRequest.downloadHandler.text;
                /*string fulldata = mapInfoRequest.downloadHandler.text;
                rows = fulldata.Split(new string[] { "<br>" }, StringSplitOptions.None);
                Debug.Log(String.Format("There are {0} comments.", rows.Length));
                foreach (string row in rows)
                {
                    Debug.Log(row);
                }*/
            }
        }
    }
    public void LoadLevel()
    {
        
        /* Retrieving the Level Name */
        string levelFile = "";
        if (levelNameLoad.text == "")
            levelFile = "new_level.json";
        else
            levelFile = levelNameLoad.text + ".json";
        
        /* Calling Function for db management */
        StartCoroutine(loadMapOnDB(levelFile));

        /* If the level is found, it will proceed down, if not if would have been stoppped in coroutine */
        EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
        foreach (EditorObject obj in foundObjects)
        Destroy(obj.gameObject);
        playerPlaced = false;
        
        /* Fill the map */
        CreateFromString();
    }

    void CreateFromString()
    {
        /* Initialisation */
        GameObject newObj;

        for (int i = 0; i < level.editorObjects.Count; i++)
        {
            if (level.editorObjects[i].objectType == EditorObject.ObjectType.Wall)
                {
                    newObj = Instantiate(wall, transform.position, Quaternion.identity);
                    newObj.transform.position = level.editorObjects[i].pos;
                    newObj.layer = 9;
                    EditorObject eo = newObj.AddComponent<EditorObject>();
                    eo.data.pos = newObj.transform.position;
                    eo.data.objectType = EditorObject.ObjectType.Wall;
                }
            else if (level.editorObjects[i].objectType == EditorObject.ObjectType.Player)
                {
                    newObj = Instantiate(player, transform.position, Quaternion.identity);
                    newObj.layer = 9;
                    newObj.transform.position = level.editorObjects[i].pos;
                    playerPlaced = true;
                    EditorObject eo = newObj.AddComponent<EditorObject>();
                    eo.data.pos = newObj.transform.position;
                    eo.data.objectType = EditorObject.ObjectType.Player;
                }
            }
        
        /*Misc Animation and UI Handling*/
        levelNameLoad.text = "";
        levelNameLoad.DeactivateInputField();
        loadUIAnimation.SetTrigger("SaveLoadOut");
        saveLoadPositionIn = false;
        saveLoadMenuOpen = false;
        levelMessage.text = "Level loading...done.";
        messageAnim.Play("MessageFade", 0, 0);
    }
}

