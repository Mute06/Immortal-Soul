using GameDevTV.Utils;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Attributes;
using System;
using UnityEngine;

namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float Chasedistance = 5f;
        [SerializeField] float SuspicionTime = 2;        
        [SerializeField] float AgroCooldownTime = 2;
        [SerializeField] float FearCooldownTime = 2;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float WaypointTolernace=1f;
        [SerializeField] float WaypointDewellTime = 2f;
        [SerializeField] private float shoutDistance=3f;
        [SerializeField] float ChaseTimeTurn = 0;
        [SerializeField] EnemyClass AIStates;
        [SerializeField][Range(0,1)] float patrolSpeedFraction = 0.2f;
        Fighter fighter;
        GameObject Player;
        Health health;
        Mover mover;
        
        LazyValue<Vector3> Guardlocation;

        float timeSinceLastsawPlayer = Mathf.Infinity;
        float timeSinceLastPatrol = Mathf.Infinity;
        float timePlayerAttacked = Mathf.Infinity;
        float timePlayerSeen = Mathf.Infinity;
        
        int currentWaypointIndex = 0;
        

        private void Awake()
        {
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            Player = GameObject.FindGameObjectWithTag("Player");
            Guardlocation = new LazyValue<Vector3>(GetGuardposition);
        }

        private Vector3 GetGuardposition()
        {
            return transform.position;
        }

        // Start is called before the first frame update
        void Start()
        {
            Guardlocation.ForceInit();
        }

        // Update is called once per frame
        void Update()
        {

            if (health.IsDead()) { return; }

            if(AIStates==EnemyClass.Enemey)
            {
                if (IsAggrovated() && fighter.CanAttack(Player))
                {//Attack State
                    AttackBehaivour();
                }
                else if (timeSinceLastsawPlayer < SuspicionTime)
                {//Patrol State
                    SuspicionBehaviour();
                }
                else
                {
                    //Gaurd State
                    PatrolBehavioeur();
                }
            }

            if(AIStates== EnemyClass.Citizen)
            {
                if(IsScared())
                {
                    ScaredBehaivour();
                }
                else if (timePlayerSeen < SuspicionTime)
                {//Patrol State
                    SuspicionBehaviour();
                }
                else
                {
                    //Gaurd State
                    PatrolBehavioeur();
                }
            }
            
            UpdateTimers();
        }

        private void ScaredBehaivour()
        {
            timePlayerSeen = 0;
            mover.RunFrom();           

        }

        

        public void Aggrovate()
        {
            timePlayerAttacked = 0;
        }

        private void UpdateTimers()
        {
            timeSinceLastsawPlayer += Time.deltaTime;
            timeSinceLastPatrol += Time.deltaTime;
            timePlayerAttacked += Time.deltaTime;
            timePlayerSeen += Time.deltaTime;
        }

        private void PatrolBehavioeur()
        {
            Vector3 nextPostion = Guardlocation.value;
            if(patrolPath!= null)
            {                
                if (AtWaypoint())
                {
                    timeSinceLastPatrol = 0;
                    CycleWaypoint();
                }
                nextPostion = GetcurrentWaypoint();
            }

            if (timeSinceLastPatrol > WaypointDewellTime)
            {
                mover.StartMoveAction(nextPostion,patrolSpeedFraction);

            }
            
        }

        private Vector3 GetcurrentWaypoint()
        {
            return patrolPath.GetWayPoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex= patrolPath.GetNextWayPoint(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float DistanceTowaypoint = Vector3.Distance(transform.position, GetcurrentWaypoint());
            return DistanceTowaypoint < WaypointTolernace;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaivour()
        {
            timeSinceLastsawPlayer = 0;
            fighter.Attack(Player);

            AggrovateNearbyEnemies();
        }

        private void AggrovateNearbyEnemies()
        {
            RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up,0);
            foreach (RaycastHit hit in raycastHits)
            {
                AIController aIController = hit.collider.GetComponent<AIController>();
                if(aIController == null) { continue; }
                aIController.Aggrovate();
            }
        }

        private bool IsAggrovated()
        {
            float DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            return (DistanceToPlayer < Chasedistance || timePlayerAttacked< AgroCooldownTime);
        }
        private bool IsScared()
        {
            float DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            return (DistanceToPlayer < Chasedistance || timePlayerSeen < FearCooldownTime);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Chasedistance);
        }
    }

    
}
