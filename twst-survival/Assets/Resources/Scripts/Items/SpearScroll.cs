using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScroll : Item
{
    public override void OnActivation()
    {
        plrWeapon = GameObject.FindWithTag("Player").GetComponent<PlayerWeapon>();
        plrWeapon.SelectNewProjectile("Spear");
    }
}
