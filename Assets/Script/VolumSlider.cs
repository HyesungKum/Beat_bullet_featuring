using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumSlider : MonoBehaviour
{
    Slider volumSlider;

    private void Awake()
    {
        volumSlider = GetComponent<Slider>();    
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.Volum = volumSlider.value;
    }
}
