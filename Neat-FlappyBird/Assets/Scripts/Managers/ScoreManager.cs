using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void UpScore(int t_addition)
    {
        currentScore += t_addition;
        UpdateText();
    }

    private void Start()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
    }

    private void UpdateText()
    {
        scoreText.text = currentScore.ToString();
    }
}
