using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a UI panel that displays a collected item's image when the player is collecting an item.
/// </summary>
public class ItemPanel : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;

    /// <summary>
    /// Displays the collected item's sprite on the item panel and activates it for a short duration.
    /// </summary>
    public IEnumerator DisplayPanel (Sprite sprite)
    {
        if (sprite != null)
        {
            itemImage.sprite = sprite;
            gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }


 
    
}
