using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }

    [SerializeField] private Level level;
    [SerializeField] private Button mainButton;
    [SerializeField] private Button difficultyButton;
    [SerializeField] private TMP_Text difficultyButtonText;
    [SerializeField] private MenuHelper helper;
    [SerializeField] private float fadeTime = 1;
    
    public bool locked;

    public Difficulty DifficultyLevel;
    
    private void Awake()
    {
        mainButton.interactable = !locked;
        difficultyButton.interactable = !locked;
        difficultyButtonText.text = DifficultyLevel.ToString();
    }

    public void Unlock()
    {
        locked = false;
        mainButton.interactable = true;
        difficultyButton.interactable = true;
    }

    public void Lock()
    {
        locked = true;
        mainButton.interactable = false;
        difficultyButton.interactable = false;
    }

    public void CycleDifficulty()
    {
        int nextDifficulty = (int) DifficultyLevel + 1;
        var enumNames = Enum.GetNames(typeof(Difficulty));
        
        if (nextDifficulty >= enumNames.Length)
        {
            DifficultyLevel = Difficulty.Easy;
        }
        else
        {
            DifficultyLevel = (Difficulty) Enum.Parse(typeof(Difficulty), enumNames[nextDifficulty]);
        }
        
        difficultyButtonText.text = DifficultyLevel.ToString();
    }

    public void LoadLevel()
    {
        PlayerPrefs.SetInt("Difficulty", (int) DifficultyLevel);
        helper.FadeOut(fadeTime, () => level.Load());   
    }
}