using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public Transform target;

    private Camera _camera;
    
    // Update is called once per frame

    void Awake()
    {
        _camera = Camera.main; //get main camera
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        float _x = target.transform.position.x; //get position of player
        float _y = target.transform.position.y; 
        Vector3 _mouse = _camera.ScreenToWorldPoint(Input.mousePosition); //convert mouse position to world position
        
        //clamp position of camera to specific walls so you cant see that far
        transform.position = new Vector3(Mathf.Clamp((_x + _mouse.x) / 4, -15, 17), Mathf.Clamp((_y + _mouse.y) / 4, -15, 17), -1);
    }
}
