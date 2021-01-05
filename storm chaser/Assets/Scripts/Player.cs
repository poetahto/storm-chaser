﻿using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Level debugEscapeLevel = default;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseUI();          
    }

    private void TogglePauseUI()
    {
        if (debugEscapeLevel.Loaded)
        {
            debugEscapeLevel.UnloadAsync();
        }
        else
        {
            debugEscapeLevel.LoadAsync();
        }
    }
}
