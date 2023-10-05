using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public Image itemImage;
    private void Awake()
    {
        gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    public IEnumerator OnCollectingItem(Sprite sprite)
    {
        Debug.Log("ENTERD");
        if (sprite != null)
        {
            itemImage.sprite = sprite;
            gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            gameObject.SetActive(false);
        }
    }
}
