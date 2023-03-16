using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem cloudsBottom;
    [SerializeField] private ParticleSystem cloudsTop;
    [SerializeField] public GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private TMPro.TMP_Text scoreText;
    [SerializeField] private TMPro.TMP_Text shieldIsActiveText;
    [SerializeField] private TMPro.TMP_Text squawkMeterText;

    private GameInstance _instance = GameInstance.Instance;

    // Bird properties
    public float spawnRate;
    public float heightOffset;
    public float moveSpeed;

    // Cloud properties
    public float velocityOverLifetime;
    public float startLifetime;

    // Player score and powerups
    public int playerScore;
    public bool shieldIsActive = false;
    public int squawkMeter;

    // Game logic
    public bool isPaused = false;

    private void Start()
    {
        initBird();
        initClouds();
        GameInstance.Instance.OnEnterLevel1();
    }

    #region Initialize GameObjects
    private void initBird()
    {
        spawnRate = SetValueFromDifficulty("spawnRate");
        heightOffset = SetValueFromDifficulty("heightOffset");
        moveSpeed = SetValueFromDifficulty("moveSpeed");
    }

    private void initClouds()
    {
        var _startLifetimeBottom = cloudsBottom.main.startLifetime;
        _startLifetimeBottom = SetValueFromDifficulty("startLifetime");
        var _velocityOverLifetimeBottom = cloudsBottom.velocityOverLifetime;
        _velocityOverLifetimeBottom.x = SetValueFromDifficulty("velocityOverLifetime");

        var _startLifetimeTop = cloudsTop.main.startLifetime;
        _startLifetimeTop = SetValueFromDifficulty("startLifetime");
        var _velocityOverLifetimeTop = cloudsTop.velocityOverLifetime;
        _velocityOverLifetimeTop.x = SetValueFromDifficulty("velocityOverLifetime");
    }

    public float SetValueFromDifficulty(string valueName)
    {
        return _instance.difficultyLevels[_instance.selectedDifficulty][valueName];
    }

    #endregion

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    #region SquawkMeter Management
    public void increaseSquawkMeter()
    {
        if (squawkMeter < 3)
        {
            squawkMeter += 1;
            squawkMeterText.text = squawkMeter.ToString();
        }
        else if (squawkMeter == 3)
        {
            shieldIsActive = true;
            squawkMeter += 1;
            shieldIsActiveText.text = shieldIsActive.ToString();
            GameInstance.Instance.birdSwoosh.Play();
            GameInstance.Instance.bigSquawk.Play();
        }
        else if (squawkMeter > 3)
        {
            squawkMeter += 1;
            shieldIsActiveText.text = shieldIsActive.ToString();
        }
    }

    public void resetSquawkMeter()
    {
        squawkMeter = 0;
        shieldIsActive = false;
        shieldIsActiveText.text = shieldIsActive.ToString();
    }
    #endregion

    #region Game State Management (pause/retry/gameover)
    public void togglePauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void OnRetryPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenuPress()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }
    #endregion

}
