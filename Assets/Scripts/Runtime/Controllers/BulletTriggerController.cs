using UnityEngine;

namespace Runtime.Controllers
{
    public class BulletTriggerController : MonoBehaviour
    {
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private Vector3 impactEffectOffset;
        public float damage = 10f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyManager>().TakeDamage(10);
                var enemy = other.GetComponent<EnemyManager>();
                enemy.TakeDamage(damage);

                GameObject GO = Instantiate(impactEffect, enemy.transform);
                GO.transform.position = enemy.transform.position + impactEffectOffset;
                Destroy(GO, 2f);
                Destroy(gameObject);
            }
        }
    
    }
}
