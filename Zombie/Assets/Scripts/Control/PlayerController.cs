using RPG.Movement;
using RPG.Attributes;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace RPG.Control
{    

    [DisallowMultipleComponent]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float MaxNavmeshProjectionDistnace = 1f;
        [SerializeField] float rayCastRadius = 1f;

        Health health;
        //Fighter fighter;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType cursourType;
            public Texture2D texture2D;
            public Vector2 hotspot;

        }

        [SerializeField] CursorMapping[] cursorMappings;
        

        private void Awake()
        {
            health = GetComponent<Health>();
            //fighter = GetComponent<Fighter>();
        }
        // Update is called once per frame
        void Update()
        {
            if (InteractwithUI()) { return; }
            if (health.IsDead()) 
            {
                SetCursour(CursorType.UI);
                return; 
            }
            if (InteractwithComponent()) { return; }
            
            if (InteractwithMovement()) { return; }
            SetCursour(CursorType.None);
        }

        private bool InteractwithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {                   
                    if (raycastable.HandleRaycast(this)) 
                    {
                        SetCursour(raycastable.GetCursourType());
                        return true; 
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(),rayCastRadius);
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;

            }
            Array.Sort(distances, hits);
            return hits;
        }

        private bool InteractwithUI()
        {            
            if(EventSystem.current.IsPointerOverGameObject())
            {
                SetCursour(CursorType.UI);
                return true;
            }
            return false;
        }



        private void SetCursour(CursorType cursour)
        {
            CursorMapping mapping = GetCursorMapping(cursour);
            Cursor.SetCursor(mapping.texture2D, mapping.hotspot, CursorMode.Auto);
        }

        //public Curs
        private CursorMapping GetCursorMapping(CursorType type)
        {

            foreach (CursorMapping mapping in cursorMappings)
            {
                if(mapping.cursourType==type)
                {
                    return mapping;
                }
            }
           return cursorMappings[0];
        }


        private bool InteractwithMovement()
        {

            Vector3 target;
            bool Hashit = RayCastNavMesh(out target);

            if (Hashit)
            {
                if (!GetComponent<Mover>().CanMoveTO(target)) { return false; }
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                    
                }
                SetCursour(CursorType.Movement);
                return true;
            }
            return false;

        }

        private bool RayCastNavMesh(out Vector3 target)
        {
            target = new Vector3();
            RaycastHit hit;
            if(!Physics.Raycast(GetMouseRay(), out hit)) { return false; }

            NavMeshHit navMeshHit;
            bool hasCast_to_NavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, MaxNavmeshProjectionDistnace, NavMesh.AllAreas);
            if (!hasCast_to_NavMesh) { return false; }

            target = navMeshHit.position;

            //NavMeshPath path = new NavMeshPath();
            //bool hasPath = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
            //if (!hasPath) { return false; }
            //if(path.status != NavMeshPathStatus.PathComplete) { return false; }
            //if (GetPathLength(path) > MaxNaveLEngth) { return false; }
            return true;
        }



        public  Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
