using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using System.Globalization;

public class apiHandler : MonoBehaviour
{
    /* URL for maps*/
    private readonly string baseURL = "http://127.0.0.1:8080/api/maps";

    /* Level manipulation for Json */
    private LevelEditor level;
    // Master class
    public Manager prog;
    
    // Obj Mention
    GameObject newObj;
     
    /* Route List*/
    private List<Routes> listRoutes ;
    int index;
    public class Routes
    {
        public string id;
        public string map_name;
        public string url;
    }

    private void Start()
    {
        listRoutes = new List<Routes>(20);
        CreateEditor();
    }

    LevelEditor CreateEditor()
    {
        level = new LevelEditor();
        level.editorObjects = new List<EditorObject.Data>();
        return level;
    }

    /* Adding routes to the list */
    private void AddId(string levelname)
    {
        /* Init */
        StartCoroutine(getLastOccurenceOfID());
    
        /* Manipulation */
        Routes info = new Routes();
        info.id = Convert.ToString(index + 1);
        info.map_name = levelname;
        info.url = baseURL+"/"+Convert.ToString(index + 1);
        listRoutes.Add(info);
    }

    private string FindID(string levelname)
    {       
         /* Init */
        int counter = 0;

        /* Incrementing the counter */ 
        foreach (Routes cn in listRoutes)
        {
            counter++;
            if (String.Equals(cn.map_name, levelname))
                return cn.url;
        }
        return("FAIL");
    }

    /* Saving Level */
    public void SaveLevel()
    {
        /* Init */
        string creatorName =  "";
        string leveltitle = "";

        /* Creating a json with all objects defined in Editor Object */
        EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
        foreach (EditorObject obj in foundObjects)
            level.editorObjects.Add(obj.data);
        string json = JsonUtility.ToJson(level);

        /* Setting the Name of The map*/
        leveltitle = prog.levelNameSave.text;
        if (prog.levelNameSave.text == "") {
            prog.levelMessage.text = "As the level name is empty it has been set to 'new_level'";
            leveltitle = "new_level";
        }

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

        /* Call to API */
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

        /* API Interaction */
        UnityWebRequest www = UnityWebRequest.Post(baseURL, formdata);
        yield return www.SendWebRequest();

        /* Checking if operations were successfull */
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            AddId(leveltitle);
            prog.levelMessage.text = "Map upload complete!";
        }
    }

    public void LoadLevel()
    {
        /* Retrieving the Level Name */
        string levelFile = prog.levelNameLoad.text;
        if (prog.levelNameLoad.text == "")
            levelFile = "new_level";
        
        /* Deleting Elements on map */
        EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
        foreach (EditorObject obj in foundObjects) {
            Destroy(obj.gameObject);
        }
        prog.playerPlaced = false;

        /* Calling Function for db management */
        StartCoroutine(loadMapOnDB(levelFile));
    }

    IEnumerator loadMapOnDB(string levelname)
    {
        /* Init */
        string[] rows;

        /* Manipulation */
        using (UnityWebRequest mapInfoRequest = UnityWebRequest.Get(FindID(levelname)))
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
                string rawdata = mapInfoRequest.downloadHandler.text;
                rows = rawdata.Split(new string[] { "}," }, StringSplitOptions.None);
                //Debug.Log(String.Format("There are {0} comments.", rows.Length));
                int ArraySize = rows.Length / 2;

                for (int i = 0, counter = 0; i < rows.Length; i++, counter++) {
                    /*getting the position of the object */
                    float x = getFloat(rows[i], "x", ":");
                    rows[i] = rows[i].Substring(rows[i].IndexOf(','));
                    float y = getFloat(rows[i], "y", ":");
                    rows[i] = rows[i].Substring(rows[i].IndexOf(','));
                    float z = getFloat(rows[i], "z", "z");
                    
                    /*  Getting the object type*/
                    i = i + 1;
                    int pos = rows[i].IndexOf(':') + 1;
                    
                    /* Creating obj */
                    if (rows[i][pos] == '0')
                        newObj = Instantiate(prog.wall, transform.position, Quaternion.identity);
                    if (rows[i][pos] == '2')
                        newObj = Instantiate(prog.StartPT, transform.position, Quaternion.identity);
                    if (rows[i][pos] == '1')
                        newObj = Instantiate(prog.caltrop, transform.position, Quaternion.identity);
                    if (rows[i][pos] == '3')
                        newObj = Instantiate(prog.mud, transform.position, Quaternion.identity);
                    if (rows[i][pos] == '4')
                        newObj = Instantiate(prog.EndPT, transform.position, Quaternion.identity);
                    newObj.transform.position = new Vector3(x, y, z);
                    newObj.layer = 9;
                    EditorObject eo = newObj.AddComponent<EditorObject>();
                    eo.data.pos = newObj.transform.position;
                    if (rows[i][pos] == '0')
                        eo.data.objectType = EditorObject.ObjectType.Wall;
                    if (rows[i][pos] == '1')
                        eo.data.objectType = EditorObject.ObjectType.Caltrop;
                     if (rows[i][pos] == '2')
                        eo.data.objectType = EditorObject.ObjectType.Player;
                    if (rows[i][pos] == '3')
                        eo.data.objectType = EditorObject.ObjectType.Mud;
                     if (rows[i][pos] == '4')
                        eo.data.objectType = EditorObject.ObjectType.End;
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
        }
    }

    private float getFloat(string number, string axis, string endMark)
    {
        if (String.Equals(axis, "z")) {
            number = number.Substring(number.IndexOf(axis) + 3);
        }
        else if (String.Equals(axis, "x") || String.Equals(axis, "y")) {
            number = number.Substring(number.IndexOf(axis) + 3, number.IndexOf(endMark) - 1);
        }
        Debug.Log("number in getFloat: "+number);
        float num = float.Parse(number, CultureInfo.InvariantCulture.NumberFormat);
        return (num);
    }

    private IEnumerator getLastOccurenceOfID()
    {
        //http call, baseURL is "http://127.0.0.1:8080/api/maps"
        using (UnityWebRequest mapInfoRequest = UnityWebRequest.Get(baseURL))
        {
            //return de la request, mapInfoRequest contient les données
            yield return mapInfoRequest.SendWebRequest();
            if (mapInfoRequest.result == UnityWebRequest.Result.ConnectionError) // Error
            {
                Debug.LogError(mapInfoRequest.error);
            }
            else // Success
            {
                // Récupérér dans une string le résultat de la requête
                string rawdata = mapInfoRequest.downloadHandler.text;
                rawdata = rawdata.Substring(rawdata.LastIndexOf("id"));
                // it will only be able to handle a max of 9 rows on databas
                index = Convert.ToInt16(rawdata[4]);
            }
        }
    }
}