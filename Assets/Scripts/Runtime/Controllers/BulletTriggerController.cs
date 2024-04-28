using UnityEngine;

namespace Runtime.Controllers
{
    public class BulletTriggerController : MonoBehaviour
    {
        [SerializeField] private GameObject impactEffect;
        public float damage = 10f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyManager>().TakeDamage(10);
                var enemy = other.GetComponent<EnemyManager>();
                enemy.TakeDamage(damage);

                GameObject GO = Instantiate(impactEffect, enemy.transform);
                Destroy(GO, 2f);
                Destroy(gameObject);
            }
        }
    
    }
}
