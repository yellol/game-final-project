using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
//class for use in enemies shooting bullets so i dont need to rewrite the function everytime, etc.
//reminder to add functions that add projectiles on death
{
    public void Shoot(GameObject proj, Vector3 pos, float offset, Vector2 dir, int damage, float spd, float time, string target)
    {
        //create a specified projectile prefab at a specified position + offset, using a direction calculated from subtracting two points within the world
        //REMINDER: write a version of this function using euler angles (use Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg)
        GameObject newBullet = Instantiate(proj, pos + new Vector3(dir.x * offset, dir.y * offset, 0), Quaternion.identity);
        Bullet bulletProps = newBullet.GetComponent<Bullet>();
        
        //set all properties of the bullet
        bulletProps.dir = dir;
        bulletProps.FixRotation();
        bulletProps.damage = damage;
        bulletProps.speed = spd;
        bulletProps.lifetime = time;
        switch (target) //set what type of entity the bullet will hit
        {
            case "Enemy":
                bulletProps.target = Bullet.Target.Enemy;
                break;
            case "Player":
                bulletProps.target = Bullet.Target.Player;
                break;
        }
        
    }
}
