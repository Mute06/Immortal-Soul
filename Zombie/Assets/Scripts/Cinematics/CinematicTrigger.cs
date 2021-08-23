using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool _Istriggered = false;
        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player") && !_Istriggered)
            {
                GetComponent<PlayableDirector>().Play();
                _Istriggered = true;
            }
        }
    }
}
