using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class HighScoreManager : MonoBehaviour
{
    public List<HighScoreDataRow> ScoreRowList;
    public static HighScoreList scoreList;
    public GameObject LeaderboardCanvas;
    public GameObject LeaderboardColumnText;
    public GameObject NoScoresText;
    public UnityAction OnHighScoreLoad;

    private void Start()
    {
        scoreList = LoadScores();
        scoreList.OrderScores();
        SetScores();
        OnHighScoreLoad?.Invoke();
    }

    public void SetScores()
    {
        if (scoreList?.Count == 0)
        {
            LeaderboardColumnText.SetActive(false);
            NoScoresText.SetActive(true);
        }
        else
        {
            NoScoresText.SetActive(false);
            LeaderboardColumnText.SetActive(true);
            for (int index = 0; index < scoreList.Count; index++)
            {
                ScoreRowList[index].SetScore(scoreList.ScoreList[index]);
            }
            OnHighScoreLoad?.Invoke();
        }

    }

    public void AddScore(HighScoreData newScoreData)
    {
        bool scoreAdded = scoreList.AddScore(newScoreData);
        if (scoreAdded)
        {
            SaveScores();
            SetScores();
        }
    }

    private static readonly string leaderboardFilename = "highscores.json";
    public void SaveScores()
    {
        HighScoreList scoreList = HighScoreManager.scoreList;
        string json = JsonUtility.ToJson(scoreList);
        File.WriteAllText($"{Application.persistentDataPath}/{leaderboardFilename}", json);
    }

    public HighScoreList LoadScores()
    {
        string path = $"{Application.persistentDataPath}/{leaderboardFilename}";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreList data = JsonUtility.FromJson<HighScoreList>(json);
            data.OrderScores();

            if (data?.Count == 0)
                return new HighScoreList();
            else
                return data;
        }
        else
            return new HighScoreList();
    }
}
