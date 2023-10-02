using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Item Database/Item Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<ItemData> items  = new List<ItemData>();
    public List<ItemData> Items => items; //property


}
