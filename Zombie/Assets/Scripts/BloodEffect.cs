using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BloodEffect : MonoBehaviour
{
    [SerializeField] private VisualEffect bloodEffect;
    [SerializeField] LayerMask GroundMask;
    int Vector3ID;
    // Start is called before the first frame update
    void Start()
    {
        //bloodEffect = GetComponent<VisualEffect>();
        Vector3ID = Shader.PropertyToID("AABoxPos");
    }


    public void FindGround()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.down,out hitInfo , 5f, GroundMask))
        {

            bloodEffect.SetVector3("AABoxPos", hitInfo.point);
            Debug.Log(hitInfo.point);
            
        }
    }


}
