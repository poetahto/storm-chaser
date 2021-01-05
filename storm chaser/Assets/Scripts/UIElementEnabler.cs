using UnityEngine;

public class UIElementEnabler : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu = null;
    [SerializeField] private CanvasGroup progressBar = null;

    public static UIElementEnabler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void TogglePauseMenu()
    {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }

    public void SetProgressBar(bool active)
    {
        progressBar.gameObject.SetActive(active);
    }
}
