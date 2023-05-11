using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealBoots : Item
{
    public override void OnActivation()
    {
        plr = GameObject.FindWithTag("Player").GetComponent<Player>();
        plr.moveSpeed += 1;
    }
}
