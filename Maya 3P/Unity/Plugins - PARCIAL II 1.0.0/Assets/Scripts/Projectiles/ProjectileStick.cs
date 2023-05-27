using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ProjectileStick : MonoBehaviour
{
    private Rigidbody rb;
    [FormerlySerializedAs("damage")] [SerializeField] private int projectileDamage;

    private bool targetHit;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke(nameof(ObjectUnspawn),20f);
    }

    private void OnCollisionEnter(Collision other)
    {
        //Only Stick to first target
        if (targetHit)
        {
            return;
        }
        else
        {
            targetHit = true;
        }

        if (other.gameObject.GetComponent<Enemies>() != null)
        {
            Enemies enemy = other.gameObject.GetComponent<Enemies>();
            enemy.TakeDamage(projectileDamage);
            Destroy(gameObject);
        } 
        else if(other.gameObject.GetComponent<Minions>() != null)
        {
            Minions minions = other.gameObject.GetComponent<Minions>();
            minions.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }

        rb.isKinematic = true;
        
        //Movimiento con el target
        transform.SetParent(other.transform);
    }
    
    void ObjectUnspawn()
    {
        Destroy(gameObject);
    }
}
