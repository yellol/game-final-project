using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssalElixir : Item
{
    public override void OnActivation()
    {
        plr = GameObject.FindWithTag("Player").GetComponent<Player>();
        plr.maxHealthPoints += 2;
    }
}
