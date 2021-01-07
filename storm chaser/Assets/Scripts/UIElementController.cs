using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIElementController : MonoBehaviour
{
    [SerializeField] private CanvasGroup black = null;
    
    [SerializeField] private CanvasGroup pauseMenu = null;
    [SerializeField] private ProgressBar progressBar = null;
    
    [SerializeField] private TMP_Text specialText = null;
    [SerializeField] private CanvasGroup specialTextCanvasGroup;
    
    [SerializeField] private TMP_Text text = null;
    [SerializeField] private CanvasGroup textCanvasGroup;

    public static UIElementController Instance;

    private void Awake()
    {
        Instance = this;
        textCanvasGroup.alpha = 0;
        specialTextCanvasGroup.alpha = 0;
        pauseMenu.alpha = 0;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;
    }

    private void SetCanvasGroup(CanvasGroup group, bool active)
    {
        if (LeanTween.isTweening(pauseMenu.gameObject))
            return;
            
        if (active)
        {
            group.interactable = true;
            group.blocksRaycasts = true;
            LeanTween.alphaCanvas(pauseMenu, 1, 0.2f).setOnComplete(() => Time.timeScale = 0);
        }
        else
        {
            Time.timeScale = 1;
            group.interactable = false;
            group.blocksRaycasts = false;
            LeanTween.alphaCanvas(pauseMenu, 0, 0.2f);
        }
    }

    public void TogglePauseMenu()
    {
        SetCanvasGroup(pauseMenu, !pauseMenu.interactable);
    }

    public void BindProgressBar(LevelGameplayController controller)
    {
        progressBar.Bind(controller);
    }

    public void DisplaySpecialText(string message, float fadeTime)
    {
        specialText.text = message;
        StartCoroutine(TitleCard(fadeTime));
    }

    private IEnumerator TitleCard(float time)
    {
        LeanTween.alphaCanvas(specialTextCanvasGroup, 1, time);
        yield return new WaitForSeconds(time);
        LeanTween.alphaCanvas(specialTextCanvasGroup, 0, time);
    }

    public void DisplaySubText(string message, float fadeTime)
    {
        text.text = message;
        LeanTween.alphaCanvas(textCanvasGroup, 1, fadeTime);
    }

    public void ClearSubText(float fadeTime)
    {
        LeanTween.alphaCanvas(textCanvasGroup, 0, fadeTime);
    }

    public void FadeOut(float time, Action callback)
    {
        LeanTween.alphaCanvas(black, 1, time).setOnComplete(callback);
    }

    public void FadeIn(float time)
    {
        LeanTween.alphaCanvas(black, 0, time);
    }

    public void ResetTime()
    {
        Time.timeScale = 1;
    }
}
