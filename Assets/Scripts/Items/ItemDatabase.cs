using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Represents a database of items containing their data, accessible as a ScriptableObject in the Unity Editor.
/// </summary>
[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Item Database/Item Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<ItemData> items  = new List<ItemData>();
    public List<ItemData> Items => items;  // Gets the list of ItemData representing the items in the database.


    /// <summary>
    /// Gets the sprite representing a specific item from the database.
    /// </summary>
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
