using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] PlayerInput _input;
    int animator_speed;
    int animator_throw;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator_speed = Animator.StringToHash("Speed");
        animator_throw = Animator.StringToHash("Throw");
    }

    private void Update()
    {
        animator.SetFloat(animator_speed,_input.input.magnitude);

        if (Input.GetButtonDown("Fire2"))
        {
            StartThrowingAnim();
        }
    }

    private void StartThrowingAnim()
    {
        animator.SetTrigger(animator_throw);
    }
}
