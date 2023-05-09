using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    //goal: find a way to have set layouts or have an even distribution of enemies spawn in each wave

    public int enemyCount = 0;
    public int enemyMax = 24;
    public bool debounce = false;

    public List<int> enemyPool = new List<int>();
    
    private float _xMin = -12;
    private float _yMin = -11;
    private float _xMax = 12;
    private float _yMax = 11;
    
    //dimensions of arena: 27 x 26

    //temp variable just to test spawning
    private float _spawnDelay = 12f;
    private float _spawnTimeCache;
    
    //load enemies types here!
    private GameObject _chaser;
    private GameObject _spawner;
    private GameManager _gm;
    
    
    //spawn position layouts (randomized!)
    private readonly String[] _layout = {"Quadrant", "SweepHorizontal", "SweepVertical", "SweepDiagonal"};

    private readonly Vector2[] _quadrants = { new Vector2(10, 10), new Vector2(-10, 10), new Vector2(-10, -10), new Vector2(10, -10) };

    private void Awake()
    {
        _gm = transform.parent.gameObject.GetComponent<GameManager>();
        _spawnTimeCache = Time.time + _spawnDelay;
        _spawner = Resources.Load<GameObject>("Prefabs/Enemies/SpawnAnimation");
        _chaser = Resources.Load<GameObject>("Prefabs/Enemies/Chaser");
        
        StartCoroutine(Spawn());
    }

    void Update()
    {
       /* if (Time.time > _spawnTimeCache)
        {
            StartCoroutine(Spawn());
           // Debug.Log(enemyCount);
            _spawnTimeCache = Time.time + _spawnDelay;
        } */
    }

    public void EnemyDied()
    {
        if (enemyPool.Count <= 0)
        {
            StartCoroutine(CatchEnemyCountException());
        }
    }

    IEnumerator CatchEnemyCountException()
    {
        yield return new WaitForSeconds(0.5f);
        if (enemyCount <= 0 && !debounce)
        {
            enemyCount = 0;
            debounce = true;
            StartCoroutine(_gm.TransitionWave());
        }
    }

    public IEnumerator Spawn()
    {
        int quadrant = 0;
        int r = Random.Range(0, _layout.Length);
        String chosenLayout = _layout[r];
        Debug.Log(chosenLayout);

        for (var i = 0; i < enemyMax; i++)
        {

            float xPos = 26/(float)enemyMax;
            float yPos = 26/(float)enemyMax;
            
            switch (chosenLayout)
            {
                case "Quadrant":
                    xPos = _quadrants[quadrant].x;
                    yPos = _quadrants[quadrant].y;
                    break;
                case "SweepHorizontal":
                    xPos = xPos * (i+1) - (26f/2f);
                    yPos = Random.Range(-3, 5);
                    break;
                case "SweepVertical":
                    yPos = yPos * (i+1) - (26f/2f);
                    xPos = Random.Range(-3, 5);
                    break;
                case "SweepDiagonal":
                    yPos = (yPos * (i+1)) - (26/2);
                    xPos = (xPos * (i+1)) - (26/2);
                    break;
                default:
                    xPos = _quadrants[quadrant].x;
                    yPos = _quadrants[quadrant].y;
                    break;
            }
            
            Instantiate(_spawner, new Vector3(Mathf.Clamp(xPos*Random.Range(0.8f,1.2f),-12, 12), Mathf.Clamp(yPos*Random.Range(0.8f,1.2f), -10, 14), 0), quaternion.identity);
            quadrant++;
            if (quadrant > 3)
            {
                quadrant = 0;
            }
            yield return new WaitForSeconds(0.1f);

        }
    }

}
