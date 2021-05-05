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
    private string[] rows;

    private readonly string baseURL = "http://127.0.0.1:8080";

    private void Start()
    {
        //maybe
        //string overAllText.text = "";
        //StartCoroutine(testingDaMothafucka());
        StartCoroutine(testingUploadShit());
    }

    IEnumerator testingDaMothafucka()
    {
        using (UnityWebRequest mapInfoRequest = UnityWebRequest.Get("http://127.0.0.1:8080/api/maps"))
        {
            yield return mapInfoRequest.SendWebRequest();
            if (mapInfoRequest.result == UnityWebRequest.Result.ConnectionError) // Error
            {
                Debug.LogError(mapInfoRequest.error);
                yield break;
            }
            else // Success
            {
                Debug.Log("Received: " + mapInfoRequest.downloadHandler.text);
                string fulldata = mapInfoRequest.downloadHandler.text;
                rows = fulldata.Split(new string[] { "<br>" }, StringSplitOptions.None);
                Debug.Log(String.Format("There are {0} comments.", rows.Length));
                foreach (string row in rows)
                {
                    Debug.Log(row);
                }
            }
        }
    }
    IEnumerator testingUploadShit()
    {
        string jsonString = File.ReadAllText(Application.dataPath+"/LevelData/Demo.json");
        
        WWWForm formdata = new WWWForm();
        formdata.AddField("title", "Testing the json upload");
        formdata.AddField("creator", "Cameron");
        formdata.AddField("map_layout", jsonString);
        formdata.AddField("result", "succes");
        

        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:8080/api/maps", formdata);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
        //string pokeName = pokeInfo["name"];
    }

    public void OnButtonRandomPokemon()
    {
        //overAll.text = "Loading...";
        //StartCoroutine(GetPokemonAtIndex(randomPokeIndex));
        //StartCoroutine(getMapURL(MapName));
    }

    IEnumerator downLoad(string MapName)
    {
        // Get MapInfo
        string mapURL = baseURL + "api/" + MapName;
        // Example URL: https://http://127.0.0.1:8000/api/TestMap

        UnityWebRequest mapInfoRequest = UnityWebRequest.Get(mapURL);

        yield return mapInfoRequest.SendWebRequest();

        /*if (mapInfoRequest.isNetworkError || mapInfoRequest.isHttpError)
        {
            Debug.LogError(mapInfoRequest.error);
            yield break;
        }

        JSONNode mapInfo = JSON.Parse(mapInfoRequest.downloadHandler.text);*/
    }

    IEnumerator upload()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", formData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
        //string pokeName = pokeInfo["name"];
    }
    
}

