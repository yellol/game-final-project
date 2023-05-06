using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int damage = 1;
    public int amountOfProjectiles = 3;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int healthPoints = 10;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _moveSpeedMultiplier = 1f; //for slowing down using focus for more precise movement
    private float _vx;
    private float _vy;



    // Start is called before the first frame update
    void Awake()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //DETECT SLOWDOWN BUTTON
        if (Input.GetButton("Focus"))
        {
            _moveSpeedMultiplier = 0.75f;
        }
        else
        {
            _moveSpeedMultiplier = 1f;
        }
        
        //MOVEMENT
        _vx = Input.GetAxisRaw("Horizontal");
        _vy = Input.GetAxisRaw("Vertical");
        
        if (_vx != 0 || _vy != 0)
        {
            _animator.SetBool("Moving", true);
        }
        else
        {
            _animator.SetBool("Moving", false);
        }
        
        if (_vx != 0 && _vy != 0) //make it so moving diagonally is the same as moving in one direction
        {
            _rigidbody.velocity = new Vector2(_vx * (moveSpeed*_moveSpeedMultiplier)/1.25f, _vy * (moveSpeed*_moveSpeedMultiplier)/1.25f);
        }
        else
        {
            _rigidbody.velocity = new Vector2(_vx * moveSpeed * _moveSpeedMultiplier, _vy * moveSpeed * _moveSpeedMultiplier);
        }
       
        
        if (_vx < 0) //flip sprite when moving in direction
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }
        else if (_vx > 0)
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        
        
    }

    public void TakeDamage(int dmg)
    {
        healthPoints -= dmg;
    }
    
}
