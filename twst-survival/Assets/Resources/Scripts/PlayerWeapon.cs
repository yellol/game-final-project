using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float shootCD = 0.2f;
    public bool catchInput = true;
    
    private Transform _source;
    private Player _stats;
    private float _shootTimer = 0f;
    private Camera _camera;
    private Sprite _projectile;
    private GameObject _chosenProjectile;
    private GameObject _gm;


    // Start is called before the first frame update
    void Awake()
    {
        _gm =  GameObject.FindWithTag("GameController");
        _stats = gameObject.GetComponent<Player>();
        _source = gameObject.transform;
        _camera = Camera.main;
        _projectile = Resources.Load<Sprite>("Art/Projectiles/SpearPlayerProjectile");
        _chosenProjectile = Resources.Load<GameObject>("Prefabs/Projectiles/DefaultPlayerProjectile");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButton("Shoot") && _shootTimer <= 0)
        {
            if (catchInput)
            {
                StartCoroutine(Burst());
                _shootTimer = shootCD;
            }
        }
        else
        {
            if (_shootTimer >= 0)
            {
                _shootTimer -= Time.deltaTime;
            }
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
            //ShootBullet.Shoot(so, 0, shootDirection, _stats.damage, 20f, 1f, "Enemy", _projectile, 1, _gm, false);
            ShootBullet.ShootWithPrefab(so, 0, shootDirection, _chosenProjectile, _gm);
            yield return new WaitForSeconds(0.1f); //may change this for fire rate or some other stat
        }
        
    }

    public void SelectNewProjectile(string choice)
    {
        
        switch (choice)
        {
            case "Spear":
                Debug.Log("Spear chosen.");
                _chosenProjectile = Resources.Load<GameObject>("Prefabs/Projectiles/SpearPlayerProjectile");
                break;
            case "Hammer":
                _chosenProjectile = Resources.Load<GameObject>("Prefabs/Projectiles/HammerPlayerProjectile");
                break;
            case "Default":
                _chosenProjectile = Resources.Load<GameObject>("Prefabs/Projectiles/DefaultPlayerProjectile");
                break;
        }
    }
}
