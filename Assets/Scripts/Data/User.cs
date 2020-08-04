using UnityEngine;
using UnityEditor;

/// <summary>
/// Data related specifically to a user
/// </summary>
public class User : ScriptableObject
{
    public string userName = "You";
    public Stats stats;
    public float curBank = 0;

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
    /// </summary>
    /// <param name="winInfo"></param>
    /// <param name="gameCardState"></param>
    public void UpdateUser(WinInfo winInfo, GameCardState gameCardState)
    {
        //TODO
    }
}