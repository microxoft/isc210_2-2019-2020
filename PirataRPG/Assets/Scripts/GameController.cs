using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int CurrentScore;
    public TextMesh ScoreText;
    public TextMesh LivesText;
    public GameObject GameOverText;
    const float MINX = -8f, MAXX = 8f;
    public GameObject BallPrefab;
    public int CurrentLives;

    // Start is called before the first frame update
    void Start()
    {
        CurrentScore = 0;
        CurrentLives = 3;
        InvokeRepeating("InstantiateBall", 0, 1.5f);
        GameOverText = GameObject.Find("GameOverText");
        GameOverText.SetActive(false);
    }
    
    void InstantiateBall()
    {
        if (CurrentLives <= 0)
            return;

        Instantiate(BallPrefab, new Vector3(Random.Range(MINX, MAXX), 6, 0.5f), Quaternion.identity);
    }

    public int IncrementScore()
    {
        CurrentScore++;
        ScoreText.text = CurrentScore.ToString();
        return CurrentScore;
    }

    public int DecrementLife()
    {
        CurrentLives = CurrentLives > 0 ? CurrentLives - 1 : 0;
        LivesText.text = $"Vidas: {CurrentLives}";

        if(CurrentLives == 0)
        {
            // Game over!!
            //StartCoroutine("SendScore");
            GameOverText.SetActive(true);
        }

        return CurrentLives;
    }

    IEnumerator SendScore()
    {
        yield return gameObject.GetComponent<WebServiceClient>().SendWebRequest(CurrentScore);
    }
}
