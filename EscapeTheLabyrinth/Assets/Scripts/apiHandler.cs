using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using System.Globalization;

public class apiHandler : MonoBehaviour
{
    /* URL for maps*/
    private readonly string baseURL = "http://127.0.0.1:8080/api/maps";
    /*Row retrieved from Database*/
    private string[] rows;
    /* Level manipulation for Json */
    private LevelEditor level;
    // Master class
    public Manager prog;
    
    //Editor
    public EditorObject eo;
    //private string mapDB;
    /*public enum ObjectType {Wall, Touret, Player, Swamp};
    
    private struct Desc
    {
        public Vector3 pos;
        public ObjectType objectType;
    }
    private Desc data;
    Desc[] newObj;*/
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

    /* Saving Level */
    public void SaveLevel()
    {
        /* Init */
        string creatorName =  "";
        string leveltitle = "";

        /*Creating a json with all objects defined in Editor Object*/
        EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
        foreach (EditorObject obj in foundObjects)
            level.editorObjects.Add(obj.data);
        string json = JsonUtility.ToJson(level);

        /* Setting the Name of The map*/
        if (prog.levelNameSave.text == "")
            leveltitle = "new_level.json";
        else
            leveltitle = prog.levelNameSave.text + ".json";
        
        /* Setting the Name of The creator */
        if (prog.creatorNameSave.text == "") {
            creatorName = "Bob";
            prog.levelMessage.text = "As your name is empty it has been set to Bob";
        } else
            creatorName = prog.creatorNameSave.text;

        /* Animation for Panel Handling */
        prog.saveUIAnimation.SetTrigger("SaveLoadOut");
        prog.saveLoadPositionIn = false;
        prog.saveLoadMenuOpen = false;

        /* Resetting Variables */
        prog.levelNameSave.text = "";
        prog.levelNameSave.DeactivateInputField();

        /* Final Message of interaction and animation handling */
        prog.messageAnim.Play("MessageFade", 0, 0);

        StartCoroutine(saveMapOnDB(leveltitle, creatorName, json));
    }

