using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    
    [SerializeField] private Collider2D spawningArea = null;
    [SerializeField] private Rigidbody2D[] obstacles = null;
    [SerializeField] private Vector2 spawnVelocity = Vector2.left;
    [SerializeField] private bool spawnWithCollision = false;
    [SerializeField] private bool spawnWithSpin = false;
    
    [SerializeField] private LevelGameplayController controller = null;
    [SerializeField] private float initialSpawnRatePerMin = 10;
    [SerializeField] private float finalSpawnRatePerMin = 60;
    
    private GameObject _poolParent;
    private Queue<Rigidbody2D> _obstaclePool;
    private Vector2 _spawnPosition;
    private bool _activelySpawning = false;

    public void StartSpawning()
    {
        _activelySpawning = true;
    }

    public void StopSpawning()
    {
        _activelySpawning = false;
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            if (!_activelySpawning)
            {
                yield return null;
            }
            else
            {
                SpawnObject();
                yield return new WaitForSeconds(60 / Mathf.Lerp(initialSpawnRatePerMin, finalSpawnRatePerMin, controller.PercentComplete));
            }
        }
    }

    private void Awake()
    {
        _obstaclePool = new Queue<Rigidbody2D>();
        _poolParent = new GameObject("Obstacle Pool");
        _spawnPosition = Vector2.zero;
        foreach (var obstacle in obstacles)
        {
            var obj = Instantiate(obstacle, _poolParent.transform, true);
            obj.GetComponent<Collider2D>().isTrigger = !spawnWithCollision;
            _obstaclePool.Enqueue(obj);
            obj.gameObject.SetActive(false);
            obj.isKinematic = false;
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    private void SpawnObject()
    {
        GetRandomSpawnPositionInArea();
        SpawnNextObstacle();
    }
    
    private void GetRandomSpawnPositionInArea()
    {
        var bounds = spawningArea.bounds;
        var spawnX = spawningArea.transform.position.x;
        var spawnY = Random.Range(bounds.min.y, bounds.max.y);

        _spawnPosition = new Vector2(spawnX, spawnY);
    }
    
    private void SpawnNextObstacle()
    {
        Rigidbody2D randomObstacle = _obstaclePool.Dequeue();
        _obstaclePool.Enqueue(randomObstacle);
        randomObstacle.transform.position = _spawnPosition;
        randomObstacle.gameObject.SetActive(true);

        var velocity = spawnVelocity * 10;

        if (target != null)
        {
            
            velocity = ((Vector2) target.position - _spawnPosition).normalized * Mathf.Lerp(5, 10, controller.PercentComplete);
            velocity.y += 2;
        }

        randomObstacle.velocity = velocity;
        
        if (spawnWithSpin)
        {
            randomObstacle.AddTorque(50);
        }
    }
}
