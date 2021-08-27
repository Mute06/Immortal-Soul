using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffectManager : MonoBehaviour
{
    public static BloodEffectManager instance;
    [SerializeField] private GameObject bloodEffectPrefab;
    [SerializeField] private Transform Player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CreateBloodEffect(Vector3 position)
    {
        GameObject blood = Instantiate(bloodEffectPrefab, position, Quaternion.identity, transform);
        blood.transform.LookAt(Player);
        var script = blood.GetComponent<BloodEffect>();
        script.FindGround();
    }
}
