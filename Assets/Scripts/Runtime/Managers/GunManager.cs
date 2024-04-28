using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Mirror;
using Runtime.Managers;
using UnityEngine;

public class GunManager : NetworkBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    public float nextTimeToFire = 0f;
    [SerializeField] private float recoil;

    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private ParticleSystem muzzleFlash;

    [SerializeField] private GameObject bullet;
    //todo[SerializeField] private AudioSource gunShotSound;
    
    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!NetworkServer.activeHost) enabled = false;
    }

    private void Update()
    {
        if (PauseMenuManager.Instance.isGamePaused) return;

        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shot();
        }
    }

    private async void Shot()
    {
        muzzleFlash.Play();

        GameObject bulletGO = Instantiate(bullet, bulletSpawn.position, cam.transform.rotation);
        bulletGO.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 1000f);
        NetworkServer.Spawn(bulletGO);
        MoveGunBackward();
        await UniTask.WaitForSeconds(2f);//
        Destroy(bulletGO);
        NetworkServer.Destroy(bulletGO);
        ResetGunPosition();
    }
    private void MoveGunBackward()
    {
        // Move the gun slightly backward in local space
        transform.Translate(Vector3.back * recoil); // Adjust the distance as needed
    }
    private void ResetGunPosition()
    {
        // Reset the gun position to its original position
        transform.localPosition = Vector3.Lerp(transform.position, new Vector3(0, -26.49f, 0),0.2f);
    }
}
