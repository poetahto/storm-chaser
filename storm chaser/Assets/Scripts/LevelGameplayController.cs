using System.Collections;
using UnityEngine;

public class LevelGameplayController : MonoBehaviour
{
    [SerializeField] private Level currentLevel = null;
    [SerializeField] private Level levelUI = null;
    [SerializeField] private Transform cameraTracker = null;
    [SerializeField] private Transform finishLine = null;
    [SerializeField] private ObstacleSpawner spawner = null;

    [SerializeField] private float initialSpawnRatePerMin = 10;
    [SerializeField] private float finalSpawnRatePerMin = 60;
    
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
    }

    private void Start()
    {
        UIElementController.Instance.BindProgressBar(this);
        StartCoroutine(SpawnObstacles());
        
        _totalDistance = CurrentDistance;
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            spawner.SpawnObject();
            yield return new WaitForSeconds(60 / Mathf.Lerp(initialSpawnRatePerMin, finalSpawnRatePerMin, PercentComplete));
        }
    }
}