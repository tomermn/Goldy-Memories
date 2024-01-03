using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Represents the result of a player's response in a memory test.
/// </summary
[System.Serializable]
public class PlayerMemoryTestResult
{
    public string item1Name;
    public string item2Name;
    public int item1Index;
    public int item2Index;
    public bool isPlayerCorrect;
    public float pressTime;


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
