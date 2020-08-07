using UnityEngine;
using UnityEditor;

/// <summary>
/// General game stats defined for a single user.
/// </summary>
[CreateAssetMenu(fileName ="AStats", menuName ="User Stats", order = 53)]
public class Stats : ScriptableObject
{
    public float winRate;
    public long numPlayed;
    public long numWon;
    public long numSecChanceWins;
    public long numFlushWins;
    public float amtWagered;
    public float amtWon;
    public float amtLost;
    
    //TODO track #good picks for each win
}