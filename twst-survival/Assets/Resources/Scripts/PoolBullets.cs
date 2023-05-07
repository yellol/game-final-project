using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolBullets : MonoBehaviour
{
    public int maxBullets = 500;
    public GameObject bulletPrefab;
    public IObjectPool<GameObject> BulletPool;
    private static readonly int Despawn = Animator.StringToHash("Despawn");


    // Start is called before the first frame update
    void Awake()
    {
        if (!bulletPrefab)
        {
            bulletPrefab = Resources.Load<GameObject>("Prefabs/DefaultProjectile");
        }

        BulletPool = new ObjectPool<GameObject>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet, maxSize: maxBullets);
    }

    private void GetBullet(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void ReleaseBullet(GameObject obj)
    {
        obj.SetActive(false);
        obj.GetComponentInChildren<Animator>().ResetTrigger(Despawn);
    }

    private void DestroyBullet(GameObject obj)
    {
        Destroy(obj);
    }

    private GameObject CreateBullet()
    {
        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bu = b.GetComponent<Bullet>();
        bu.SetPool(BulletPool);
        return b;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
