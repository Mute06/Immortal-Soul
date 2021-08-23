using RPG.Core;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        Health target;
        [SerializeField] float ProjectileSpeed = 2f;
        [SerializeField] float damage=2;
        [SerializeField] bool isHoming =false;
        [SerializeField] GameObject fireBallEffect;
        [SerializeField] GameObject[] DestroyOnHit;
        [SerializeField] UnityEvent onHit;
        GameObject Instigator;
        void Start()
        {
            if (!isHoming)
            {
                transform.LookAt(GetAimLocation());
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) { return; }
            if(isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            
            transform.Translate(Vector3.forward * Time.deltaTime * ProjectileSpeed);
        }

        public void SetTarget(Health target, GameObject Instigator,float damage)
        {
            this.target = target;
            this.damage = damage;
            this.Instigator = Instigator;
            Destroy(gameObject, 2f);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider Targetcapsule = target.GetComponent<CapsuleCollider>();
            if (Targetcapsule == null) { return target.transform.position; }
            return target.transform.position + Vector3.up * Targetcapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) { return; }
            if (target.IsDead()) { return; }
            target.TakeDamage(Instigator,damage);


            onHit.Invoke();

            if(fireBallEffect !=null)
            {
                GameObject fireballFX = Instantiate(fireBallEffect, GetAimLocation(),transform.rotation);

                Destroy(fireballFX, 0.2f);
            }
            foreach (GameObject toDestroy in DestroyOnHit)
            {
                Destroy(toDestroy);
            }
            Destroy(gameObject,0.2f);
        }


        public float GetDamage()
        {
            return damage;
        }
    }
}
