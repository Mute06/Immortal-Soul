using RPG.Core;
using RPG.Attributes;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;
using System;

namespace RPG.Movement
{
    
    [DisallowMultipleComponent]
    public class Mover : MonoBehaviour,IAction//,ISavable
    {
        
        NavMeshAgent _navMeshAgent;
        Health health;
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float MaxNaveLEngth = 40;
        [SerializeField] float multiplyBy;
        GameObject player;
        EnemyClass AIStates;
        private void Awake()
        {
            health = GetComponent<Health>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            player= GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            _navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }


        public bool CanMoveTO(Vector3 Destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, Destination, NavMesh.AllAreas, path);
            if (!hasPath) { return false; }
            if (path.status != NavMeshPathStatus.PathComplete) { return false; }
            if (GetPathLength(path) > MaxNaveLEngth) { return false; }

            return true;
        }

        public void TurnDirection()
        {

        }

        public void Moveto(Vector3 Destination,float speedFratction)
        {
            _navMeshAgent.destination = Destination;
            _navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFratction);
            _navMeshAgent.isStopped = false;            
        }

        public void StartMoveAction(Vector3 destination, float speedFratction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            Moveto(destination, speedFratction);            
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) { return total; }
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return total;

        }

        public void RunFrom()
        {

            //temporarily point the object to look away from the player
            transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);

            //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
            // for this if you want variable results) and store it in a new Vector3 called runTo
            Vector3 runTo = transform.position + transform.forward * multiplyBy;
            //Debug.Log("runTo = " + runTo);

            //So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.

            NavMeshHit hit;    // stores the output in a variable called hit

            // 5 is the distance to check, assumes you use default for the NavMesh Layer name
            NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetNavMeshLayerFromName("Walkable"));
            //Debug.Log("hit = " + hit + " hit.position = " + hit.position);

           


            // And get it to head towards the found NavMesh position
            _navMeshAgent.SetDestination(hit.position);
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            //GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;            
        }

        //public void RestoreState(object state)
        //{
        //    SerializableVector position = (SerializableVector)state;
        //    _navMeshAgent.enabled = false;
        //    transform.position = position.ToVector();
        //    _navMeshAgent.enabled = true;
        //    GetComponent<ActionScheduler>().CancelCurrentAction();
        //}

        //public object CaptureState()
        //{
        //    return new SerializableVector(transform.position);
        //}
    }

}