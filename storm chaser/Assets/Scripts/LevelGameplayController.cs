using System;
using UnityEngine;

public class LevelGameplayController : MonoBehaviour
{
    [SerializeField] private Level currentLevel = null;
    [SerializeField] private Level levelUI = null;
    [SerializeField] private Transform cameraTracker = null;
    [SerializeField] private Transform finishLine = null;
    [SerializeField] private Texture2D mouseImage = default;
    
    private float CurrentDistance => finishLine.position.x - cameraTracker.position.x;
    private float _totalDistance;

    public float PercentComplete => Mathf.Clamp01(1 - CurrentDistance / _totalDistance);

    public void RestartLevel()
    {
        currentLevel.Load();
    }
    
    private void Awake()
    {
        levelUI.Load();
        Cursor.SetCursor(mouseImage, new Vector2(4, -4), CursorMode.Auto);
    }

    private void Start()
    {
        UIElementController.Instance.BindProgressBar(this);
        
        _totalDistance = CurrentDistance;
    }

    private void Update()
    {
        Debug.Log($"finishline: {finishLine.position.x} trakcer: {cameraTracker.position.x} ");
    }
}