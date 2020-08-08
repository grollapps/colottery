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

    public float GetLastWin()
    {
        Debug.Log("TODO - GetLastWin");
        //TODO calc win amount here??
        return lastWin;
    }
}