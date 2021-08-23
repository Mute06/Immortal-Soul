using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Scenemanagemnt
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine currentlyActiveCoroutine;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediately()
        {
            canvasGroup.alpha = 1;
        }

        public Coroutine Fadeout(float time)
        {
            return Fade(1, time);
        }
        

        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);

        }

        public Coroutine Fade(float target,float time)
        {
            if(currentlyActiveCoroutine != null)
            {
                StopCoroutine(currentlyActiveCoroutine);
            }
            currentlyActiveCoroutine = StartCoroutine(FadeRoutine(target,time));
            return currentlyActiveCoroutine;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }

    }
}
