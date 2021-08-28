using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffects : MonoBehaviour
{
    public GameObject bloodPartic;
    public void PlayEffects(Transform position)
    {
        AudioManager.instance.PlaySound("Damage");
        Instantiate(bloodPartic, position);
    }
}
