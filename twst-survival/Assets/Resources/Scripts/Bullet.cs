using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public enum Target {Player, Enemy}
    
    public Target target;
    public bool piercing = true;
    public float speed;
    public int damage;
    public float lifetime = 0f;
    [HideInInspector] public Vector2 dir;
    [HideInInspector] public float rotation;
    [HideInInspector] public bool spawnAnimation = false;

    private IObjectPool<GameObject> b_pool;

    private Transform _sprite;
    private List<int> hitList = new List<int>();
    private int _damageCached;
    private Animator _anim;
    private GameObject _spawnAnim;
    private bool _despawning = false;

    [Range(0, 4)] public int angleCorrection = 0; //for snapping the sprite in the correct direction
    private static readonly int Despawn = Animator.StringToHash("Despawn");

    // Update is called once per frame

    public void Awake()
    {
        //Debug.Log(target);
        _spawnAnim = transform.Find("SpawnAnimation").gameObject;
        if (spawnAnimation)
        {
            _spawnAnim.SetActive(true);
        }
        //for consistency's sake
        _anim = GetComponentInChildren<Animator>();
        _anim.enabled = false; //disabled during lifetime in order to change the bullet sprite!
        _damageCached = damage;
        StartCoroutine(Lifetime()); //start counting down as soon as instance is made
    }
    public void SetPool(IObjectPool<GameObject> p)
    {
        b_pool = p;
    }
    
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (target == Target.Enemy) //makes projectile mutually exclusive so it does not register hits on both enemies and player
        {
            if (col.CompareTag("Enemy"))
            {
               // Debug.Log("hi");
                int identifier = col.GetInstanceID();
                if (piercing) //if the projectile is piercing, make sure it does not trigger damage more than once (im not even sure if this is needed)
                {
                    if (!hitList.Contains(identifier))
                    {
                        col.GetComponent<Enemy>().TakeDamage(damage);
                        hitList.Add(identifier);
                    }
                }
                else
                {
                    col.GetComponent<Enemy>().TakeDamage(damage);
                    BeginDespawn();
                }
            }
        } else if (target == Target.Player)
        {
            col.GetComponent<Player>().TakeDamage(damage);
            //put player code here
        }
    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(0.1f); //buffer time so it doesnt despawn instantly
        yield return new WaitForSeconds(lifetime); //wait for specified time
        BeginDespawn(); //play despawnming animation and stop
    }

    void BeginDespawn()
    {
        _anim.enabled = true;
        if (!_despawning)
        {
            _anim.SetTrigger(Despawn);
            if (spawnAnimation)
            {
                _spawnAnim.SetActive(false);
            }

            damage = 0;
            speed = 0;
            _despawning = true;
        }
    }

    public void RestartLifetime()
    {
        _anim.enabled = false;
        if (spawnAnimation)
        {
            _spawnAnim.SetActive(true);
        }

        _despawning = false;
        damage = _damageCached;
        hitList.Clear();
        StartCoroutine(Lifetime());
    }

    public void FixRotation() //snaps the sprite to the correct shooting direction
    {
        _sprite = transform.Find("ProjectileSprite");
        _sprite.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - (45f*angleCorrection));
    }

    public void OnDespawnAnimationEnd() //once animation is over in the child, function is called to bring this projectile back to the pool
    {
        b_pool?.Release(gameObject);
    }
}
