using UnityEngine;
using UnityEditor;
using System;

/// <summary>
/// The state submitted as a playable entry on behalf of a user.
/// </summary>
public class GameCardState
{
    //TODO fields:
    //selections, secondChance, bet amount, total wager, etc
    private Bean[] choices = new Bean[GameConstants.NUM_GAME_ROWS];
    private bool enableSecondChance = false;
    private float betAmt = 0; //base amount
    private float totalWager = 0;  //total amount
    private DateTime entryTimestamp;

    public float GetTotalWager()
    {
        Debug.Log("GetTotalWager");
        return totalWager;
    }

    /// <summary>
    /// Creates a gamecard with the given options selected, 
    /// timestamped at the current time
    /// </summary>
    /// <param name="choices"></param>
    /// <param name="enableSecondChance"></param>
    /// <param name="betAmt"></param>
    public void FillCard(Bean[] choices, bool enableSecondChance, float betAmt)
    {
        Debug.Log("FillCard");
        entryTimestamp = System.DateTime.UtcNow;
        for(int i = 0; i < choices.Length; i++)
        {
            this.choices[i] = choices[i];
        }
        this.enableSecondChance = enableSecondChance;
        this.betAmt = betAmt;

        totalWager = CalculateWager(betAmt, enableSecondChance);
    }

    private float CalculateWager(float baseWager, bool hasSecondChance)
    {
        //There could potentially be options to add bet multipliers or other
        //factors that influence the totalWager. Those would be calculated here.
        float total = baseWager;
        if (hasSecondChance)
        {
            total += GameConstants.SECOND_CHANCE_COST;
        }

        return total;
    }

    public bool IsSecondChanceEnabled()
    {
        return enableSecondChance;
    }
}