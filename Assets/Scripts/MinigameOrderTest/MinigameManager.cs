using System;
using System.Collections;
using System.Collections.Generic;
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


    [SerializeField] public Pair[] pairs;

    private void Start()
    {
        inventory = Inventory.Instance;
        Debug.Log(inventory.n_collected);

        foreach (Pair pair in pairs)
        {
            int i = pair.first;
            int j = pair.second;
            string itemName1 = inventory.GetItemByIndex(i);
            string itemName2 = inventory.GetItemByIndex(j);
            Sprite itemSprite1 = GetItemSprite(itemName1);
            Sprite itemSprite2 = GetItemSprite(itemName2);

            if (itemSprite1 != null && itemSprite2 != null)
            {
                image1.sprite = itemSprite1;
                image2.sprite = itemSprite2;
                image1.gameObject.AddComponent<Button>();
                image2.gameObject.AddComponent<Button>();
            }


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


}
