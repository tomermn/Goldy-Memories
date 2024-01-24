using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;






[System.Serializable]
public class Pair
{
    [SerializeField]
    private int first;

    [SerializeField]
    private int second;

    public Pair(int first, int second)
    {
        this.first = first;
        this.second = second;
    }

    public int First => first;
    public int Second => second;
}
/// <summary>
/// Manages the execution and progression of the memory test minigame.
/// </summary>
public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance { get; private set; }

    [SerializeField]
    private ItemDatabase itemDB;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private Image image1;

    [SerializeField]
    private Image image2;

    [SerializeField]
    private Button button1;

    [SerializeField]
    private Button button2;

    [SerializeField]
    private int pairNumber;

    [SerializeField]
    private int correctAnswer;

    [SerializeField]
    private float timeDisplay;

    [SerializeField]
    private float timePressed;

    [SerializeField]
    private bool toSaveStats;

    [SerializeField]
    private int numCorrect;

    [SerializeField]
    private Pair[] pairs;

    [SerializeField]
    private Book book;

    private List<PlayerMemoryTestResult> results = new List<PlayerMemoryTestResult>();

    /// <summary>
    /// Initializes the GameManager singleton instance and sets the initial respawn point.
    /// </summary>
    private void Awake()
    {
 
        if (Instance != null) //is there already an instance available
        {
            DestroyImmediate(gameObject);

        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(book.gameObject);
            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(false);
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
    }



    public void StartMinigame()
    {
        Debug.Log("got inside StartMinigame of minigameManager");
        book.DisplayBook();
        inventory = Inventory.Instance;
        //StartCoroutine(PlayMinigame());
    }



    public void DisplayNextItems()
    {
        Debug.Log("got inside DisplayNextItems()");
        Pair currentPair = pairs[pairNumber];
        Debug.Log(currentPair.Second.ToString());
        Debug.Log(currentPair.First.ToString());
        Debug.Log(currentPair);
        DisplayNextItems(currentPair.First, currentPair.Second);
    }

    /// <summary>
    /// Displays the images of the next pair of items in the test.
    /// </summary>
    private void DisplayNextItems(int first, int second)
    {
        if (pairNumber == pairs.Length)
        {
            return;
        }

        correctAnswer = first < second ? 0 : 1;

        string itemName1 = inventory.GetItemByIndex(first);
        string itemName2 = inventory.GetItemByIndex(second);

        Sprite itemSprite1 = itemDB.GetItemSprite(itemName1);
        Sprite itemSprite2 = itemDB.GetItemSprite(itemName2);

        if (itemSprite1 != null && itemSprite2 != null)
        {
            image1.sprite = itemSprite1;
            image2.sprite = itemSprite2;
            image1.gameObject.SetActive(true);
            image2.gameObject.SetActive(true);
            timeDisplay = Time.time;
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            button1.interactable = true;
            button2.interactable = true;
        }
        pairNumber++;
    }



    /// <summary>
    /// Records the result of the player's response.
    /// </summary>
    private void RecordResult(bool isCorrect)
    {
        string itemName1 = inventory.GetItemByIndex(pairs[pairNumber].First);
        string itemName2 = inventory.GetItemByIndex(pairs[pairNumber].Second);
        float timePassed = timePressed - timeDisplay;

        PlayerMemoryTestResult result = new PlayerMemoryTestResult(itemName1, itemName2, pairs[pairNumber].First, pairs[pairNumber].Second, isCorrect, timePassed);
        results.Add(result);
    }

    public void OnButton1Click()
    {
        timePressed = Time.time;
        bool isCorrect = (correctAnswer == 0);
        if (isCorrect)
        {
            numCorrect++;
        }
        RecordResult(isCorrect);
        ContinueToNextPair();
    }

    public void OnButton2Click()
    {
        timePressed = Time.time;
        bool isCorrect = (correctAnswer == 1);
        if (isCorrect)
        {
            numCorrect++;
        }
        RecordResult(isCorrect);
        ContinueToNextPair();

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
        title.text = Constants.GameOverTitle;
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);

        Application.Quit();


    }

    private void ContinueToNextPair()
    {
        pairNumber++;
        if (pairNumber == pairs.Length)
        {
            button1.interactable = false;
            button2.interactable = false;
            Debug.Log("write to csv");
            FinishMemoryTest();
            return;
        }
        //Pair currentPair = pairs[pairNumber];
        //DisplayNextItems(currentPair.First, currentPair.Second);
    }
}



