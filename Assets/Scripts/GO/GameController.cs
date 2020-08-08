using System.Collections;
using System.Collections.Generic;
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
[RequireComponent(typeof(BoardAnimController))]
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

    private BoardAnimController boardAnimCtrl;

    bool roundActive = false; //true while timer is at 0
    bool waitingOnAnimation = false; //true while round is showing drawing

    void Start()
    {
        Debug.Log("GameController start");

        boardAnimCtrl = GetComponent<BoardAnimController>();
        if (boardAnimCtrl == null)
        {
            throw new System.Exception("Could not find board animation controller: " + gameObject.name);
        }

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

        //Initially the timer will be counting down
        timer.StartTimer();
    }

    void Update()
    {
        if (roundActive)
        {
            if (waitingOnAnimation)
            {
                if (boardAnimCtrl.IsAnimDone())
                {
                    //drawing animations done, transition to end of drawing process
                    waitingOnAnimation = false;
                    PlayRoundEnd(); //should set roundActive = false when done
                }
            }
        }
        
    }

    /// <summary>
    /// Button handler for submitting a completed game card entry.
    /// </summary>
    public void HandleSubmitGameCard()
    {
        Debug.Log("HandleSubmitGameCard");
        curGameCard.handleSubmit();
        UIController.Instance.SetLastWinText(0);  //Clear previous win
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
        ClearRound();
        //This can adapt to call PlayRound once ready
        Debug.Log("Creating target state");
        TargetState targetState = TargetState.FromSeed(1234);
        lastTargetState = targetState;
        Debug.Log("Target state created");

        Debug.Log("Showing target draws");
        StartAnimateToState(targetState, 1.0f);
        Debug.Log("Done showing target draws");

        PlayRoundEnd();
    }

    public void _Dbg_Clear_Round()
    {
        Debug.Log("Debug clear round - TODO remove this code");
        ClearRound();
    }

    /// <summary>
    /// Test method to immediate draw a flush target state.
    /// </summary>
    public void _Dbg_Draw_Flush()
    {
        Debug.Log("Debug draw flush - TODO  remove this code");
        ClearRound();
        //Creates a random color flush
        int numRows = GameConstants.NUM_GAME_ROWS;
        int randColor = UnityEngine.Random.Range(0, numRows);
        int[] colors = new int[numRows];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = randColor;
        }
        int secChanceColIdx = 0;
        int secChanceRow = 3;
        TargetState targetState = TargetState._Dbg_FromColors(colors, secChanceColIdx, secChanceRow);
        lastTargetState = targetState;

        roundActive = true;
        Debug.Log("Showing target draws");
        StartAnimateToState(targetState, 1.0f);
        Debug.Log("Done showing target draws");

        PlayRoundEnd();
    }


    public void HandleTimerExpire()
    {
        Debug.Log("HandleTimerExpire");
        Debug.Log("TODO - random seed");
        long seed = 1234;
        PlayRoundStart(seed);
        //Don't immediately play round end; wait for animations of drawing to finish
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
    /// Begins playing out a single round of the game.  User entries must be submitted
    /// before this is called.
    /// Generates a target state from the given seed.  Triggers animations to
    /// show the round.  
    /// The round is not concluded until PlayRoundEnd() is called.
    /// </summary>
    /// <param name="seed"></param>
    public void PlayRoundStart(long seed)
    {
        Debug.Log("GameController PlayRound");
        if (roundActive == true)
        {
            Debug.Log("Round is already running");
        }
        else
        {
            roundActive = true;
            ClearRound();  //Maybe should clear last round a bit before starting this round?
            TargetState targetState = TargetState.FromSeed(seed);
            lastTargetState = targetState;
            StartAnimateToState(targetState, 0.0f);
        }
    }

    /// <summary>
    /// Finishes the current round started with PlayRoundStart().
    /// Calculates user wins and cleans up state to prepare for the next round.
    /// </summary>
    private void PlayRoundEnd()
    {
        Debug.Log("PlayRoundEnd");
        if (roundActive == false)
        {
            Debug.Log("Round is not active");
        }
        else
        {
            float totalUserWin = 0;
            foreach (var entry in roundEntries)
            {
                WinInfo winInfo = EvalWin(lastTargetState, entry.gameCardState);
                totalUserWin += entry.user.UpdateUser(winInfo, entry.gameCardState);
            }
            UIController.Instance.SetLastWinText(totalUserWin); //TODO animation here would be nice
            UpdateUserBankText(user);
            FinishRound(lastTargetState);
            roundActive = false;
            waitingOnAnimation = false;
        }
    }

    /// <summary>
    /// Take the current board state and transition it to targetState.
    /// This function returns before the animation is complete.  Check
    /// Animation status by querying the BoardAnimController.IsAnimDone().
    /// </summary>
    /// <param name="targetState"></param>
    /// <param name="paramT">value between 0 and 1 defining how far along
    /// in the animation we are. 0 is start, 1 is end.</param>
    void StartAnimateToState(TargetState targetState, float paramT)
    {
        Debug.Log("StartAnimateToState");

        int numRows = targetState.GetNumRows();
        for (int r = 0; r < numRows; r++)
        {
            Debug.Log("Update target row " + r);
            Bean rBean = targetState.GetDrawForRow(r);
            beanRows[r].SetBean(rBean);
        }
        boardAnimCtrl.AnimateBeanDraws(beanRows, paramT);
        waitingOnAnimation = true;

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
        Debug.Log("EvalWin");
        WinInfo winInfo = new WinInfo();

        Bean secChanceBean = null;
        int secChanceRow = -1;
        //Only check second chance if user selected that option
        if (gameCardState.IsSecondChanceEnabled())
        {
            secChanceBean = targetState.GetDrawBeanForSecondChance();
            secChanceRow = targetState.GetDrawRowForSecondChance();
        }

        int numRowsWon = 0;
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

            if (drawMatch)
            {
                numRowsWon++;
            }

        }

        int betIdx = gameCardState.GetBetIndex();
        if (betIdx < 0 || betIdx >= BetMap.MAX_BET_IDX)
        {
            throw new System.Exception("Invalid bet index: " + betIdx);
        }

        bool flushWon = targetState.HasFlush();
        float flushWinAmt = 0;
        if (flushWon)
        {
            flushWinAmt = Payouts.flushWin[betIdx];
        }
        winInfo.SetFlushWon(flushWon);
        Debug.Log("flushWon? " + flushWon + " => Win:" + flushWinAmt);

        float matchWinAmt = Payouts.anyOrder_match[numRowsWon][betIdx];
        Debug.Log("Matched:" + numRowsWon + ", betIdx:" + betIdx + " => Win:" + matchWinAmt);

        float totalWin = matchWinAmt + flushWinAmt;
        winInfo.SetTotalWin(totalWin);

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
