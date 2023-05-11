using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;

    public int wispCost = 0;
    //public int amount;

    protected Player plr;
    protected PlayerWeapon plrWeapon;
    
    private UIManager _uim;

    public virtual void OnActivation()
    {
        //change in inherited scripts
    }
}
