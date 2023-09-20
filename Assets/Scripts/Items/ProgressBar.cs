using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    

    // Start is called before the first frame update
    void Awake()
    {
        slider  = GetComponent<Slider>(); 
        slider.value = 0;
    }

    public void IncProgress(float value)
    {
        slider.value = value;
    }
}
