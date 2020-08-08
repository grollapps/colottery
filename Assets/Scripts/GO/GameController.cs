using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Central game controller; orchestrates interaction between different game components.
/// The actual game is broken into discrete "Rounds" in which a target state is generated
/// and compared against submitted player GameCardState(s) for that round.  Each round
/// evaluates win/loss values and assigns those back to the player.
///
/// The Game layout consists of this GameController driving the execution and displaying 
/// the results in objects contained in an instance.  The target draws are displayed
/// by a collection of BeanRows that each show a bean as it is drawn (selected randomly).
/// A BeanRow renders in the target as a BeanGo which positions the data in the game world.
/// </summary>
public class GameController : MonoBehaviour
{
    //Static bean rows that display the target draws
    [SerializeField]
    private List<BeanRow> beanRows;

    //Tracks Entries submitted by users for playing in the next round
    private List<(User user, GameCardState gameCardState)> roundEntries = new List<(User, GameCardState)>();

    private TargetState lastTargetState = null;

    [SerializeField]
    private ActiveGameCard curGameCard;

    //Stub user for this prototype
    [SerializeField]
    private User user;

    [SerializeField]
    private Timer timer;

    void Start()
    {
        Debug.Log("GameController start"); 
        if (user == null)
        {
            throw new System.Exception("Could not find User instance: " + gameObject.name);
        }

        if (curGameCard == null)
        {
            throw new System.Exception("No game card set: " + gameObject.name);
        }

        if (timer == null)
        {
            throw new System.Exception("No timer set: " + gameObject.name);
        }

        UpdateUserBankText(user);
        UpdateWinText(null);
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Button handler for submitting a completed game card entry.
    /// </summary>
    public void HandleSubmitGameCard()
    {
        Debug.Log("HandleSubmitGameCard");
        curGameCard.handleSubmit();
    }

    /// <summary>
    /// Return the one user for the prototype game.
    /// A user simply identifies one particular unique entity that
    /// can submit game entries.
    /// </summary>
    /// <returns></returns>
    public User GetUser()
    {
        return user;
    }

    //TODO - remove, debug only
    public void _Dbg_Start_Round()
    {
        Debug.Log("Debug start round - TODO remove this code");
        //This can adapt to call PlayRound once ready
        Debug.Log("Creating target state");
        TargetState targetState = TargetState.FromSeed(1234);
        lastTargetState = targetState;
        Debug.Log("Target state created");

        Debug.Log("Showing target draws");
        AnimateToState(targetState);
        Debug.Log("Done showing target draws");

    }

    public void _Dbg_Clear_Round()
    {
        Debug.Log("Debug clear round - TODO remove this code");
        ClearRound();
    }

    public void HandleTimerExpire()
    {
        Debug.Log("HandleTimerExpire");
        Debug.Log("TODO - random seed");
        long seed = 1234;
        PlayRound(seed);
    }

    /// <summary>
    /// Enter the User with the given GameCardState into the next playable round.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="gameCard"></param>
    public void EnterNextRound(User user, GameCardState gameCard)
    {
        Debug.Log("GameController EnterNextRound");
        float entryAmt = gameCard.GetTotalWager();
        if (user.CanPay(entryAmt))
        {
            user.AdjustBank(-entryAmt);
            roundEntries.Add((user, gameCard));
        }
        else
        {
            //TODO
            Debug.Log("TODO - User can't afford entry");
        }
        UpdateUserBankText(user);
    }

    public void UpdateUserBankText(User user)
    {
        UIController.Instance.SetBankText(user.curBank);
    }

    public void UpdateWinText(WinInfo winInfo)
    {
        if (winInfo == null || winInfo.GetLastWin() <= 0)
        {
            UIController.Instance.SetLastWinText(0);
        }
        else
        {
            UIController.Instance.SetLastWinText(winInfo.GetLastWin());
        }
    }

    /// <summary>
    /// Plays out a single round of the game.  User entries must be submitted
    /// before this is called.
    /// Generates a target state from the given seed.  Triggers animations to
    /// show the round.  Evaluates target state vs user submissions and updates
    /// win/losses accordingly.
    /// </summary>
    /// <param name="seed"></param>
    public void PlayRound(long seed)
    {
        Debug.Log("GameController PlayRound");
        ClearRound();  //Maybe should clear last round a bit before starting this round?
        TargetState targetState = TargetState.FromSeed(seed);
        lastTargetState = targetState;
        AnimateToState(targetState);
        foreach (var entry in roundEntries)
        {
            WinInfo winInfo = EvalWin(targetState, entry.gameCardState);
            entry.user.UpdateUser(winInfo, entry.gameCardState);
        }
        FinishRound(targetState);
    }

    /// <summary>
    /// Take the current board state and transition it to targetState.
    /// This function does not return until the transition is complete.
    /// </summary>
    /// <param name="targetState"></param>
    void AnimateToState(TargetState targetState)
    {
        //TODO
        Debug.Log("TODO - AnimateToState");

        //TODO - For now we will just show the ending positions
        int numRows = targetState.GetNumRows();
        for (int r = 0; r < numRows; r++)
        {
            Debug.Log("Update target row " + r);
            Bean rBean = targetState.GetDrawForRow(r);
            beanRows[r].SetBean(rBean);
        }
        Debug.Log("TODO - show second chance");
        Debug.Log("TODO - show flush");
    }

    /// <summary>
    /// Compares the GameCardState to a targetState to determine if the 
    /// game card represents a winning entry.
    /// </summary>
    /// <param name="targetState"></param>
    /// <param name="gameCardState"></param>
    /// <returns></returns>
    WinInfo EvalWin(TargetState targetState, GameCardState gameCardState)
    {
        //TODO
        Debug.Log("TODO - EvalWin");
        WinInfo winInfo = new WinInfo();

        Bean secChanceBean = null;
        int secChanceRow = -1;
        //Only check second chance if user selected that option
        if (gameCardState.IsSecondChanceEnabled())
        {
            secChanceBean = targetState.GetDrawBeanForSecondChance();
            secChanceRow = targetState.GetDrawRowForSecondChance();
        }

        for (int r = 0; r < targetState.GetNumRows(); r++)
        {
            Bean targetBean = targetState.GetDrawForRow(r);
            Bean entryBean = gameCardState.GetChoiceForRow(r);
            bool drawMatch = targetBean.IsEqual(entryBean);
            if (drawMatch == false && secChanceBean != null && r == secChanceRow)
            {
                drawMatch = secChanceBean.IsEqual(entryBean); //Check if 2nd chance matches
                if (drawMatch)
                {
                    winInfo.SetSecondChanceWon(true);
                }
            }
            else
            {
                winInfo.SetRowWon(r, drawMatch);
            }

        }

        //TODO win amount
        Debug.Log("TODO - Win Amount");
        //Set in WinInfo itself??

        Debug.Log("TODO - won flush?");
        bool flushWon = false;
        winInfo.SetFlushWon(flushWon);

        return winInfo;
    }

    /// <summary>
    /// Called at the end of a round to clean up state.
    /// </summary>
    void FinishRound(TargetState targetState)
    {
        roundEntries.Clear();
        timer.Reset(true);
    }

    void ClearRound()
    {
        if (lastTargetState != null)
        {
            Debug.Log("ClearRound");
            int numRows = lastTargetState.GetNumRows();
            for (int r = 0; r < numRows; r++)
            {
                beanRows[r].Reset();
            }
        }
        lastTargetState = null;
    }
}
