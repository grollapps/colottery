using UnityEngine;

/// <summary>
/// Data related specifically to a user
/// </summary>
public class User : MonoBehaviour
{
    public string userName = "You";
    public Stats stats;
    public float curBank = 0;

    void Start()
    {
        if (stats == null)
        {
            stats = GetComponentInChildren<Stats>();
            if (stats == null)
            {
                throw new System.Exception("Could not find stats for user: " + gameObject.name);
            }
        }

        Debug.Log("User " + gameObject.name + "(" + userName + ") has stats object: " + stats.gameObject.name);
    }

    /// <summary>
    /// Test if the user has enough funds to pay amt.  Returns true
    /// if so, false otherwise.
    /// </summary>
    /// <param name="amt"></param>
    /// <returns></returns>
    public bool CanPay(float amt)
    {
        return curBank >= amt;
    }

    /// <summary>
    /// Change the users bank by delta amt.  Use a negative value
    /// to decrease the bank, positive to increase.
    /// </summary>
    /// <param name="delta"></param>
    public void AdjustBank(float delta)
    {
        curBank += delta;
    }

    /// <summary>
    /// Updates this user object with WinInfo and a GameCardState
    /// representing the results of running a round with the submitted
    /// gameCardState. Stats are accumulated based on wagers and results.
    /// Returns total amount of win.
    /// </summary>
    /// <param name="winInfo"></param>
    /// <param name="gameCardState"></param>
    public float UpdateUser(WinInfo winInfo, GameCardState gcs)
    {
        Debug.Log("Update user with winInfo");
        float totalWin = winInfo.GetLastWin();
        AdjustBank(totalWin);

        //update stats
        Debug.Log("Update user stats");
        float amtWagered = gcs.GetTotalWager();
        stats.UpdateAmts(amtWagered, totalWin);

        bool isWin = winInfo.IsWin();

        //Currently 1 card per play
        int numPlayed = 1;
        int numWin = isWin ? 1 : 0;
        int numSecChanceWin = winInfo.GetWonSecondChance() ? 1 : 0;
        int numFlushWin = winInfo.GetFlushWon() ? 1 : 0;
        stats.UpdatePlays(numPlayed, numWin, numSecChanceWin, numFlushWin);

        return totalWin;
    }
}