using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
//class for use in enemies shooting bullets so i dont need to rewrite the function everytime, etc.
//reminder to add functions that add projectiles on death
{
    public static void Shoot(Vector3 pos, float offset, Vector2 dir, int damage, float spd, float time, string target, Sprite s, int ac, GameObject pool, bool piercing)
    {
        //create a specified projectile prefab at a specified position + offset, using a direction calculated from subtracting two points within the world
        //REMINDER: write a version of this function using euler angles (use Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg)
        
        //GameObject newBullet = Instantiate(proj, pos + new Vector3(dir.x * offset, dir.y * offset, 0), Quaternion.identity);
        
        // The above function was replaced with an object pool system provided by the gameobject arugment pool.
        // in this case, the pool argument would be fulfilled by a reference to Game Manager.
        
        if (pool)
        {
            GameObject newBullet = pool.GetComponent<PoolBullets>().BulletPool.Get();

            newBullet.transform.position = pos + new Vector3(dir.x * offset, dir.y * offset, 0);
            newBullet.transform.rotation = Quaternion.identity;

            Bullet bulletProps = newBullet.GetComponent<Bullet>();
            SpriteRenderer spr = newBullet.GetComponentInChildren<SpriteRenderer>();

            //set all properties of the bullet
            spr.sprite = s;
            bulletProps.dir = dir;
            bulletProps.angleCorrection = ac;
            bulletProps.FixRotation();
            bulletProps.damage = damage;
            bulletProps.speed = spd;
            bulletProps.lifetime = time;
            bulletProps.RestartLifetime();
            switch (target) //set what type of entity the bullet will hit
            {
                case "Enemy":
                    bulletProps.target = Bullet.Target.Enemy;
                    bulletProps.spawnAnimation = true;
                    break;
                case "Player":
                    bulletProps.target = Bullet.Target.Player;
                    break;
            }
        }

    }

    //a function designed to shoot using a premade prefab instead of several arguments
    public static void ShootWithPrefab(Vector3 pos, float offset, Vector2 dir, GameObject prefabBullet, GameObject pool)
    {
        if (pool)
        {
            GameObject newBullet = pool.GetComponent<PoolBullets>().BulletPool.Get();

            newBullet.transform.position = pos + new Vector3(dir.x * offset, dir.y * offset, 0);
            newBullet.transform.rotation = Quaternion.identity;

            Bullet prefabProps = prefabBullet.GetComponent<Bullet>();
            SpriteRenderer prefabSpr = prefabBullet.GetComponentInChildren<SpriteRenderer>(true);

            Bullet bulletProps = newBullet.GetComponent<Bullet>();
            SpriteRenderer spr = newBullet.GetComponentInChildren<SpriteRenderer>(true);

           //set all properties of the bullet
            
            
            
            spr.sprite = prefabSpr.sprite;
            bulletProps.dir = dir;
            bulletProps.angleCorrection = prefabProps.angleCorrection;
            bulletProps.FixRotation();
            bulletProps.damage = prefabProps.damage;
            bulletProps.speed = prefabProps.speed;
            bulletProps.lifetime = prefabProps.lifetime;
            bulletProps.piercing = prefabProps.piercing;
            bulletProps.RestartLifetime();
            switch (prefabProps.target) //set what type of entity the bullet will hit
            {
                case Bullet.Target.Enemy:
                    bulletProps.target = Bullet.Target.Enemy;
                    bulletProps.spawnAnimation = true;
                    break;
                case Bullet.Target.Player:
                    bulletProps.target = Bullet.Target.Player;
                    break;
            }
        }
    }
}
