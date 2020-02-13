using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebServiceClient : MonoBehaviour
{
    [Serializable]
    public class GravilotaScore
    {
        public int Id;
        public string PlayerName;
        public double Score;
    }

    UnityWebRequest www;
    const string webServiceURL = "http://localhost:64198/api/values";
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("SendWebRequest");
    }

    public IEnumerator SendWebRequest(double score)
    {
        GravilotaScore newScore = new GravilotaScore();
        newScore.Id = 5;
        newScore.PlayerName = "Unity Player";
        newScore.Score = score;

        www = UnityWebRequest.Put(webServiceURL, JsonUtility.ToJson(newScore));
        www.SetRequestHeader("Content-Type", "application/json");
        //yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
