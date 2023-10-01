using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Minigame_1_Manager : MonoBehaviour
{
    public List<(string, DateTime)> collectedItems;
    public List<(int, int)> pairs;
    public Image itemImage1;
    public Image itemImage2;

    private int pairsIndex;

    private void Start()
    {
        pairsIndex = 0;
    }

    //private void DisplayNextPair()
    //{
    //    if (pairsIndex < pairs.Count)
    //    {
    //        itemImage1 = 
    //    }
    //}
}
