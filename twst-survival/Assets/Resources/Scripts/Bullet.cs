using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Target {Player, Enemy}
    
    public Target target;
    [HideInInspector] public Vector2 dir;
    [HideInInspector] public float rotation;
    [HideInInspector] public int damage;
    [HideInInspector] public float speed;
    [HideInInspector] public float lifetime = 0f;
    [HideInInspector] public bool piercing = false;

    private Transform _sprite;
    private int[] _hitList;
    private int _hitListIndex = 0;

    [SerializeField][Range(0, 4)] int angleCorrection = 0; //for snapping the sprite in the correct direction

    // Update is called once per frame

    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime; //kill the projectile when running out of time
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (target == Target.Player) //makes projectile mutually exclusive so it does not register hits on both enemies and player
        {
            if (col.CompareTag("Player"))
            {
                int identifier = col.GetInstanceID();
                bool found = false;
                col.GetComponent<Player>().TakeDamage(damage);
                if (piercing) //if the projectile is piercing, make sure it does not trigger damage more than once (im not even sure if this is needed)
                {
                    for (int i = 0; i < _hitListIndex; i++)
                    {
                        if (_hitList[_hitListIndex] == identifier)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        _hitList[_hitListIndex] = identifier;
                        _hitListIndex++;
                        col.GetComponent<Player>().TakeDamage(damage);
                    }
                }
                else
                {
                    col.GetComponent<Player>().TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        } else if (target == Target.Enemy)
        {
            col.GetComponent<EnemyCollision>().TakeDamage(damage);
            //put enemy code here
        }
    }

    public void FixRotation() //snaps the sprite to the correct shooting direction
    {
        _sprite = transform.Find("ProjectileSprite");
        _sprite.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - (45f*angleCorrection));
    }
}
