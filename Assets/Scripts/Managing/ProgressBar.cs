using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int itemToCollect = 4;
    public int collectedItems = 0;
    public Image mask;


    //new
    public int maximum, current;


    private void Update()
    {
        getCurrentFill();
    }


    void getCurrentFill()
    {
        float fillAmount = (float) collectedItems / (float) itemToCollect;
        mask.fillAmount = fillAmount;
    }
}
