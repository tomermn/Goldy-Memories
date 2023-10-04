using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class Pair
{
    public int first;
    public int second;

    public Pair(int first, int second)
    {
        this.first = first;
        this.second = second;
    }
}

public class MinigameManager : MonoBehaviour
{
    public ItemDatabase itemDB;
    public Inventory inventory;
    public Image image1;
    public Image image2;
    public Button button1;
    public Button button2;
    public int pairNumber;
    public int correctAnswer;


    [SerializeField] public Pair[] pairs;

    private void Start()
    {
        
        inventory = Inventory.Instance;
        pairNumber = 0;
        StartCoroutine(PlayMinigame());
        
        
    }

    private IEnumerator PlayMinigame()
    {
        yield return new WaitForSeconds(5f);
        Pair currentPair = pairs[pairNumber];
        DisplayNextItems(currentPair.first, currentPair.second);
        

    }


    private void DisplayNextItems(int first, int second)
    {
        if (pairNumber == pairs.Length)
        {
            return;
        }

        correctAnswer = first < second ? 0 : 1;

        string itemName1 = inventory.GetItemByIndex(first);
        string itemName2 = inventory.GetItemByIndex(second);

        Sprite itemSprite1 = GetItemSprite(itemName1);
        Sprite itemSprite2 = GetItemSprite(itemName2);

        if (itemSprite1 != null && itemSprite2 != null)
        {
            image1.sprite = itemSprite1;
            image2.sprite = itemSprite2;
            button1.interactable = true;
            button2.interactable = true;
        }
    }

    
    private Sprite GetItemSprite(string itemName)
    {
        foreach (var itemData in itemDB.Items)
        {
            if (itemData.ItemName == itemName)
            {
                return itemData.ItemSprite; 
            }
        }

        Debug.LogWarning($"Item '{itemName}' not found in the database.");
        return null; 
    }

    public void OnButton1Click()
    {
        if (correctAnswer == 0)
        {
            //handle
        }
        else
        {
            //...
        }
        pairNumber++;
        if (pairNumber == pairs.Length)
        {
            return;
        }
        Pair currentPair = pairs[pairNumber];
        DisplayNextItems(currentPair.first, currentPair.second);

    }

    public void OnButton2Click()
    {
        if (correctAnswer == 1)
        {
            //handle
        }
        else
        {
            //...
        }
        pairNumber++;
        if (pairNumber == pairs.Length)
        {
            return;
        }
        Pair currentPair = pairs[pairNumber];
        DisplayNextItems(currentPair.first, currentPair.second);
    }
}



