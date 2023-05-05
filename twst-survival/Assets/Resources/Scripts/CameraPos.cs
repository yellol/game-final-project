using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public Transform target;
    
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        float _x = transform.position.x;
        float _y = transform.position.y;
        transform.position = new Vector3((_x + Input.mousePosition.x) / 2, (_y + Input.mousePosition.y) / 2, 0);
    }
}
