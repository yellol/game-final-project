using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;


    private Rigidbody2D _rigidbody;
    private float _vx;
    private float _vy;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _vx = Input.GetAxisRaw("Horizontal");
        _vy = Input.GetAxisRaw("Vertical");
        if (_vx != 0 && _vy != 0) //make it so moving diagonally is the same as moving in one direction
        {
            _rigidbody.velocity = new Vector2(_vx * moveSpeed/1.25f, _vy * moveSpeed/1.25f);
        }
        else
        {
            _rigidbody.velocity = new Vector2(_vx * moveSpeed, _vy * moveSpeed);
        }
    }
}
