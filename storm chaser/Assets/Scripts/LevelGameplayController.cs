using System;
using UnityEngine;

public class LevelGameplayController : MonoBehaviour
{
    [SerializeField] private Level currentLevel = null;
    [SerializeField] private int nextLevelIndex = 0;
    [SerializeField] private Level levelUI = null;
    [SerializeField] private Level mainMenu = null;
    [SerializeField] private Transform cameraTracker = null;
    [SerializeField] private LevelObjective objective = null;
    [SerializeField] private Texture2D mouseImage = default;
    
    private float CurrentDistance => objective.transform.position.x - cameraTracker.position.x;
    private float _totalDistance;

    public float PercentComplete => Mathf.Clamp01(1 - CurrentDistance / _totalDistance);

    public void RestartLevel()
    {
        currentLevel.Load();
    }
    
    private void Awake()
    {
        Debug.Log("LOaded with dif " + PlayerPrefs.GetInt("Difficulty"));
        
        levelUI.Load();
        objective.EndLevel += OnLevelEnd;
        Cursor.SetCursor(mouseImage, new Vector2(4, -4), CursorMode.Auto);
    }

    private void Start()
    {
        UIElementController.Instance.BindProgressBar(this);
        
        _totalDistance = CurrentDistance;
    }

    private void OnLevelEnd(object sender, EventArgs args)
    {
        PlayerPrefs.SetInt("FinishedLevelOne", 1);
        mainMenu.Load();
    }
}