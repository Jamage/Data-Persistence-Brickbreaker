using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject Leaderboard;

    public static bool m_Started = false;
    private int m_Points;

    public static bool m_GameOver = false;
    public static bool m_EnteringName = false;
    public TMP_InputField NameInputField;
    public GameObject NameEntryText;
    public Text TopScoreText;
    public Button backButton;
    public HighScoreManager highScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        Leaderboard.SetActive(false);
        m_Started = false;
        m_GameOver = false;
        m_EnteringName = false;
        backButton.onClick.AddListener(ReturnToMainMenu);
        highScoreManager.OnHighScoreLoad += SetTopScore;
    }

    private void OnDestroy()
    {
        highScoreManager.OnHighScoreLoad -= SetTopScore;
    }

    private void SetTopScore()
    {
        HighScoreData topScore = HighScoreManager.scoreList.HighestScore;
        TopScoreText.text = $"Top Score: {topScore.Name} - {topScore.Score}";
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_EnteringName)
        {
            if (Input.GetKeyDown(KeyCode.Return) && NameInputField.text.Trim().Length > 0)
            {
                HighScoreData newScoreData = new HighScoreData()
                {
                    Name = NameInputField.text.Trim(),
                    Score = m_Points
                };

                highScoreManager.AddScore(newScoreData);
                NameInputField.gameObject.SetActive(false);
                NameEntryText.SetActive(false);
                m_EnteringName = false;
                GameOver();
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score: {m_Points}";
    }

    public void RoundOver()
    {
        if (m_Points > HighScoreManager.scoreList.LowestScore)
        {
            SetupNameEntry();
            m_EnteringName = true;
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    private void SetupNameEntry()
    {
        Leaderboard.SetActive(true);
        NameInputField.Select();
    }
}
