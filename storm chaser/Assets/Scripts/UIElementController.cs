using TMPro;
using UnityEngine;

public class UIElementController : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu = null;
    [SerializeField] private ProgressBar progressBar = null;
    
    [SerializeField] private TMP_Text text = null;
    [SerializeField] private CanvasGroup textCanvasGroup;

    public static UIElementController Instance;

    private void Awake()
    {
        Instance = this;
        textCanvasGroup.alpha = 0;
    }

    public void TogglePauseMenu()
    {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }

    public void BindProgressBar(LevelGameplayController controller)
    {
        progressBar.Bind(controller);
    }

    public void DisplayText(string message, float fadeTime)
    {
        text.text = message;
        LeanTween.alphaCanvas(textCanvasGroup, 1, fadeTime);
    }

    public void ClearText(float fadeTime)
    {
        LeanTween.alphaCanvas(textCanvasGroup, 0, fadeTime);
    }
}
