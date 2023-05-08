using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    //goal: find a way to have set layouts or have an even distribution of enemies spawn in each wave
    private float _xMin = -12;
    private float _yMin = -12;
    private float _xMax = 12;
    private float _yMax = 12;
    
    //temp variable just to test spawning
    private float _spawnDelay = 5f;
    private float _spawnTimeCache;
    
    //load enemies types here!
    private GameObject _chaser;
    private GameObject _spawner;

    private void Awake()
    {
        _spawnTimeCache = Time.time + _spawnDelay;
        _spawner = Resources.Load<GameObject>("Prefabs/Enemies/SpawnAnimation");
        _chaser = Resources.Load<GameObject>("Prefabs/Enemies/Chaser");
    }

    void Update()
    {
        if (Time.time > _spawnTimeCache)
        {
            SpawnEnemy();
            _spawnTimeCache = Time.time + _spawnDelay;
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(_xMin, _xMax);
        float randomY = Random.Range(_yMin, _yMax);

        Instantiate(_spawner, new Vector3(randomX, randomY, 0), quaternion.identity);
    }
    
}
