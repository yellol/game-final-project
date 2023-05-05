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
        _camera = Camera.main;
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        float _x = target.transform.position.x;
        float _y = target.transform.position.y;
        Vector3 _mouse = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3((_x + _mouse.x) / 2, (_y + _mouse.y) / 2, -1);
    }
}
