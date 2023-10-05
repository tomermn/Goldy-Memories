using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Inventory : MonoBehaviour
{
    public List<(string, DateTime)> collectedItems = new List<(string, DateTime)>();
    public int n_collected;
    public int n_to_collect;
    public ProgressBar progressBar;
    public ItemPanel itemPanel;
    public ItemDatabase itemDB;
    public static Inventory Instance;
    


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            //LoadInventory();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void AddToInvetory(Item item)
    {
        collectedItems.Add((item.name, DateTime.Now));
        n_collected = collectedItems.Count;
        if (n_to_collect != 0)
        {
            float progressRatio = (float)n_collected / n_to_collect;
            progressBar.IncProgress(progressRatio);
            StartCoroutine(itemPanel.OnCollectingItem(itemDB.GetItemSprite(item.name)));
        }

        SaveInventory();
    }


    public void SaveInventory()
    {
        string inventoryJson = JsonUtility.ToJson(collectedItems);
        PlayerPrefs.SetString("CollectedItems", inventoryJson);
        PlayerPrefs.Save();
        //ExportInventoryToCSV(); NOTICE: need to activate for saving the data into csv.
    }

    public void LoadInventory()
    {
        // Retrieve the JSON string from PlayerPrefs
        string inventoryJson = PlayerPrefs.GetString("CollectedItems");


        // Deserialize the JSON string back into a List<string>
        collectedItems = JsonUtility.FromJson<List<(string, DateTime)>>(inventoryJson);
        n_collected = collectedItems.Count;
    }
    

    /*
     * at this moment, export the inventory every time an item has been collect. we will change it to every time the player is quit the game, or every time the player enter to minigame
     */
    public void ExportInventoryToCSV()
    {
        string fp = Application.dataPath + "/CSVFiles/inventory.csv";

        using (StreamWriter writer = new StreamWriter(fp))
        {

            writer.WriteLine("Item,Timestamp");
            foreach ((string itemName, DateTime timestamp) in collectedItems)
            {
                string csvLine = $"{itemName},{timestamp.ToString()}";
                writer.WriteLine(csvLine);
            }
        }
    }

    public string GetItemByIndex(int index)
    {
        if (index >= 0 && index <= collectedItems.Count)
        {
            return collectedItems[index].Item1;
        }
        else
        {
            Debug.LogError("item num is not valid");
            return null;
        }
    }
}
