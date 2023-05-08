using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //this will be used to manage game stats (waves, money, events, maybe shops)
    private static GameManager _instance;

    [HideInInspector] public int abyssalWisps = 0;
    [HideInInspector] public int wave = 0;


    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("GM is null");
            }
            return _instance;
        }
    }
    
    
    // initialize singleton
    void Awake()
    {
        _instance = this;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
