using UnityEngine;
using UnityEditor;

/// <summary>
/// Holds information about a win/loss as computed from a targetState and
/// GameCardState.
/// </summary>
public class WinInfo 
{
    //TODO ID for last game and entry card

    private bool wonFlush;
    //one entry per row, set true if entry matched target row
    private bool[] wonRow;

    //True if the win for the row would have been a loss but the
    //second chance was played and matched.
    private bool wonSecondChance;

    private float lastWin;

    public WinInfo()
    {
        wonFlush = false;
        wonRow = new bool[GameConstants.NUM_GAME_ROWS];
        for (int i = 0; i < wonRow.Length; i++)
        {
            wonRow[i] = false;
        }
        lastWin = 0;
    }

    public void SetRowWon(int rowNum, bool won)
    {
        wonRow[rowNum] = won;
    }

    public void SetSecondChanceWon(bool won)
    {
        wonSecondChance = won;
    }

    public bool GetWonSecondChance()
    {
        return wonSecondChance;
    }

    public void SetFlushWon(bool won)
    {
        wonFlush = won;
    }

    public bool GetFlushWon()
    {
        return wonFlush;
    }

    /// <summary>
    /// Set the amount won.  This will always be a positive amount and is
    /// a total amount (includes winning back initial wager)
    /// </summary>
    /// <param name="total"></param>
    public void SetTotalWin(float total)
    {
        if (total > 0)
        {
            this.lastWin = total;
        }
        else
        {
            this.lastWin = 0;
        }
    }

    /// <summary>
    /// Returns last win amount which is always >= 0
    /// </summary>
    /// <returns></returns>
    public float GetLastWin()
    {
        return lastWin;
    }

    /// <summary>
    /// Returns true if this represents a win of any amount
    /// </summary>
    /// <returns></returns>
    public bool IsWin()
    {
        return lastWin > 0;
    }
}