using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

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

    void Awake() //awake used to load all references
    {
        _walkSpeed= Random.Range(4f, 7f);
        _target = GameObject.FindWithTag("Player").transform;
        _anim = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _anim.SetBool(Spawning, true);
        _scale = transform.localScale;
    }

    void Update()
    {
        if (_activated) //when activated, chase the player
        {
            Vector2 chaseDir = _target.position - transform.position;
            Vector2 chaseDist = chaseDir;
            chaseDir.Normalize(); //get the direction
            _rigidbody.velocity = chaseDir * _walkSpeed;
            
            //Debug.Log(healthPoints);
            if (chaseDist.x < 0.25 && chaseDist.y < 0.25)
            {
                transform.localScale = new Vector3((_rigidbody.velocity.x > 0 ? 1 : -1), _scale.y, _scale.z); //flip sprite based on direction
            }

            _anim.SetBool(Moving, true);
        }
    }

    private void OnTriggerStay2D(Collider2D col) //when the player is inside the collider , deal damage (if active)
    {
        if (_activated)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Player>().TakeDamage(1);
            }
        }
    }

    public void OnSpawnAnimationEnd()
    {
        _anim.SetBool(Spawning, false);
        _activated = true;
    }
}
