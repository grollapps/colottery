using UnityEngine;

/// <summary>
/// General game stats defined for a single user.
/// </summary>
public class Stats : MonoBehaviour
{
    private float winRate;
    private long numPlayed;
    private long numWon;
    private long numSecChanceWins;
    private long numFlushWins;
    private float amtWagered;
    private float amtWon;
    private float amtLost;

    void Start()
    {
        ClearStats();
    }

    public void ClearStats()
    {
        winRate = 0;
        numPlayed = 0;
        numWon = 0;
        numSecChanceWins = 0;
        numFlushWins = 0;
        amtWagered = 0;
        amtWon = 0;
        amtLost = 0;
    }

    /// <summary>
    /// Add in the given amounts to the current stats
    /// </summary>
    /// <param name="amtWagered"></param>
    /// <param name="amtWon"></param>
    public void UpdateAmts(float amtWagered, float amtWon) {
        this.amtWagered += amtWagered;
        this.amtWon += amtWon;
        RecalcStats();
    }

    /// <summary>
    /// Update games played stats.  Given numbers are added
    /// to existing stats.
    /// </summary>
    /// <param name="numPlayed"></param>
    /// <param name="numWon"></param>
    /// <param name="numSecChanceWins"></param>
    /// <param name="numFlushWins"></param>
    public void UpdatePlays(int numPlayed, int numWon,
        int numSecChanceWins, int numFlushWins) {
        this.numPlayed += numPlayed;
        this.numWon += numWon;
        this.numSecChanceWins += numSecChanceWins;
        this.numFlushWins += numFlushWins;
        RecalcStats();
    }

    /// <summary>
    /// Updates derived stats
    /// </summary>
    private void RecalcStats() {
        winRate = numPlayed / (float) numWon;

        float totalLoss = amtWagered - amtWon;
        if (totalLoss > 0)
        {
            amtLost = totalLoss;
        }
        else
        {
            amtLost = 0;
        }
    }
}