using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public TMPro.TMP_Dropdown difficultyDropdown;
    //private Dictionary _selectedDifficulty;

    private void Start()
    {
        difficultyDropdown.value = GameInstance.Instance.selectedDifficultyIndex;
    }

    public void onPlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void onOptionsPress()
    {
        Debug.Log("options was pressed");

    }

    public void OnDifficultyChange(int index)
    {
        GameInstance.Instance.HandleDifficultyChange(index);
    }
    public void onQuitPress()
    {
        Debug.Log("quit was pressed");
        Application.Quit();
    }
}
