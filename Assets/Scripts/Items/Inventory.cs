using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Exceptions;


/// <summary>
/// Manages the player's inventory, including collected items, progress tracking, and data persistence.
/// </summary>
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;   // Singleton instance of the Inventory.

    private List<(string, DateTime)> collectedItems = new();
    private static readonly string IllegalIndexMessage = "index {0} is not in the collected items";

    [SerializeField] private int numOfCollectedItems;               // Number of items collected.
    [SerializeField] private int numOfItemsToCollect;      // Total number of items to collect.
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private ItemPanel itemPanel;
    [SerializeField] private ItemDatabase itemDB;



    /// <summary>
    /// Adds a collected item to the inventory, updates progress, and triggers UI animations.
    /// </summary>
    public void AddToInvetory(Item item)
    {
        
        collectedItems.Add((item.name, DateTime.Now));
        numOfCollectedItems = collectedItems.Count;
        if (numOfItemsToCollect != 0)
        {
            float progressRatio = (float)numOfCollectedItems / numOfItemsToCollect;
            progressBar.IncProgress(progressRatio);
            
            StartCoroutine(itemPanel.DisplayPanel(itemDB.GetItemSprite(item.name)));
        }

        SaveInventory();
    }

    /// <summary>
    /// Gets the name of the collected item at the specified index in the inventory.
    /// </summary>
    public string GetItemByIndex(int index) 
    {
        if (index < 0 || index > collectedItems.Count)
        {
            throw new IllegalItemIndexException(string.Format(IllegalIndexMessage, index));

        }
        return collectedItems[index].Item1;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //LoadInventory(); - an option
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Saves the current inventory data to PlayerPrefs.
    /// </summary>
    private void SaveInventory()
    {
        string inventoryJson = JsonUtility.ToJson(collectedItems);
        PlayerPrefs.SetString("CollectedItems", inventoryJson);
        PlayerPrefs.Save();
        //ExportInventoryToCSV(); NOTICE: need to activate for saving the data into csv.
    }

    /// <summary>
    /// Loads the inventory data from PlayerPrefs.
    /// </summary>
    private void LoadInventory()
    {
        // Retrieve the JSON string from PlayerPrefs
        string inventoryJson = PlayerPrefs.GetString("CollectedItems");

        // Deserialize the JSON string back into a List<string>
        collectedItems = JsonUtility.FromJson<List<(string, DateTime)>>(inventoryJson);
        numOfCollectedItems = collectedItems.Count;
    }

    /// <summary>
    /// Exports the inventory data to a CSV file.
    /// At this moment, export the inventory every time an item has been collect. we will change it to every time the player is quit the game, or every time the player enter to minigame
    /// </summary>

    private void ExportInventoryToCSV()
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
