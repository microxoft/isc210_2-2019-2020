using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class EssenceScore
{
    public int Id;
    public string PlayerName;
    public float BlueScore;
    public float GreenScore;
    public float RedScore;
    public float YellowScore;
    public float OrangeScore;
    public float PurpleScore;
}

[Serializable]
public class Container
{
    public EssenceScore[] essenceScores; 
}

public class EssenceWebClient : MonoBehaviour
{
    ScoreController scoreController;
    UnityWebRequest www;
    const string url = "http://localhost:64198/api/Essence/";

    private void Awake()
    {
        scoreController = GetComponent<ScoreController>();
    }

    public void SaveScore()
    {
        StartCoroutine(SendScores());
    }

    public void TopScores()
    {
        StartCoroutine(GetTopScores());
    }

    IEnumerator SendScores()
    {
        EssenceScore newScore = new EssenceScore();
        newScore.PlayerName = "Miguel Unity";
        newScore.BlueScore = scoreController.Scores[0];
        newScore.GreenScore = scoreController.Scores[1];
        newScore.OrangeScore = scoreController.Scores[2];
        newScore.PurpleScore = scoreController.Scores[3];
        newScore.RedScore = scoreController.Scores[4];
        newScore.YellowScore = scoreController.Scores[5];

        www = UnityWebRequest.Put(url, JsonUtility.ToJson(newScore));
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
    }

    public IEnumerator GetTopScores()
    {
        www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        foreach(EssenceScore currentScore in JsonUtility.FromJson<Container>("{\"essenceScores\":" + www.downloadHandler.text + "}").essenceScores)
        {
            Debug.Log(currentScore.PlayerName);
        }
    }
}
