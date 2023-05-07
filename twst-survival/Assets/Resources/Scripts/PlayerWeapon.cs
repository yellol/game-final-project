using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public enum ProjectileTypes {Spear, Hammer, Default}

    public ProjectileTypes SelectedProjectile;
    public float shootCD = 1f;
    
    private Transform _source;
    private Player _stats;
    private float _shootTimer = 0f;
    private Camera _camera;
    private Sprite _projectile;
    private GameObject _gm;

    // Start is called before the first frame update
    void Awake()
    {
        _gm =  GameObject.FindWithTag("GameController");
        _stats = gameObject.GetComponent<Player>();
        _source = gameObject.transform;
        _camera = Camera.main;
        _projectile = Resources.Load<Sprite>("Art/Projectiles/DefaultPlayerProjectile");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Shoot") && _shootTimer <= 0)
        {
            StartCoroutine(Burst());
            _shootTimer = shootCD;
        }
        else
        {
            _shootTimer -= Time.deltaTime;
        }
    }

    IEnumerator Burst()
    {
        for (int n = 0; n < _stats.amountOfProjectiles; n++)
        {
            Vector3 so = _source.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
            Vector3 m = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootDirection =  m - so;
            shootDirection.Normalize();
            ShootBullet.Shoot(so, 0, shootDirection, _stats.damage, 20f, 1f, "Enemy", _projectile, 1, _gm);
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
