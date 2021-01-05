using UnityEngine;

public class LevelGameplayController : MonoBehaviour
{
    [SerializeField] private Level levelUI = null;
    [SerializeField] private Transform cameraTracker = null;
    [SerializeField] private Transform finishLine = null;

    private float CurrentDistance => finishLine.position.x - cameraTracker.position.x;
    private float _totalDistance;

    public float PercentComplete => Mathf.Clamp01(1 - CurrentDistance / _totalDistance); 
    
    private void Awake()
    {
        levelUI.Load();
    }

    private void Start()
    {
        UIElementController.Instance.BindProgressBar(this);

        _totalDistance = CurrentDistance;
    }
}