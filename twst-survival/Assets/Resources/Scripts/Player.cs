using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int damage = 20;
    public int amountOfProjectiles = 3;

    public float moveSpeed = 2f;
    public int healthPoints = 6;
    public int maxHealthPoints = 6;

    public bool InputAllowed;

    

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _moveSpeedMultiplier = 1f; //for slowing down using focus for more precise movement
    private float _vx;
    private float _vy;
    private bool _invuln = false;
    private bool _catchInput = true;
    private UIManager _uiM;
    private PlayerWeapon _weapon;
    private static readonly int Moving = Animator.StringToHash("Moving");
    


    // Start is called before the first frame update
    void Awake()
    {
        _uiM = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _weapon = GetComponent<PlayerWeapon>();
        StartCoroutine(InvulnFrames());
    }

    // Update is called once per frame
    void Update()
    {
        if (_catchInput)
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

            if (_vx != 0 && _vy != 0) //make it so moving diagonally is the same as moving in one direction
            {
                _rigidbody.velocity = new Vector2(_vx * (moveSpeed * _moveSpeedMultiplier) / 1.25f,
                    _vy * (moveSpeed * _moveSpeedMultiplier) / 1.25f);
            }
            else
            {
                _rigidbody.velocity = new Vector2(_vx * moveSpeed * _moveSpeedMultiplier,
                    _vy * moveSpeed * _moveSpeedMultiplier);
            }

        }
        else
        {
            _rigidbody.velocity = new Vector2(0, 0);
        }


        if (_vx != 0 || _vy != 0)
        {
            _animator.SetBool(Moving, true);
        }
        else
        {
            _animator.SetBool(Moving, false);
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
        //Debug.Log(_invuln);
        if (!_invuln) //invulnerability timer
        {
            healthPoints -= dmg;
            _uiM.RefreshUI();
            StartCoroutine(InvulnFrames());
        }
    }

    IEnumerator InvulnFrames()
    {
        _invuln = true;
        yield return new WaitForSeconds(0.25f);
        _invuln = false;
    }

    public void SwitchInputState()
    {
        _catchInput = !_catchInput;
        _weapon.catchInput = _catchInput;
    }
}
