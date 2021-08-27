using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public LayerMask enemyLayer;

    public GameObject explosionEffect;


    private void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    private void Explode()
    {
        //PLay Effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Detect nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var nearObject in colliders)
        {
            Rigidbody nearRb = nearObject.GetComponent<Rigidbody>();
            if (nearRb != null)
            {
                nearRb.AddExplosionForce(force, transform.position, radius);
            }
            if (IsInsideLayerMask(nearObject.gameObject.layer, enemyLayer))
            {
                //damage enemys
            }
        }
            //Add force or damage them

        //Remove granade
        Destroy(gameObject);
    }

    private bool IsInsideLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
