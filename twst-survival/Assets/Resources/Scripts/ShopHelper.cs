using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHelper : MonoBehaviour
{
    public GameObject heldItem;
    private ShopManager _shm;

    void Awake()
    {
        _shm = GetComponentInParent<ShopManager>();
    }

    public void SelectItem() //to interface nicely with unity's onclick buttons
    {
        _shm.ChooseItem(heldItem);
    }
}
