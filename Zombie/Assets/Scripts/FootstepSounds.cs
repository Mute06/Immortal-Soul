using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FootstepSounds : MonoBehaviour
{
    public PlayerInput _input;
    public PlayerController controller;
    public  float timeBetweenStepsOnWalk;
    public float timeBetweenStepsOnRun;
    public float timeBetweenStepsOnCrouch;
    public AudioClip[] stepSoundsOnWalk;
    public AudioClip[] stepSoundsOnRun;
    public AudioClip[] jumpStartSounds;
    public float minPitch = 0.85f;
    public float maxPitch = 1.15f;

    private AudioClip[] currentSounds;
    private float _timeBetweenSteps;
    private float timer;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        timer = timeBetweenStepsOnWalk;
    }

    private void Update()
    {
        if (_input.jumpButtonPressed)
        {
            source.clip = jumpStartSounds[Random.Range(0, jumpStartSounds.Length)];
            source.pitch = Random.Range(minPitch, maxPitch);
            source.Play();
            return;
        }

        // if sprinting
        if (controller.status == Status.sprinting)
        {
            _timeBetweenSteps = timeBetweenStepsOnRun;
            currentSounds = stepSoundsOnRun;
        }

        else if (controller.status == Status.crouching)
        {
            _timeBetweenSteps = timeBetweenStepsOnCrouch;
            currentSounds = stepSoundsOnWalk;
        }

        // if walking or any other status
        else 
        {
            _timeBetweenSteps = timeBetweenStepsOnWalk;
            currentSounds = stepSoundsOnWalk;
        }


        // Ýf player is moving
        if (_input.input.magnitude >= 0.01f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = _timeBetweenSteps;
                source.clip = currentSounds[Random.Range(0, currentSounds.Length)];
                source.pitch = Random.Range(minPitch, maxPitch);
                source.Play();
            }
        }

        else
        {
            timer = _timeBetweenSteps;
        }
    }
}
