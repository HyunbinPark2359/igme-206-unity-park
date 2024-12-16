using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance { get; private set; }

    private int score = 0;
    private Text scoreText;
    private Text highScoreText;
    private Text newRecord;
    private bool newRecordMessageIsShown = false;
    private float timer = 0;

    private int stock = 3;
    [SerializeField] private GameObject[] hearts;

    private int bomb = 2;
    [SerializeField] private GameObject[] bombs;

    private bool isGameOver = false;
    private Text gameOver;

    public int Bomb
    {
        get
        { 
            return bomb;
        }
        set
        {
            bomb = value;
        }
    }

    private void Awake()
    {
        instance = this;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        newRecord = GameObject.Find("NewRecord").GetComponent<Text>();
        newRecord.enabled = false;
        gameOver = GameObject.Find("GameOver").GetComponent<Text>();
        gameOver.enabled = false;
        SetHighScore();
    }

    private void Update()
    {
        if (stock <= 0 && isGameOver == false)
        {
            GameOver();
        }

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            UpdateRecord();
            SetHighScore();
            if (!newRecordMessageIsShown)
            {
                newRecord.enabled = true;
            }
        }

        if (isGameOver && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadSceneAsync("SHMUPScene");
            stock = 3;
        }

        if (newRecord.enabled)
        {
            newRecordMessageIsShown = true;
            timer += Time.deltaTime;

            if (timer > 3.0f)
            {
                newRecord.enabled = false;
            }
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = string.Format("{0:D5}", score);
    }

    public void TakeDamage()
    {
        for (int i = hearts.Length - 1; i >= 0; i--)
        {
            GameObject heart = hearts[i];
            if (heart.activeInHierarchy)
            {
                heart.SetActive(false);
                stock--;
                return;
            }
        }    
    }

    public void UseBomb()
    {
        if (bomb > 0)
        {
            for (int i = bombs.Length - 1; i >= 0; i--)
            {
                GameObject _bomb = bombs[i];
                if (_bomb.activeInHierarchy)
                {
                    _bomb.SetActive(false);
                    bomb--;
                    BulletSpawner.instance.ExplodeBullets();
                    EnemySpawner.instance.ExplodeShips();
                    return;
                }
            }
        }
    }

    public void UpdateRecord()
    {
        if (PlayerPrefs.GetInt("HighScore", 0) < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    public void ClearRecord()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }

    public void SetHighScore()
    {
        highScoreText.text = string.Format("{0:D5}", PlayerPrefs.GetInt("HighScore", 0));
    }

    public void GameOver()
    {
        UpdateRecord();
        Time.timeScale = 0;
        isGameOver = true;
        gameOver.enabled = true;
    }
}
