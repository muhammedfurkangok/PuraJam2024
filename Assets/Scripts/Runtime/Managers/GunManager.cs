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
    [SerializeField] private GameObject impactEffect;

    [SerializeField] private GameObject bullet;
    //todo[SerializeField] private AudioSource gunShotSound;
    
    
 

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
        GameObject bulletGO = Instantiate(bullet, cam.transform.position, cam.transform.rotation);
        bulletGO.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 1000f);
        Destroy(bulletGO, 2f);
        
        GameObject GO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(GO, 2f);

    }
}
