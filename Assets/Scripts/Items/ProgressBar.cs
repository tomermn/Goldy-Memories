using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a progress bar UI element that can be updated to show progress.
/// </summary>
public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    

    
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
