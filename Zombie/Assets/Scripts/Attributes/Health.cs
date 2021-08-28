using GameDevTV.Utils;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace RPG.Attributes
{


    public class Health : MonoBehaviour//,ISavable
    {
        bool isDead=false;
        [SerializeField] float maxRegen=70;
        LazyValue<float> health;
        [SerializeField] TakeDamageEvent takeDamge;
        [SerializeField] UnityEvent onDie;
        [SerializeField] float HEALTH;

        [System.Serializable]
        public class TakeDamageEvent: UnityEvent<float>
        {

        }











        private void Awake()
        {
            //health = new LazyValue<float>(110f);
        }

        //private float GetIntialHealth()
        //{
        //    return GetComponent<BaseStats>().GetStat(Stats.Stats.Health);
        //}
        private void Start()
        {
           // health.ForceInit();

        }

        //private void OnEnable()
        //{
        //    GetComponent<BaseStats>().OnLevelUp += RegenHealth;
        //}
        //private void OnDisable()
        //{
        //    GetComponent<BaseStats>().OnLevelUp -= RegenHealth;
        //}


        public void TakeDamage(GameObject Instigator,float Damage)
        {
            HEALTH = Mathf.Max(HEALTH - Damage, 0);
            
            
            if (HEALTH <= 0 && !isDead)
            {
                onDie.Invoke();
                //AwardExperience(Instigator);
                Die();
            }
            else
            {
                takeDamge.Invoke(Damage);
            }

        }

        private void AwardExperience(GameObject Instigator)
        {
            Experience experience = Instigator.GetComponent<Experience>();
            if (experience == null) { return; }
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stats.Stats.ExperienceReward));
           
        }


        private void RegenHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stats.Stats.Health) * (maxRegen / 100);
            health.value = Mathf.Max(health.value, regenHealthPoints);
        }

        private void Die()
        {
            if (isDead) { return; }
            isDead = true;
            if (gameObject.CompareTag("Player"))
            {
                GetComponent<CharacterController>().enabled = false;
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                GetComponent<CapsuleCollider>().enabled = false;
                
                Debug.Log("Death");
            }

            
            if (!gameObject.CompareTag("Player"))
            {
                //GetComponent<Animator>().SetBool("isDead",true);
                GetComponent<ActionScheduler>().CancelCurrentAction();
                Destroy(gameObject);
            }
            
            
            
        }
        public void Heal(float HP)
        {
            HEALTH = Mathf.Min(HEALTH + HP, GetMAXHealtPoints());
        }

        public float GetMAXHealtPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stats.Stats.Health);
        }

        public float GetHealtPoints()
        {
            return HEALTH;
        }


        public float GetPercentageHealth()
        {
            return 100 * (health.value / GetComponent<BaseStats>().GetStat(Stats.Stats.Health));
        }

        public float GetFractionHealth()
        {
            return health.value / GetComponent<BaseStats>().GetStat(Stats.Stats.Health);
        }

        public bool IsDead()
        {
            return isDead;
        }

        //public void RestoreState(object state)
        //{
        //    health.value = (float)state;
        //    if (health.value <= 0)
        //    { 
        //        Die();
        //    }
        //}

        //public object CaptureState()
        //{
        //    return health.value;
        //}
    }

}