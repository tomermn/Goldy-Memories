using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item Database/Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;

    public string ItemName => itemName;
    public Sprite ItemSprite => itemSprite;

    // Update is called once per frame
    void Update()
    {
        
    }
}
