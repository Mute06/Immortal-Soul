using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] float attackRate= 2f; // How many times you can attack per second
    [SerializeField] GameObject[] particles;
    public int AttackDamage;
    private Animator animator;
    private int attackTrigger;
    float nextAttackTime;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        attackTrigger = Animator.StringToHash("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time>= nextAttackTime)
        {
            if (_input.attack)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        //Play animation
        animator.SetTrigger(attackTrigger);

        //Detect Enemies
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        if (hitEnemies != null)
        {
            foreach (var enemy in hitEnemies)
            {
                Debug.Log("We Hit" + enemy.name);
                enemy.GetComponent<Health>().TakeDamage(null,AttackDamage);
                Vector3 hitPosition = enemy.ClosestPointOnBounds(attackPoint.position);
                GameObject partic = Instantiate(particles[Random.Range(0, particles.Length)], hitPosition,Quaternion.identity);
                partic.transform.LookAt(transform);
                partic.transform.SetParent(enemy.transform);
                //BloodEffectManager.instance.CreateBloodEffect(hitPosition);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