    IEnumerator saveMapOnDB(string leveltitle, string creatorName, string jsonString)
    {   
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
            prog.levelMessage.text = "Map upload complete!";
        }
        SaveLevelToLocal(leveltitle);
    }
    public void SaveLevelToLocal(string levelFile)
    {
        EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
        foreach (EditorObject obj in foundObjects)
            level.editorObjects.Add(obj.data);
        
        string json = JsonUtility.ToJson(level);
        string folder = Application.dataPath + "/Imports/";
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        string path = Path.Combine(folder, levelFile);
        if (File.Exists(path))
	        File.Delete(path);
        File.WriteAllText(path, json);
    }

    public void LoadLevel()
    {
        /* Retrieving the Level Name */
        string levelFile = prog.levelNameLoad.text;
        if (prog.levelNameLoad.text == "")
            levelFile = "new_level";
        
        /* Calling Function for db management */
        StartCoroutine(loadMapOnDB(levelFile));
        
        /* Fill the map */
        //LoadLevelFromWeb(levelFile);
        CreateFromString();
    }

    IEnumerator loadMapOnDB(string levelname)
    {
        //mapDB = "";
    
        using (UnityWebRequest mapInfoRequest = UnityWebRequest.Get("http://127.0.0.1:8080/api/maps/1"))
        {
            yield return mapInfoRequest.SendWebRequest();
            if (mapInfoRequest.result == UnityWebRequest.Result.ConnectionError) // Error
            {
                prog.levelMessage.text = levelname + " could not be found!";
                prog.loadUIAnimation.SetTrigger("SaveLoadOut");
                prog.saveLoadPositionIn = false;
                prog.saveLoadMenuOpen = false;
                prog.messageAnim.Play("MessageFade", 0, 0);
                prog.levelNameLoad.DeactivateInputField();
                Debug.LogError(mapInfoRequest.error);
                yield break;
            }
            else // Success
            {
                string json = mapInfoRequest.downloadHandler.text;
                //Debug.Log("JSon: "+json);
                //Player[] player = JsonHelper.FromJson<Player>(json);
                //level = JsonUtility.FromJson<LevelEditor>(json);
                //Reformating return message
                /*string mess = mapInfoRequest.downloadHandler.text;
                //getting beginning
                mess = mess.Substring(mess.IndexOf(levelname));
                //getting end
                mess = mess.Substring(0, mess.IndexOf("]"));
                //Cutting the beggining again
                mess = mess.Substring(mess.IndexOf("{"));
                // Reformating the end
                mess = mess + "]}";
                // Removing special characters
                mapDB = mess.Replace(@"\", "");
                
                //Debug.Log("full request: "+mapDB);
                rows = mapDB.Split(new string[] { "}," }, StringSplitOptions.None);
                Debug.Log(String.Format("There are {0} comments.", rows.Length));
                int ArraySize = rows.Length / 2;
                
                for (int i = 0, counter = 0; i != rows.Length; i++, counter++) {
                    //getting the position of the object
                    float x = getFloat(rows[i], "x", ",");
                    rows[i] = rows[i].Substring(rows[i].IndexOf(','));
                    float y = getFloat(rows[i], "y", ",");
                    rows[i] = rows[i].Substring(rows[i].IndexOf(','));
                    float z = getFloat(rows[i], "z", "\0");
                    //getting the object type
                    i = i + 1;
                    int pos = rows[i].IndexOf(':') + 1;
                    Debug.Log("objtype"+rows[i][pos]);
                    //Recreating the objects
                    newObj = new Desc[ArraySize];
                    newObj[counter] = new Desc();
                    newObj[counter].pos = new Vector3(x, y, z);
                    if (rows[i][pos] == '0')
                        newObj[counter].objectType = ObjectType.Wall;
                    if (rows[i][pos] == '2')
                        newObj[counter].objectType = ObjectType.Player;
                }*/
                /*Converting to Json and finally to Level */ 
                //string json = JsonHelper.ToJson(newObj, true);
                //Debug.Log("json: "+json);
                
                //newObj = null;   
            }
        }
    }

    float getFloat(string number, string axis, string endMark)
    {
        number = number.Substring(number.IndexOf(axis) + 3, 16);
        float num = float.Parse(number, CultureInfo.InvariantCulture.NumberFormat);
        return (num);
    }

    void CreateFromString()
    {
        /* Initialisation */
        GameObject newObj;
        Debug.Log("In creating from String");
        for (int i = 0; i < level.editorObjects.Count; i++)
        {
            Debug.Log("In the loop");
            if (level.editorObjects[i].objectType == EditorObject.ObjectType.Wall)
                {
                    newObj = Instantiate(prog.wall, transform.position, Quaternion.identity);
                    newObj.transform.position = level.editorObjects[i].pos;
                    newObj.layer = 9;
                    EditorObject eo = newObj.AddComponent<EditorObject>();
                    eo.data.pos = newObj.transform.position;
                    eo.data.objectType = EditorObject.ObjectType.Wall;
                }
            else if (level.editorObjects[i].objectType == EditorObject.ObjectType.Player)
                {
                    newObj = Instantiate(prog.player, transform.position, Quaternion.identity);
                    newObj.layer = 9;
                    newObj.transform.position = level.editorObjects[i].pos;
                    prog.playerPlaced = true;
                    EditorObject eo = newObj.AddComponent<EditorObject>();
                    eo.data.pos = newObj.transform.position;
                    eo.data.objectType = EditorObject.ObjectType.Player;
                }
            }

        /*Misc Animation and UI Handling*/
        prog.levelNameLoad.text = "";
        prog.levelNameLoad.DeactivateInputField();
        prog.loadUIAnimation.SetTrigger("SaveLoadOut");
        prog.saveLoadPositionIn = false;
        prog.saveLoadMenuOpen = false;
        prog.levelMessage.text = "Level loading...done.";
        prog.messageAnim.Play("MessageFade", 0, 0);
    }

    public void LoadLevelFromWeb(string levelfile)
    {
        string folder = Application.dataPath + "/Imports/";
        string levelFile = "";
        if (prog.levelNameLoad.text == "")
            levelFile = "new_level.json";
        
        string path = Path.Combine(folder, levelFile);
        if (File.Exists(path))
        {
            EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
            foreach (EditorObject obj in foundObjects)
                Destroy(obj.gameObject);
            prog.playerPlaced = false;
            
            string json = File.ReadAllText(path);
            level = JsonUtility.FromJson<LevelEditor>(json);
            CreateFromString();
        }
    }
}