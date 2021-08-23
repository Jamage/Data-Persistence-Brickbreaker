using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMenu : MonoBehaviour
{
    public Button startButton;
    public Button leaderboardButton;
    public Button backButton;
    public Button exitButton;
    public GameObject mainCanvas;
    public GameObject leaderboard;
    public HighScoreManager highScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        if(startButton != null)
            startButton.onClick.AddListener(StartGame);
        if(leaderboardButton != null)
            leaderboardButton.onClick.AddListener(ShowLeaderboard);
        if (exitButton != null)
            exitButton.onClick.AddListener(Exit);
        if (backButton != null)
            backButton.onClick.AddListener(Back);
        leaderboard.SetActive(false);
        mainCanvas.SetActive(true);
    }

    private void Back()
    {
        leaderboard.SetActive(false);
        mainCanvas.SetActive(true);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ShowLeaderboard()
    {
        leaderboard.SetActive(true);
        mainCanvas.SetActive(false);
    }

    private void Exit()
    {
        highScoreManager.SaveScores();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
