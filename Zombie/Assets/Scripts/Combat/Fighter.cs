using GameDevTV.Utils;
using RPG.Core;
using RPG.Movement;
using RPG.Attributes;
using RPG.Saving;
using RPG.Stats;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{   
    public class Fighter : MonoBehaviour,IAction//,ISavable, IModifierProvider
    {
        
        [SerializeField] float TimebetweenAttacks = 1f;
        [SerializeField] Transform lefttHandTransfrom;
        [SerializeField] Transform rightHandTransfrom;
        [SerializeField] WeaponConfig defaultWeapon;
        [SerializeField]EnemyClass AIStates;
        [SerializeField] float Range = 0;
        Health target;
        GameObject Player;
        Animator animator;
        WeaponConfig currentWeaponConfig;
        LazyValue<Weapon> currentWeapon;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Awake()
        {
            Player = GameObject.FindWithTag("Player");
            currentWeaponConfig = defaultWeapon;
            //currentWeapon = new LazyValue<Weapon>(GetInitalWeapon);
            animator = GetComponent<Animator>();
        }

        //private Weapon GetInitalWeapon()
        //{            
        //    return AttachWeapon(defaultWeapon);
        //}
        private void Start()
        {
            //AttachWeapon(currentWeaponConfig);
            //currentWeapon.ForceInit();              
        }

        //public void EquipWeapon(WeaponConfig weapon)
        //{
        //   currentWeapon.value = AttachWeapon(weapon);
        //}

        //private Weapon AttachWeapon(WeaponConfig weapon)
        //{
        //    currentWeaponConfig = weapon;
        //   return weapon.SpawnWeapon(rightHandTransfrom, lefttHandTransfrom, animator);
        //}

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

           // if(target == null) { return; }
          //  if (target.IsDead()) { return; }
            if(AIStates==EnemyClass.Enemey)
            {
                if (!GetsInRange(Player.transform))
                {
                    GetComponent<Mover>().Moveto(Player.transform.position, 1f);
                }
                else
                {
                    GetComponent<Mover>().Cancel();
                    AttackBehaviour();
                }
            }
            if(AIStates==EnemyClass.Citizen)
            {
                if (GetsInRange(Player.transform))
                {
                    GetComponent<Mover>().RunFrom();
                }
            }
           

            //if (target.IsDead())
            //{
            //    Cancel();
            //}
            

            
        }

        public bool CanAttack(GameObject Combattarget)
        {
            if (Combattarget == null) { return false; }
            if (!GetComponent<Mover>().CanMoveTO(Combattarget.transform.position) && !GetsInRange(Combattarget.transform)) { return false; }
            //Health targetTOTest= Combattarget.GetComponent<Health>();
            return true;//!targetTOTest.IsDead() && targetTOTest != null;
        }

        private void AttackBehaviour() 
        {
            transform.LookAt(Player.transform);
            if(timeSinceLastAttack > TimebetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
                //GetComponent<Mover>().Cancel();
            }

        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("isShooting");
            animator.SetTrigger("isShooting");
        }

        private bool GetsInRange(Transform targetTransform)
        {
            return Vector3.Distance(targetTransform.position, transform.position) < Range;//currentWeaponConfig.GetWeaponRange();
        }



        public void Attack(GameObject Combattarget)//for Player to Attack Enemy
        {
            target = Combattarget.GetComponent<Health>();
            GetComponent<ActionScheduler>().StartAction(this);
            
        }

        public Health GetTarget()
        {
            return target;
        }

        public IEnumerable<float> GetAdditiveModifiers(Stats.Stats stats)
        {
            if(stats==Stats.Stats.Damage)
            {
                yield return currentWeaponConfig.GetWeaponDamage();
              //  yield return currentWeapon.GetWeaponDamage();//Dual weilding
            }
        }


        public IEnumerable<float> GetPercentageModifier(Stats.Stats stats)
        {
            if (stats == Stats.Stats.Damage)
            {
                yield return currentWeaponConfig.GetPercentageBonus();
                //  yield return currentWeapon.GetPercentageBonus();//Dual weilding
            }
        }

        public void Cancel()
        {
            TriggerCancelAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void TriggerCancelAttack()
        {
            animator.ResetTrigger("isShooting");
            animator.SetTrigger("isShooting");
        }

        void Hit() //Animation Event in the Attack Animation
        {
            if(target == null) { return; }

           //float damage = GetComponent<BaseStats>().GetStat(Stats.Stats.Damage);
            //if(currentWeapon.value!=null)
            //{
            //    currentWeapon.value.OnHit();
            //}
            //if (currentWeaponConfig.HasProjectile())
            //{
            //    currentWeaponConfig.LaunchProjectile(rightHandTransfrom, lefttHandTransfrom, target,gameObject,damage);
            //}
            //else
            //{
            //    target.TakeDamage(gameObject, damage);
            //}
            
        }

        void Shoot()
        {
            Hit();
        }

        //public void RestoreState(object state)
        //{

        //    string WeaponName = (string)state;
        //    WeaponConfig weapon = UnityEngine.Resources.Load<WeaponConfig>(WeaponName);
        //    EquipWeapon(weapon);
        //}

        //public object CaptureState()
        //{
        //    return currentWeaponConfig.name;
        //}

        
    }
}

