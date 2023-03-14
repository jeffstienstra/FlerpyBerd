using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManagerScript : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text scoreText;
    [SerializeField]
    private TMPro.TMP_Text squawkMeterText;
    [SerializeField]
    private TMPro.TMP_Text shieldIsActiveText;

    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public BirdSoundController birdSoundController;
    public DifficultyManagerScript difficultyManager;

    public float spawnRate;
    public float heightOffset;
    public float moveSpeed;
    public int playerScore;
    public bool shieldIsActive = false;
    public int squawkMeter;
    public bool isPaused = false;

    private void Awake()
    {
        birdSoundController = GameObject.FindGameObjectWithTag("BirdSounds").GetComponent<BirdSoundController>();
        difficultyManager = GameObject.FindGameObjectWithTag("Difficulty").GetComponent<DifficultyManagerScript>();

        if (PlayerPrefs.HasKey("SelectedDifficulty"))
        {
            Debug.Log("playerPrefs: " + PlayerPrefs.GetInt("SelectedDifficulty"));
            //int storedDifficulty = PlayerPrefs.GetInt("SelectedDifficulty");
            //difficultyManager.HandleDifficultyChange(storedDifficulty);
        }
        //else
        //{
        //    difficultyManager.HandleDifficultyChange(0);
        //}
    }
    private void Start()
    {

        spawnRate = difficultyManager.difficultyLevels[difficultyManager.selectedDifficulty]["spawnRate"];
        heightOffset = difficultyManager.difficultyLevels[difficultyManager.selectedDifficulty]["heightOffset"];
        moveSpeed = difficultyManager.difficultyLevels[difficultyManager.selectedDifficulty]["moveSpeed"];
        //velocityOverLifetime = difficultyManager.difficultyLevels[difficultyManager.selectedDifficulty]["velocityOverLifetime"];
        //startLifetime = difficultyManager.difficultyLevels[difficultyManager.selectedDifficulty]["startLifetime"];
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void togglePauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        } else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

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
            birdSoundController.birdSwoosh.Play();
            birdSoundController.bigSquawk.Play();
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

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
