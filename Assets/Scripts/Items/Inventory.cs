using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> collectedItems = new List<string>();
    public int lst_size;
    public static Inventory Instance;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }



    public void AddToInvetory(Item item)
    {

        collectedItems.Add(item.name);
        lst_size = collectedItems.Count;
    }



}
