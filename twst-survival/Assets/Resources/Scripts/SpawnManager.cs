using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //goal: find a way to have set layouts or have an even distribution of enemies spawn in each wave
    private float _xMin;
    private float _yMin;
    private float _xMax;
    private float _yMax;
    
    //load enemies types here!
    private GameObject _chaser;

    private void Awake()
    {
        _chaser = Resources.Load<GameObject>("Prefabs/Enemies/Chaser");
    }
}
