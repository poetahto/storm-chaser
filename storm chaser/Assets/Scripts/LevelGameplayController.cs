using System;
using UnityEngine;

public class LevelGameplayController : MonoBehaviour
{
    [SerializeField] private Player player = null;
    [SerializeField] private float fadeInTime = 1;
    [SerializeField] private float titlecardAnimationTime = 0.25f;
    [SerializeField] private Level currentLevel = null;
    [SerializeField] private int nextLevelIndex = 0;
    [SerializeField] private Level levelUI = null;
    [SerializeField] private Level mainMenu = null;
    [SerializeField] private Transform cameraTracker = null;
    [SerializeField] private LevelObjective objective = null;
    [SerializeField] private Texture2D mouseImage = default;

    [SerializeField] private ObstacleSpawner[] spawners = null;
    
    private float CurrentDistance => objective.transform.position.x - cameraTracker.position.x;
    private float _totalDistance;

    public float PercentComplete => Mathf.Clamp01(1 - CurrentDistance / _totalDistance);

    public void RestartLevel()
    {
        currentLevel.Load();
    }
    
    private void Awake()
    {
        foreach (var spawner in spawners)
        {
            spawner.SetDifficulty(PlayerPrefs.GetInt("Difficulty"));
        }

        player.SetDifficulty(PlayerPrefs.GetInt("Difficulty"));

        levelUI.Load();
        objective.EndLevel += OnLevelEnd;
        Cursor.SetCursor(mouseImage, new Vector2(4, -4), CursorMode.Auto);
    }

    private void Start()
    {
        string difficulty = "ERROR";
        int diffIndex = PlayerPrefs.GetInt("Difficulty");

        if (diffIndex == 0)
            difficulty = "Easy";
        else if (diffIndex == 1)
            difficulty = "Normal";
        else if (diffIndex == 2)
            difficulty = "Hard";

        UIElementController.Instance.BindProgressBar(this);
        UIElementController.Instance.DisplaySpecialText($"{currentLevel.name} - {difficulty}", titlecardAnimationTime);
        UIElementController.Instance.FadeIn(fadeInTime);
        
        _totalDistance = CurrentDistance;
    }

    private void OnLevelEnd(object sender, EventArgs args)
    {
        // PlayerPrefs.SetInt("FinishedLevelOne", 1);
        UIElementController.Instance.FadeOut(fadeInTime, ()=> mainMenu.Load());
    }
}