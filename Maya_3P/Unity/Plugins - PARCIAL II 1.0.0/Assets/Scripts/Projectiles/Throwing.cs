using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Throwing : MonoBehaviour
{
    
    [FormerlySerializedAs("camera")] [Header("References")] 
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrows;
    [SerializeField] private float shootCooldown;

    [Header("Throwing")]
    [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] private float shootForce;
    [SerializeField] private float shootUpwardsForce;

    private bool readyToShoot;
    
    void Start()
    {
        readyToShoot = true;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(shootKey) && readyToShoot && totalThrows > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);
        
        
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        
        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position,cam.forward, out hit, 500F))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }
        
        //ADD force to Rb
        Vector3 forceToAdd = forceDirection * shootForce + transform.up * shootUpwardsForce;
        
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        
        totalThrows--;

        Invoke(nameof(ResetCooldown), shootCooldown);
    }

    private void ResetCooldown()
    {
        readyToShoot = true;
    }
}
