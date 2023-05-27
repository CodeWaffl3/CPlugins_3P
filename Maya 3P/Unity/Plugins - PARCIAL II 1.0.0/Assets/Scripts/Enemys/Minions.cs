using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : MonoBehaviour
{
    [Header("Stats")] 
    public int health;

    [SerializeField] private float movementSpeed = 4;
    [SerializeField] private float targetClose = 20;
    public Transform target;
    

    [SerializeField] private Transform guardWalk;
    private int indexGuardWalk;
    [SerializeField] private Vector3[] positionsToGuard;
    private bool isFollowPlayer;
    
    private Animator _animator;
    private bool readyToAttack;
    

    void Start()
    {
        
        isFollowPlayer = false;
        indexGuardWalk = 0;
        if (positionsToGuard.Length < 0)
        {
            guardWalk.position = positionsToGuard[indexGuardWalk];
        }
        
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


    private void GuardWalk(bool followplayer)
    {
        if (followplayer)
        {
            _animator.SetBool("isWalking", true);
            var stepGuard =  (movementSpeed * 0.8f) * Time.deltaTime;
            if (Vector3.Distance(transform.position, guardWalk.position) < 0.5f)
            {
                if (indexGuardWalk < (positionsToGuard.Length - 1))
                {
                    indexGuardWalk++;
                    guardWalk.position = positionsToGuard[indexGuardWalk];
                }
                else
                {
                    indexGuardWalk = 0;
                    guardWalk.position = positionsToGuard[indexGuardWalk];
                }
            
            
                
            }
        
            if (!isFollowPlayer)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(guardWalk.position.x,transform.position.y, guardWalk.position.z), stepGuard);
                transform.LookAt(guardWalk);
            } 
        }
        
    }
    
    private void FollowPlayer()
    {
        //Te persigue
        if ((target.position - transform.position).magnitude < targetClose && (target.position - transform.position).magnitude > 0.7f)    
        {
            isFollowPlayer = true;
            var step =  movementSpeed * Time.deltaTime * 0.8f;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x,0, target.position.z), step);
            transform.LookAt(target);
            _animator.SetBool("isWalking", true);
        }
        else
        {
            //No te persigue
            GuardWalk(true);
        }
    }

    private void AttackPlayer()
    {
        var attackRange = targetClose - 10f;
        if (((target.position - transform.position).magnitude < attackRange) && readyToAttack && (target.position - transform.position).magnitude > 1)
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
