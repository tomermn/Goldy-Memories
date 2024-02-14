
using UnityEngine;

/// <summary>
/// Represents the data for a specific item in the game.
/// </summary>
[CreateAssetMenu(fileName = "ItemData", menuName = "Item Database/Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;

    public string ItemName => itemName;
    public Sprite ItemSprite => itemSprite;


}
    