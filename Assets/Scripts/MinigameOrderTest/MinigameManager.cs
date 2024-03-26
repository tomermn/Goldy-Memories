using System.Collections.Generic;
using System.IO;
using Exceptions;
using UnityEngine;



[System.Serializable]
public class Pair
{
    [SerializeField] private int first;
    [SerializeField] private int second;
    public Pair(int first, int second)
    {
        this.first = first;
        this.second = second;
    }

    public int First => first;
    public int Second => second;
}

/// <summary>
/// Manages the execution and progression of the first memory test minigame.
/// </summary>
public class MinigameManager : MonoBehaviour
{
    /// <summary>
    /// Gets the singleton instance of the MinigameManager class.
    /// </summary>
    public static MinigameManager Instance { get; private set; }

    [SerializeField] private ItemDatabase itemDB;
    [SerializeField] private Inventory inventory;
    [SerializeField] private int pairNumber;
    [SerializeField] private int correctAnswer;
    [SerializeField] private float timeDisplay;
    [SerializeField] private bool toSaveStats;
    [SerializeField] private int numCorrect;
    [SerializeField] private Pair[] pairs;
    [SerializeField] private Book book;

    private List<PlayerMemoryTestResult> results = new List<PlayerMemoryTestResult>();

    /// <summary>
    /// Starts the memory test minigame by displaying the book and wait for user input from the book instance.
    /// </summary>
    public void StartMinigame()
    {
        book.DisplayBook();
        inventory = Inventory.Instance;
    }
    /// <summary>
    /// Prepares and returns the sprites for the next pair of items.
    /// </summary>
    /// <returns>An array of sprites representing the next pair of items.</returns>
    public Sprite[] PrepareNextPair()
    {
        try
        {
            Pair currentPair = pairs[pairNumber];
            return PrepareNextPair(currentPair.First, currentPair.Second);
        }
        catch (IllegalItemIndexException e)
        {
            Debug.Log("not enough items has been collected");
            return null;
        }

    }

    /// <summary>
    /// Records the player's result for the current pair and proceeds to the next pair.
    /// </summary>
    /// <param name="pressedButton">The index of the button pressed by the player.</param>
    /// <param name="timePressed">The time at which the button was pressed.</param>
    public void RecordResult(int pressedButton, float timePressed)
    {
        bool isCorrect = (correctAnswer == pressedButton);
        if (isCorrect)
        {
            numCorrect++;
        }

        try
        {
            string itemName1 = inventory.GetItemByIndex(pairs[pairNumber].First);
            string itemName2 = inventory.GetItemByIndex(pairs[pairNumber].Second);
            float timePassed = timePressed - timeDisplay;
            PlayerMemoryTestResult result = new PlayerMemoryTestResult(itemName1, itemName2, pairs[pairNumber].First,
                pairs[pairNumber].Second, isCorrect, timePassed);
            results.Add(result);
            Instance.ContinueToNextPair();
        }
        catch (IllegalItemIndexException e)
        {
            Debug.Log("error in recording results");
        }

    }


    private Sprite[] PrepareNextPair(int first, int second)
    {
        if (pairNumber == pairs.Length)
        {
            return null;
        }

        correctAnswer = first < second ? 0 : 1;

        string itemName1 = inventory.GetItemByIndex(first);
        string itemName2 = inventory.GetItemByIndex(second);

        Sprite itemSprite1 = itemDB.GetItemSprite(itemName1);
        Sprite itemSprite2 = itemDB.GetItemSprite(itemName2);
        Sprite[] sprites = new Sprite[2];
        sprites[0] = itemSprite1;
        sprites[1] = itemSprite2;
        return sprites;
    }

    private void Awake()
    {

        if (Instance != null) 
        {
            DestroyImmediate(gameObject);

        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(book.gameObject);
        }
    }

    private void SaveResultsToCSV()
    {
        string filePath = Application.dataPath + "/CSVFiles/memory_test_results.csv";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Item1Name,Item2Name,Item1Index,Item2Index,IsPlayerCorrect,TimePassed");

            foreach (PlayerMemoryTestResult result in results)
            {
                string line = $"{result.Item1Name},{result.Item2Name},{result.Item1Index},{result.Item2Index},{result.IsPlayerCorrect},{result.PressTime}";
                writer.WriteLine(line);
            }
        }
    }

    private void FinishMemoryTest()
    {
        if (toSaveStats)
        {
            SaveResultsToCSV();
        }
        book.CloseBook();
        GameManager.Instance.ResumeGame();

    }

    private void ContinueToNextPair()
    {
        pairNumber++;
        if (pairNumber == pairs.Length)
        {
            Debug.Log("write to csv");
            FinishMemoryTest();
            return;
        }
        book.DisplayNextItems();
    }
}



