using UnityEngine;

public class UIElementController : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu = null;
    [SerializeField] private ProgressBar progressBar = null;

    public static UIElementController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void TogglePauseMenu()
    {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }

    public void BindProgressBar(LevelGameplayController controller)
    {
        progressBar.Bind(controller);
    }
}
