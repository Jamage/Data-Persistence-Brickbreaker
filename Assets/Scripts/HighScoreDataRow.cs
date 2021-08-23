using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDataRow : MonoBehaviour
{
    [Range(1, 10)]
    public int Rank = 1;
    private HighScoreData data;
    public TextMeshProUGUI RankText;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI ScoreText;

    private void Start()
    {
        if (data == null)
            gameObject.SetActive(false);
    }

    public void SetScore(HighScoreData scoreData)
    {
        data = scoreData;
        SetText();
        gameObject.SetActive(true);
    }

    private void SetText()
    {
        RankText.text = data.Rank.ToString();
        NameText.text = data.Name;
        ScoreText.text = data.Score.ToString("000");
    }
}
