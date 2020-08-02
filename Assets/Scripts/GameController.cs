﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Central game controller; orchestrates interaction between different game components.
/// The actual game is broken into discrete "Rounds" in which a target state is generated
/// and compared against submitted player GameCardState(s) for that round.  Each round
/// evaluates win/loss values and assigns those back to the player.
/// </summary>
public class GameController : MonoBehaviour
{
    private List<(User user, GameCardState gameCardState)> roundEntries = new List<(User, GameCardState)>();

    void Start()
    {
        Debug.Log("GameController start"); 
    }

    void Update()
    {
        
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
        TargetState targetState = TargetState.FromSeed(seed);
        AnimateToState(targetState);
        foreach (var entry in roundEntries)
        {
            WinInfo winInfo = EvalWin(targetState, entry.gameCardState);
            entry.user.Update(winInfo, entry.gameCardState);
        }
        FinishRound();
    }

    /// <summary>
    /// Take the current board state and transition it to targetState.
    /// This function does not return until the transition is complete.
    /// </summary>
    /// <param name="targetState"></param>
    void AnimateToState(TargetState targetState)
    {
    //TODO
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
        return null;
    }

    /// <summary>
    /// Called at the end of a round to clean up state.
    /// </summary>
    void FinishRound()
    {
        roundEntries.Clear();
    }
}
