using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private ObstacleDifficulty[] difficulties = null;

    [SerializeField] private Transform target = null;
    
    [SerializeField] private Collider2D spawningArea = null;
    [SerializeField] private Rigidbody2D[] obstacles = null;
    [SerializeField] private Vector2 spawnVelocity = Vector2.left;
    [SerializeField] private bool spawnWithCollision = false;
    [SerializeField] private bool spawnWithSpin = false;
    
    [SerializeField] private LevelGameplayController controller = null;

    private ObstacleDifficulty _curDif;
    private GameObject _poolParent;
    private Queue<Rigidbody2D> _obstaclePool;
    private Vector2 _spawnPosition;
    private bool _activelySpawning = false;

    public void SetDifficulty(int difficulty)
    {
        Debug.Log(difficulty);
        _curDif = difficulties[Mathf.Min(difficulty, difficulties.Length - 1) ];
    }
    
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
                yield return new WaitForSeconds(60 / Mathf.Lerp(
                    _curDif.initialSpawnRatePerMin, _curDif.finalSpawnRatePerMin, controller.PercentComplete));
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

        var veloMult = Mathf.Lerp(_curDif.initialVelocityMult, _curDif.finalVelocityMult, controller.PercentComplete);
        var velocity = spawnVelocity * veloMult;

        if (target != null)
        {
            
            velocity = ((Vector2) target.position - _spawnPosition).normalized * veloMult;
            velocity.y += Mathf.Lerp(_curDif.initialYBoost, _curDif.finalYBoost, controller.PercentComplete);;
        }

        randomObstacle.velocity = velocity;
        
        if (spawnWithSpin)
        {
            randomObstacle.AddTorque(Mathf.Lerp(_curDif.initialTorque, _curDif.finalTorque, controller.PercentComplete));
        }
    }
}
