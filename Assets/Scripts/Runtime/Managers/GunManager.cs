using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Runtime.Managers;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    public float nextTimeToFire = 0f;
    
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private ParticleSystem muzzleFlash;

    private void Update()
    {
        if (PauseMenuManager.Instance.isGamePaused) return;

        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shot();
        }
    }

    private void Shot()
    {
        muzzleFlash.Play();
        
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit,range))
        {
           EnemyManager enemy = hit.transform.GetComponent<EnemyManager>();
           
           if(enemy != null)
           {
               enemy.TakeDamage(damage);
           }
           if(hit.rigidbody != null)
           {
               hit.rigidbody.AddForce(-hit.normal * impactForce);
           }
        }

       // Gameobject GO =Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
       //Destroy(GO, 2f);

    }
}
