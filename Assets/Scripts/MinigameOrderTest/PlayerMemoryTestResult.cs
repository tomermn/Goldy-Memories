using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents the result of a player's response in a memory test.
/// </summary
[System.Serializable]
public class PlayerMemoryTestResult
{
    private string item1Name;
    private string item2Name;
    private int item1Index;
    private int item2Index;
    private bool isPlayerCorrect;
    private float pressTime;

    public string Item1Name => item1Name;
    public string Item2Name => item2Name;
    public int Item1Index => item1Index;
    public int Item2Index => item2Index;
    public bool IsPlayerCorrect => isPlayerCorrect;
    public float PressTime => pressTime;


    public PlayerMemoryTestResult(string item1Name, string item2Name, int item1Index, int item2Index, bool isPlayerCorrect, float pressTime)
    {
        this.item1Name = item1Name;
        this.item2Name = item2Name;
        this.item1Index = item1Index;
        this.item2Index = item2Index;
        this.isPlayerCorrect = isPlayerCorrect;
        this.pressTime = pressTime;
    }
}
