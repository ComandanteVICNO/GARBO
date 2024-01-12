using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("References")]
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text timerText;
    public TMP_Text gameoverScoreText;
    public TMP_Text gameoverScoreTextValue;
    public GameObject gameUI;
    public GameObject gameOverUI;
    public VibrationManager vibrationManager;



    [Header("Gamemode Titles")]
    public GameObject timedTitle;
    public GameObject mistakeTitle;

    [Header("TimeValues")]
    private float startTime;
    public float clockStopTime;
    private float clockPausedTime;
    private int clockPauseTimes;
    public float maxTime;
    public float newElapsedTime;

    [Header("Score Values")]
    int highScore;
    int score;
    int failed;


    [Header("Bool Checks")]
    public bool isTimerRunning = true;
    public bool isGameOver;
    public bool isGamemodeTimed;
    public bool setTimed;
    public bool wasTipShown;

    [Header("Recycling Tips")]
    [Multiline]
    public string[] tipsPT;
    [Multiline]
    public string[] tipsEN;
    public TMP_Text tipsText;
    public Locale ptLocale;
    public Locale enLocale;

    [Header("Audio")]
    public AudioSource itemDropAudioSource;
    public AudioClip itemDropClip;


    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        
        
    }
    private void Start()
    {
        int gamemode = PlayerPrefs.GetInt("GameMode");
        if (gamemode == 0)
        {
            isGamemodeTimed = false;
            timedTitle.SetActive(false);
            mistakeTitle.SetActive(true);
        }
        else
        {
            isGamemodeTimed = true;
            timedTitle.SetActive(true);
            mistakeTitle.SetActive(false);

        }
        Debug.Log("gamemode:" + gamemode);
        instance = this;
        vibrationManager = FindAnyObjectByType<VibrationManager>();
        isGameOver = false;
        score = 0;
        failed = 0;
        gameUI.SetActive(true);
        gameOverUI.SetActive(false);
        highScoreText.text = highScore.ToString(); 
        wasTipShown = false;
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        if (isGamemodeTimed)
        {
            ClockCountdown();
        }
    }
    #region ScoreHandling
    public void IncreaseScore()
    {
        score += 1;
        vibrationManager.Vibrate(vibrationManager.correctVibrationTime);

        PlayAudio();
    }

    public void DecreaseScore()
    {
        vibrationManager.WrongVibrate();
        if (isGamemodeTimed)
        {
            if (score == 0) return;
            else
            {
                score -= 1;
            }
        }
        else
        {
            failed += 1;
            if(failed > 3)
            {
                GameOverUI();
            }
            timerText.text = failed.ToString();
        }
        
    }
    #endregion
    #region Clock
    private void ClockCountdown()
    {
        if (isTimerRunning)
        {
            newElapsedTime = maxTime -= Time.deltaTime;
            UpdateTimerText(newElapsedTime);
            if (newElapsedTime <= 0)
            {
                GameOverUI();
            }
        }
    }

    private void UpdateTimerText(float elapsedTime)
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

        timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }

    public void StopClockActivate()
    {
        StartCoroutine(PauseClock(clockStopTime));
    }

    public IEnumerator PauseClock(float stopCooldown)
    {
        isTimerRunning = false;
        yield return new WaitForSeconds(stopCooldown);
        isTimerRunning = true;
        
    }
    #endregion
    #region UIHandling
    void GameOverUI()
    {
        isGameOver = true;
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
        gameoverScoreTextValue.text = "" + score;
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
        Time.timeScale = 0f;

        if (wasTipShown) return;

        if(LocalizationSettings.SelectedLocale == ptLocale)
        {
            int randomIndex = UnityEngine.Random.Range(0, tipsPT.Length);
            tipsText.text = tipsPT[randomIndex];
        }
        if(LocalizationSettings.SelectedLocale == enLocale)
        {
            int randomIndex = UnityEngine.Random.Range(0, tipsEN.Length);
            tipsText.text = tipsEN[randomIndex];
        }

        wasTipShown = true;
    }

    public void RestartGame()
    {
        
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene("MainMenu");
    }

    #endregion


    public void PlayAudio()
    {
        itemDropAudioSource.pitch = UnityEngine.Random.Range(0.8f, 1.3f);

        itemDropAudioSource.PlayOneShot(itemDropClip);
    }
}