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
        collectedItems.Add((item.name, DateTime.Now));
        n_collected = collectedItems.Count;
        if (n_to_collect != 0)
        {
            float progressRatio = (float)n_collected / n_to_collect;
            Debug.Log(progressRatio);
            progressBar.IncProgress(progressRatio);

        }

        SaveInventory();
    }


    public void SaveInventory()
    {
        string inventoryJson = JsonUtility.ToJson(collectedItems);
        PlayerPrefs.SetString("CollectedItems", inventoryJson);
        PlayerPrefs.Save();
        ExportInventoryToCSV();
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
}
