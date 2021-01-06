using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Collider2D spawningArea = null;
    [SerializeField] private Rigidbody2D[] obstacles = null;

    private GameObject _poolParent;
    private Queue<Rigidbody2D> _obstaclePool;
    private Vector2 _spawnPosition;

    private void OnGUI()
    {
        if (GUILayout.Button("Spawn Obstacle"))
        {
            SpawnObject();
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
            obj.GetComponent<Collider2D>().isTrigger = true;
            _obstaclePool.Enqueue(obj);
            obj.gameObject.SetActive(false);
            obj.isKinematic = false;
        }
    }

    public void SpawnObject()
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

        randomObstacle.velocity = Vector2.left * 10;
    }
}
