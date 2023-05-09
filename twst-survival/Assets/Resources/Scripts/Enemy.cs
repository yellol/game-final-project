using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int healthPoints;
    public string enemyName;
    private SpawnManager _spawnManager;
    private bool _invuln;
    
    void Start()
    {
        _spawnManager = GameObject.FindWithTag("GameController").GetComponentInChildren<SpawnManager>();
        _spawnManager.enemyPool.Add(gameObject.GetInstanceID()); //stores the unique id of the enemy within the spawner
        //The ID is needed because otherwise, when removing enemies from the list there is a chance to remove two entities instead of one (because it removes itself and duplicates as well)
        //This makes sure the removal of itself from the pool is contained to solely its own death.
    }
    

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            //put death explosion or animation here!
            if (damage != 0) {
                _spawnManager.enemyPool.Remove(gameObject.GetInstanceID()); 
                _spawnManager.EnemyDied();
            }
            Destroy(gameObject);
        }
    }
    
}
