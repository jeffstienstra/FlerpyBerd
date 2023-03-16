using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{

    #region Instance Management
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadPersistentLevel()
    {
        const string LevelName = "PersistentLevel";

        for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; sceneIndex++)
        {
            if (SceneManager.GetSceneAt(sceneIndex).name == LevelName)
                return;
        }

        SceneManager.LoadScene(LevelName, LoadSceneMode.Additive);
    }

    public static GameInstance Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Fourn duplicate GameInstance on {gameObject.name}");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(Instance);
    }
    #endregion

    #region Audio Setup
    public AudioSource birdSongs;
    public AudioSource bigSquawk;
    public AudioSource birdFlap;
    public AudioSource birdSwoosh;
    public AudioSource getEgg;
    public AudioSource hit1;
    public AudioSource hit2;
    public AudioSource squawk;
    #endregion

    #region Game Phase
    public enum EGamePhase
    {
        Unknown,

        MainMenu,
        Level1,
    }

    public EGamePhase Phase { get; private set; } = EGamePhase.Unknown;
    #endregion

    #region Difficulty Management
    public enum Difficulty { Easy, Medium, Hard };
    public Difficulty selectedDifficulty = Difficulty.Easy;
    public int selectedDifficultyIndex = 0;

    public Dictionary<Difficulty, Dictionary<string, int>> difficultyLevels = new Dictionary<Difficulty, Dictionary<string, int>>()
    {
        {
            Difficulty.Easy, new Dictionary<string, int>()
            {
                { "spawnRate", 2 },
                { "heightOffset", 6 },
                { "moveSpeed", 10 },
                { "velocityOverLifetime", -4 },
                { "startLifetime", 7 }
            }
        },
        {
            Difficulty.Medium, new Dictionary<string, int>()
            {
                { "spawnRate", 1 },
                { "heightOffset", 10 },
                { "moveSpeed", 20 },
                { "velocityOverLifetime", -9 },
                { "startLifetime", 5 }
            }
        },
        {
            Difficulty.Hard, new Dictionary<string, int>()
            {
                { "spawnRate", 1 },
                { "heightOffset", 14 },
                { "moveSpeed", 30 },
                { "velocityOverLifetime", -14 },
                { "startLifetime", 3 }
            }
        }
    };

    public void HandleDifficultyChange(int index)
    {
        switch (index)
        {
            case 0:
                selectedDifficulty = Difficulty.Easy;
                selectedDifficultyIndex = index;
                Debug.Log("Difficulty: " + selectedDifficulty);
                break;
            case 1:
                selectedDifficulty = Difficulty.Medium;
                selectedDifficultyIndex = index;
                Debug.Log("Difficulty: " + selectedDifficulty);
                break;
            case 2:
                selectedDifficulty = Difficulty.Hard;
                selectedDifficultyIndex = index;
                Debug.Log("Difficulty: " + selectedDifficulty);
                break;
        }
    }
    #endregion

    public void OnEnterMainMenu()
    {
        Phase = EGamePhase.MainMenu;
        Debug.Log("Phase: " + Phase);
    }

    public void OnEnterLevel1()
    {
        Phase = EGamePhase.Level1;
        Debug.Log("Phase: " + Phase);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
