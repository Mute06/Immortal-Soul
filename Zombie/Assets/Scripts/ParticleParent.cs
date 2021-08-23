using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleParent : MonoBehaviour
{
    public bool DestroyItselfAfterCreated;
    public float secondsToDestroy;
    private ParticleSystem[] particles;

    void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
        if (DestroyItselfAfterCreated)
            StartCoroutine(DestroyAfterSeconds(secondsToDestroy));
    }

    public void PlayAll()
    {
        foreach (var particle in particles)
        {
            if (!particle.isPlaying)
            {
                particle.Play();
            }
        }
    }
    public IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
