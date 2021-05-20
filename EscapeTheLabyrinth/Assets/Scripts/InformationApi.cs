using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;
using System.Globalization;
using TMPro;

public class InformationApi : MonoBehaviour
{
    /* Base Url of obstacles*/
    private readonly string baseURL = "http://127.0.0.1:8080/api/obstacles";

    /* Reference to UI Class */
    public InformationManager info;
    
    /* Overall data retrieved from web */
    private string rawdata;
    private int index;

    /* Container Class for data*/
    private List<Routes> listRoutes;
    public class Routes
    {
        public string id;
        public string name;
        public string maximum;
        public string minimum;
        public string passable;
        public string url;
    }

    // Start is called before the first frame update
    void Start()
    {
        /* Creating the Routes */
        listRoutes = new List<Routes>(4);
        addRoute("Wall", "100", "0", "Not Possible");
        addRoute("Player", "1", "1", "Not Possible");
        addRoute("Killer", "1", "0", "Possible");
        addRoute("Mud", "5", "0", "Possible");
    
        /*Inserting Data on Database */
        StartCoroutine(postInformation("Wall", "100", "1", "Not Possible"));
        StartCoroutine(postInformation("Player", "1", "1", "Not Possible"));
        StartCoroutine(postInformation("Killer", "1", "0", "Possible"));
        StartCoroutine(postInformation("Mud", "5", "0", "Possible"));
    }

    private void addRoute(string name, string maximum, string minimum, string passable)
    {
        /* Init */
        int counter = 0;
        
        /* Deleting Previous Routes with same name */
        if (index > 4) {
            StartCoroutine(getLastOccurenceOfID());
            foreach(Routes cn in listRoutes)
            {
                counter++;
                if (String.Equals(cn.name, name)) {
                    break;
                }
            }
            listRoutes.RemoveAt(counter);
        }

        /*Updating the index for initial depoyment */
        if (index < 5)
            index += 1;
    
        /* Manipulation */
        Routes info = new Routes();
        info.name = name;
        info.id = index.ToString();
        info.maximum = maximum;
        info.minimum = minimum;
        info.url = baseURL+"/"+index.ToString();
        info.passable = passable;
        listRoutes.Add(info);
    }

    private IEnumerator getInformation(string url)
    {
         /* API Interaction */
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        /* Checking if operations were successfull */
        if (www.result != UnityWebRequest.Result.Success) // Error
        {
            Debug.Log(www.error);
        }
        else
        {
            rawdata = www.downloadHandler.text; // Success
        }
    }

    private IEnumerator postInformation(string name, string maximum, string minimum, string passable)
    {
        /* Adding rows to form which will be sent to DB */
        WWWForm formData = new WWWForm();
        formData.AddField("name", name);
        formData.AddField("passable", passable);
        formData.AddField("maximum", maximum);
        formData.AddField("minimum", minimum);
        // To work, no values can be empty*

        /* API Interaction */
        UnityWebRequest www = UnityWebRequest.Post(baseURL, formData);
        yield return www.SendWebRequest();

        /* Checking if operations were successfull */
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Creation on database");
        }
    }

    public void UpdateSection()
    {
        if (info.desc_wall.activeSelf) {
            /* Posting New Data on Database */
            StartCoroutine(postInformation("Wall", info.max_in.text, info.min_in.text, "No"));    
            
            /* Updating the UI */
            if (info.max_in.text != null)
                info.wall_max.text = "Max: "+info.max_in.text;
            if (info.min_in.text != null)
                info.wall_min.text = "Min: "+info.min_in.text;
            
            /*Updating the List */
            addRoute("Wall", info.max_in.text, info.min_in.text, "No");
        }
        else if (info.desc_mud.activeSelf) {
            /* Posting New Data on Database */
            StartCoroutine(postInformation("Mud", info.max_in.text, info.min_in.text, "Yes"));

            /* Updating the UI */ 
            if (info.max_in.text != null)
                info.mud_max.text = "Max: "+info.max_in.text;
            if (info.min_in.text != null)
                info.mud_min.text = "Min: "+info.min_in.text;

            /*Updating the List */
            addRoute("Mud", info.max_in.text, info.min_in.text, "Yes");
        }
        else if (info.desc_killer.activeSelf) {
            /* Posting New Data on Database */
            StartCoroutine(postInformation("Killer", info.max_in.text, info.min_in.text, "Yes"));

            /* Updating the UI */
            if (info.max_in.text != null)
                info.killer_max.text = "Max: "+info.max_in.text;
            if (info.min_in.text != null)
                info.killer_min.text = "Min: "+info.min_in.text;
            
            /*Updating the List */
            addRoute("Killer", info.max_in.text, info.min_in.text, "Yes");
        }
        /* Setting the Inputs Fields to Empty */
        info.max_in.text = "";
        info.min_in.text = "";
    }

    private IEnumerator getLastOccurenceOfID()
    {
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
                // it will only be able to handle a max of 9 rows on database
                // Because C# PARSING IS CRAP
                index = (int)Char.GetNumericValue(rawdata[4]);
            }
        }
    }
}
