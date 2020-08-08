using UnityEngine;
using UnityEditor;
using System;

/// <summary>
/// The state submitted as a playable entry on behalf of a user.
/// </summary>
public class GameCardState
{
    private Bean[] choices = new Bean[GameConstants.NUM_GAME_ROWS];
    private bool enableSecondChance = false;
    private int betAmtIdx = -1; //Index of bet amount
    private float betAmt = 0; //base amount as money (dollars)
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
    /// <param name="betAmtIdx">Index for bet, used to lookup dollar amount of bet</param>
    public void FillCard(Bean[] choices, bool enableSecondChance, int betAmtIdx)
    {
        Debug.Log("FillCard");
        entryTimestamp = System.DateTime.UtcNow;
        for(int i = 0; i < choices.Length; i++)
        {
            this.choices[i] = choices[i];
        }
        this.enableSecondChance = enableSecondChance;
        this.betAmtIdx = betAmtIdx;
        betAmt = BetMap.GetBetFromIdx(betAmtIdx);

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

    /// <summary>
    /// Get the bet index which corresponds to the bet amount table (BetMap).
    /// </summary>
    /// <returns></returns>
    public int GetBetIndex()
    {
        return betAmtIdx;
    }

    public bool IsSecondChanceEnabled()
    {
        return enableSecondChance;
    }

    /// <summary>
    /// Get the bean choice for the given row.
    /// Assumes the GameCardState is already initialized.
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    public Bean GetChoiceForRow(int row)
    {
        if (row >= choices.Length || choices[row] == null)
        {
            throw new Exception("Invalid choice row " + row + " (forgot to init entry?)");
        }
        return choices[row];
    }

}