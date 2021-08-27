using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeThrower : MonoBehaviour
{
    public Transform throwPoint;
    public float throwForce = 40f;
    public GameObject granadePrefab;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }



    public void ThrowGranade()
    {
        GameObject granade = Instantiate(granadePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = granade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
