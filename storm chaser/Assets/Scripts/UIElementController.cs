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
    }

    public void TogglePauseMenu()
    {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
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
}
