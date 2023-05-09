using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //this will be used to manage game stats (waves, money, events, maybe shops)

    [HideInInspector] public int abyssalWisps = 0;
    [HideInInspector] public int wave = 1;

    private SpawnManager _spm;
    private UIManager _uim;

    void Awake()
    {
        _spm = gameObject.GetComponentInChildren<SpawnManager>();
        _uim = gameObject.GetComponentInChildren<UIManager>();
    }

    public void NewWave()
    {
        //add enemies to pool here maybe...
        wave += 1;
        _spm.enemyMax++;
        _spm.debounce = false;
        _uim.RefreshWave();
        StartCoroutine(_spm.Spawn());
    }

    public IEnumerator TransitionWave()
    {
        //do any special stuff here
        yield return new WaitForSeconds(3f);
        NewWave();
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
