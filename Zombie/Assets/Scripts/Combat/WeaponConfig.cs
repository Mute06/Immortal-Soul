using RPG.Attributes;
using System;
using UnityEngine;


namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG Project/Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] float WeaponRange = 2f;
        [SerializeField] float WeaponDamage = 10f;
        [SerializeField] float PercentageBonus = 10f;
        [SerializeField] Weapon EquippedPrefab;
        [SerializeField] AnimatorOverrideController animatorOveride;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile;

        const string WeaponName = "Weapon";

        public Weapon SpawnWeapon(Transform RightHand, Transform LeftHand, Animator animator)
        {
            DestroyOldWeapon(RightHand, LeftHand);
            Weapon weapon = null;
            if (EquippedPrefab!=null)
            {                
                Transform handTransform = GetTransform(RightHand, LeftHand);
                weapon = Instantiate(EquippedPrefab, handTransform);
                weapon.gameObject.name = WeaponName;
                
            }
            var overideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOveride != null)
            {
                animator.runtimeAnimatorController = animatorOveride;
            }
            else if (overideController != null)
            {
                animator.runtimeAnimatorController = overideController.runtimeAnimatorController;
            }

            return weapon;

        }

        private void DestroyOldWeapon(Transform rightHand,Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(WeaponName);
            if(oldWeapon==null)
            {
                oldWeapon = leftHand.Find(WeaponName);
            }
            if (oldWeapon == null) { return; }

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform RightHand, Transform LeftHand)
        {
            Transform handTransform;
            if (isRightHanded)
            {
                handTransform = RightHand;
            }
            else
            {
                handTransform = LeftHand;
            }

            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform RightHand, Transform LeftHand,Health target,GameObject Instigator,float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(RightHand,LeftHand).position,Quaternion.identity);
            projectileInstance.SetTarget(target,Instigator, calculatedDamage);
        }

        public float GetWeaponDamage()
        {
            return WeaponDamage;
        }

        public float GetWeaponRange()
        {
            return WeaponRange;
        }


        public float GetPercentageBonus()
        {
            return PercentageBonus;
        }
    }
}
