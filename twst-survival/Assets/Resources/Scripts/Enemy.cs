using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int healthPoints;
    public string enemyName;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            //put death explosion or animation here!
            Destroy(gameObject);
        }
        return;
    }
}
