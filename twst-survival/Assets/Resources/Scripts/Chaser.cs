using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Chaser : Enemy
//enemies need box colliders AND rigidbodies to be collided with projectiles. please remember this.
{
    private Transform _target;
    private float _walkSpeed = 5f;
    private bool _activated;
    private Animator _anim;
    private Vector3 _scale;
    private Rigidbody2D _rigidbody; //consider putting this in Enemy instead in Chaser
    private static readonly int Spawning = Animator.StringToHash("Spawning");
    private static readonly int Moving = Animator.StringToHash("Moving");

    void Awake()
    {
        _target = GameObject.FindWithTag("Player").transform;
        _anim = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _anim.SetBool(Spawning, true);
        _scale = transform.localScale;
    }

    void Update()
    {
        if (_activated)
        {
            Vector2 chaseDir = _target.position - transform.position;
            chaseDir.Normalize();
            _rigidbody.velocity = chaseDir * _walkSpeed;
            
            //Debug.Log(healthPoints);
            transform.localScale = new Vector3((_rigidbody.velocity.x > 0 ? 1 : -1), _scale.y, _scale.z);
            _anim.SetBool(Moving, true);
        }
    }

    public void OnSpawnAnimationEnd()
    {
        _anim.SetBool(Spawning, false);
        _activated = true;
    }
}
