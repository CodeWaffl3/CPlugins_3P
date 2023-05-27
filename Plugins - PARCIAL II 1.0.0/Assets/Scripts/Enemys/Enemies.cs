using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [Header("Stats")] 
    public int health;

    [SerializeField] private float movementSpeed = 4;
    [SerializeField] private float targetClose = 20;
    public Transform target;
    public Transform tentacles;

    private Animator _animator;
    private bool readyToAttack;

    void Start()
    {
        
        readyToAttack = true;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        FollowPlayer();
        AttackPlayer();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FollowPlayer()
    {
        if ((target.position - transform.position).magnitude < targetClose && (target.position - transform.position).magnitude > 1f)
        {
            var step =  movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x,0, target.position.z), step);
            transform.LookAt(target);
            _animator.SetBool("isWalking", true);
            tentacles.position = new Vector3(1.56629109f,-12.2399998f,112f);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }

    private void AttackPlayer()
    {
        var attackRange = targetClose - 10f;
        if (((target.position - transform.position).magnitude < attackRange) && readyToAttack)
        {
            readyToAttack = false;
            _animator.SetBool("IsInRangeAttack", true);
            Invoke(nameof(ResetCooldownAttack), 2.15f);
        }
    }

    private void ResetCooldownAttack()
    {
        readyToAttack = true;
        _animator.SetBool("IsInRangeAttack", false);
    }
    
}
