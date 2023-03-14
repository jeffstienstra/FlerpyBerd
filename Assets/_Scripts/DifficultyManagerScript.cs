using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManagerScript : MonoBehaviour
{
    public enum Difficulty { Easy, Medium, Hard };
    public Difficulty selectedDifficulty = Difficulty.Easy;

    //private void Awake()
    //{
        // Load the previously selected difficulty from PlayerPrefs
        //if (PlayerPrefs.HasKey("SelectedDifficulty"))
        //{
        //    int storedDifficulty = PlayerPrefs.GetInt("SelectedDifficulty");
        //    HandleDifficultyChange(storedDifficulty);
        //}
        //else
        //{
            //HandleDifficultyChange(0);
            //selectedDifficulty = Difficulty.Easy;
        //}
    //}

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

    public void HandleDifficultyChange(int val)
    {
        if (val == 0)
        {
            selectedDifficulty = Difficulty.Easy;
            Debug.Log("Easy was selected:");
        }
        if (val == 1)
        {
            selectedDifficulty = Difficulty.Medium;
            Debug.Log("Medium was selected:");
        }
        if (val == 2)
        {
            selectedDifficulty = Difficulty.Hard;
            Debug.Log("Hard was selected:");
        }

        // Store the selected difficulty level in PlayerPrefs
        PlayerPrefs.SetInt("SelectedDifficulty", val);
    }
}
