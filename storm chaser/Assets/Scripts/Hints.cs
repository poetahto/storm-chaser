using UnityEngine;

public class Hints : MonoBehaviour
{
    [SerializeField] private ObstacleSpawner mainSpawner = null;
    [SerializeField] private float fadeTime = 1f;

    private bool _hintActive = false;
    
    public void ShowHint(string message)
    {
        if (_hintActive)
            return;

        _hintActive = true;
        mainSpawner.StopSpawning();
        UIElementController.Instance.DisplayText(message, fadeTime);
    }

    public void ClearHints()
    {
        _hintActive = false;
        mainSpawner.StartSpawning();
        UIElementController.Instance.ClearText(fadeTime);
    }
}
