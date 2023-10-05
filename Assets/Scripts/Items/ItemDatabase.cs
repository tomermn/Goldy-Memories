using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Item Database/Item Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<ItemData> items  = new List<ItemData>();
    public List<ItemData> Items => items; //property


    public Sprite GetItemSprite(string itemName)
    {
        foreach (var itemData in items)
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
