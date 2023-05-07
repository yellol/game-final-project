using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpriteHelper : MonoBehaviour
{
   //use this to return the parent object back to the pool
   private void OnAnimationEnd()
   {
      transform.parent.gameObject.GetComponent<Bullet>().OnDespawnAnimationEnd();
   }
}
