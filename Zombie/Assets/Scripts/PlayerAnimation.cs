using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] PlayerInput _input;
    int animator_speed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator_speed = Animator.StringToHash("Speed");
    }

    private void Update()
    {
        animator.SetFloat(animator_speed,_input.input.magnitude);
    }
}
