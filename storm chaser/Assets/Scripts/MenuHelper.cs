﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHelper : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private float fadeTime = 1;
    
    private void Start()
    {
        FadeIn(fadeTime);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void LoadLevel(Level level)
    {
        level.Load();
    }

    public void UnloadLevel(Level level)
    {
        level.UnloadAsync();
    }

    public void FadeOut(float time, Action callback)
    {
        LeanTween.alphaCanvas(blackScreen, 1, time).setOnComplete(callback);
    }

    public void FadeIn(float time)
    {
        LeanTween.alphaCanvas(blackScreen, 0, time);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        FadeOut(0.5f, ()=> SceneManager.LoadScene(SceneManager.GetSceneAt(0).buildIndex));
    }
}
