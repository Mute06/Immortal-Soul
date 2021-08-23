using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] RectTransform Foreground;
        [SerializeField] Health health;
        [SerializeField] Canvas canvas;

        // Update is called once per frame
        void Update()
        {
            Foreground.localScale = new Vector3(health.GetFractionHealth(), 1, 1);
            if(Mathf.Approximately(health.GetHealtPoints(),0) || Mathf.Approximately(health.GetFractionHealth(),1))
            {
                canvas.gameObject.SetActive(false);
            }
            else
            {
                canvas.gameObject.SetActive(true);
            }
        }
    }

}