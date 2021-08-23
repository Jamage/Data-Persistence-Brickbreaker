using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public class HighScoreList
{
    public List<HighScoreData> ScoreList;
    public bool IsEmpty => ScoreList.Count > 0;
    public int Count => ScoreList.Count;
    public int LowestScore => GetLowestScore();
    public HighScoreData HighestScore => GetHighestScore();

    private readonly int maxSize = 10;

    public HighScoreList()
    {
        ScoreList = new List<HighScoreData>();
    }
    
    private int GetLowestScore()
    {
        if (ScoreList.Count == maxSize)
            return ScoreList.Last().Score;
        else
            return 0;
    }

    private HighScoreData GetHighestScore()
    {
        if (ScoreList.Count == 0)
            return new HighScoreData();
        else
            return ScoreList.First();
    }

    public bool AddScore(HighScoreData newScoreData)
    {
        if (ScoreList.Count < 10)
        {
            Add(newScoreData);
            return true;
        }
        else
        {
            for (int index = ScoreList.Count - 1; index >= 0; index--)
            {
                if (ScoreList[index].Score < newScoreData.Score)
                {
                    ScoreList.RemoveAt(index);
                    Add(newScoreData);
                    return true;
                }
            }
        }

        return false;
    }

    internal void OrderScores()
    {
        ScoreList = ScoreList.OrderByDescending(x => x.Score).ThenBy(x => x.Name).ToList();
        RankScores();
    }

    private void RankScores()
    {
        if (ScoreList.Count == 0)
            return;

        HighScoreData lastRank = ScoreList[0];
        for (int index = 1; index < ScoreList.Count; index++)
        {
            if(lastRank.Score == ScoreList[index].Score)
            {
                ScoreList[index].Rank = lastRank.Rank;
            }
            else if(lastRank.Score > ScoreList[index].Score)
            {
                ScoreList[index].Rank = lastRank.Rank + 1;
                lastRank = ScoreList[index];
            }
        }
    }

    private void Add(HighScoreData scoreData)
    {
        ScoreList.Add(scoreData);
        OrderScores();
    }
}

[Serializable]
public class HighScoreData
{
    public int Score;
    public int Rank;
    public string Name;

    public HighScoreData()
    {
        Score = 0;
        Name = "";
        Rank = 1;
    }
}