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
    private int scoreTracker = 0;   // Track score left to spawn boss
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
    public int Score { get { return score; } }
    public int ScoreTracker { get { return scoreTracker; } set { scoreTracker = value; } }

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
        // Finish the game if player loses all stock
        if (stock <= 0 && isGameOver == false)
        {
            GameOver();
        }

        // Check if player hit the new record
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            UpdateRecord();
            SetHighScore();

            // Inform player only one time
            if (!newRecordMessageIsShown)
            {
                newRecord.enabled = true;
            }
        }

        // Restart the game
        if (isGameOver && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadSceneAsync("SHMUPScene");
            stock = 3;
        }

        // Hide the new record message after 3 seconds
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

    // Add a score to the current score
    public void AddScore(int amount)
    {
        score += amount;
        scoreTracker += amount;
        scoreText.text = string.Format("{0:D5}", score);
    }
    
    // Player loses stock
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

    // Consume a bomb to explode specific bullets and ships in the field
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
                    MissileSpawner.instance.ExplodeMissiles();
                    return;
                }
            }
        }
    }

    // Update new record
    public void UpdateRecord()
    {
        if (PlayerPrefs.GetInt("HighScore", 0) < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    // Restore highest score to 0
    public void ClearRecord()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }

    // Set highest score to current score
    public void SetHighScore()
    {
        highScoreText.text = string.Format("{0:D5}", PlayerPrefs.GetInt("HighScore", 0));
    }

    // Finish current game and ask user to retry
    public void GameOver()
    {
        UpdateRecord();
        Time.timeScale = 0;
        isGameOver = true;
        gameOver.enabled = true;
    }
}
