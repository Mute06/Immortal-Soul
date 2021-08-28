using RPG.Control;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


namespace RPG.Scenemanagemnt
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier{ A,B }
        [SerializeField] int Scenenumber = 0;
        [SerializeField] Transform SpawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float Fadeout_IN_Time =2;

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition(other));                
            }
        }



        IEnumerator Transition(Collider other)
        {          
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            //SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

           
            other.GetComponent<PlayerController>().enabled = false;

            yield return fader.Fadeout(Fadeout_IN_Time);            
            //savingWrapper.Save();
            yield return SceneManager.LoadSceneAsync(Scenenumber);
            PlayerController NewplayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            NewplayerController.enabled = false;
            //savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();            
            //UpdatePlayer(otherPortal);

            //savingWrapper.Save();

            yield return new WaitForSeconds(0.5f);
            yield return fader.FadeIn(Fadeout_IN_Time);

            //NewplayerController.enabled = true;
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            //Player.GetComponent<NavMeshAgent>().enabled = false;
            //Player.transform.position=otherPortal.SpawnPoint.position;
            //Player.transform.rotation = otherPortal.SpawnPoint.rotation;
            //Player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) { continue; }
                if (portal.destination != destination) { continue; }
                return portal;
            }
            return null;
        }
    }

}