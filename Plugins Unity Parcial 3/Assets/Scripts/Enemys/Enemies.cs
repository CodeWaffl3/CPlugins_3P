using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemies : MonoBehaviour
{
    [Header("Stats")] 
    public int health = 100;

    public Rigidbody rb;
    public Transform orientation;
    private Vector3 moveDirection;

    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float targetClose = 20;
    public Transform target;
    

    private Animator _animator;
    private bool readyToAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
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
            orientation.LookAt(target);
            transform.LookAt(target);
            moveDirection = orientation.forward * 1;
            rb.AddForce(moveDirection * movementSpeed * 10f, ForceMode.Force);
            _animator.SetBool("isWalking", true);
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
