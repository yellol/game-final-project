using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using Unity.Mathematics;
using UnityEngine;

public class SpawnHelper : MonoBehaviour
{

    public GameObject enemyToSpawn;
    private Animator _animator;
    private static readonly int Spawn = Animator.StringToHash("Spawn");

    void Awake()
    {
        if (!enemyToSpawn)
        {
            enemyToSpawn = Resources.Load<GameObject>("Prefabs/Enemies/Chaser");
        }

        _animator = gameObject.GetComponent<Animator>();
    }
    
    void OnWindUpEnd() 
    {
        Instantiate(enemyToSpawn, transform.position, quaternion.identity);
        _animator.SetBool(Spawn, true);
    }
    

    void OnExplosionEnd()
    {
        Destroy(gameObject);
    }
}
