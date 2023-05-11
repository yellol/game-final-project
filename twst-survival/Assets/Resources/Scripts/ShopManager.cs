using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    public GameObject[] Panels;
    public int amountOfItems = 3;
    private GameObject[] ItemPool;
    private GameObject[] ChosenItems;
    private GameManager _gm;
    private UIManager _uim;
    private TMP_Text _wispAmount;

    private GameObject _ChosenItem;

    void Awake()
    {
        _gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        _uim = GameObject.FindWithTag("GameController").GetComponentInChildren<UIManager>();
        _wispAmount = transform.Find("WispAmount").gameObject.GetComponent<TMP_Text>();
        ItemPool = Resources.LoadAll<GameObject>("Prefabs/ItemData");
        InitializeShop();
    }

    private void OnEnable()
    {
        _wispAmount.text = _gm.abyssalWisps.ToString();
    }


    void InitializeShop()
    {
        ChosenItems = new GameObject[amountOfItems];
        for (int i = 0; i < amountOfItems; i++)
        {
            int randomItem = Random.Range(0, ItemPool.Length);
            ChosenItems[i] = ItemPool[randomItem];
        }

        for (int n = 0; n < Panels.Length; n++)
        {
            InitializePanel(Panels[n], ChosenItems[n]);
        }
    }

    void InitializePanel(GameObject panel, GameObject heldItem)
    {
        var spr = panel.transform.Find("ItemSprite").gameObject.GetComponent<Image>();
        var nme = panel.transform.Find("ItemName").gameObject.GetComponent<TMP_Text>();
        var desc = panel.transform.Find("Description").gameObject.GetComponent<TMP_Text>();
        var cost = panel.transform.Find("Cost").gameObject.GetComponent<TMP_Text>();
        var data = heldItem.GetComponent<Item>();    
        
        spr.sprite = data.itemSprite;
        nme.text = data.itemName;
        desc.text = data.itemDescription;
        cost.text = data.wispCost.ToString();
        panel.GetComponent<ShopHelper>().heldItem = heldItem;
    }

    public void ChooseItem(GameObject chosen)
    {
        var text = transform.Find("ChosenItemText").gameObject.GetComponent<TMP_Text>();
        _ChosenItem = chosen;
        text.text = "Chosen Item: " + chosen.GetComponent<Item>().itemName;
    }

    public void ConfirmItem()
    {
        _gm.AddItem(_ChosenItem);
        _uim.OpenPanel(gameObject);
        Debug.Log("Confirmed!?");
    }
    
}
