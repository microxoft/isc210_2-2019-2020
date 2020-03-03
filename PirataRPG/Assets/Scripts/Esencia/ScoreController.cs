using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int[] Scores = new int[6];
    public TextMesh BlueScoreText, GreenScoreText, OrangeScoreText, PurpleScoreText, RedScoreText, YellowScoreText;
    
    public void IncrementScore(EssenceType number)
    {
        Scores[(int)number]++;
        switch (number)
        {
            case EssenceType.Blue:
                BlueScoreText.text = Scores[(int)number].ToString();
                break;
            case EssenceType.Green:
                GreenScoreText.text = Scores[(int)number].ToString();
                break;
            case EssenceType.Orange:
                OrangeScoreText.text = Scores[(int)number].ToString();
                break;
            case EssenceType.Purple:
                PurpleScoreText.text = Scores[(int)number].ToString();
                break;
            case EssenceType.Red:
                RedScoreText.text = Scores[(int)number].ToString();
                break;
            case EssenceType.Yellow:
                YellowScoreText.text = Scores[(int)number].ToString();
                break;
        }
    }
}
