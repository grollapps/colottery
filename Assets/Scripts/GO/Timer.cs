using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles Timer-related updates
/// </summary>
public class Timer : MonoBehaviour
{

    private const int secondsBetweenRounds = GameConstants.SEC_BETWEEN_ROUNDS;

    private float remainingSeconds;
    private bool running = false;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        remainingSeconds = secondsBetweenRounds;
        running = false;

        gameController = FindObjectOfType<GameController>();
        if (gameController == null)
        {
            throw new System.Exception("Could not find game controller: " + gameObject.name);
        }

        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            if (remainingSeconds > 0)
            {
                remainingSeconds -= Time.deltaTime;
                UpdateDisplay();
            }
            else
            {
                remainingSeconds = 0;
                UpdateDisplay();
                HandleTimerExpired();
            }
        }
        
    }

    /// <summary>
    /// Stops the timer and signals the game controller
    /// </summary>
    private void HandleTimerExpired()
    {
        running = false;
        gameController.HandleTimerExpire();
    }

    public void UpdateDisplay()
    {
        UIController.Instance.SetRemainingTimeText(remainingSeconds);
    }

    /// <summary>
    /// Start or stop the timer.
    /// </summary>
    public void ToggleTimer()
    {
        Debug.Log("ToggleTimer");
        running = !running;
    }

    public void StartTimer()
    {
        Debug.Log("Start Timer");
        running = true;
    }

    /// <summary>
    /// Reset timer countdown to max value and start a new
    /// countdown if alsoStart == true
    /// </summary>
    /// <param name="alsoStart"></param>
    public void Reset(bool alsoStart)
    {
        remainingSeconds = secondsBetweenRounds;
        running = alsoStart;
    }

    /// <summary>
    /// A method for testing timer triggers.  Advances a running
    /// timer to the last 2 seconds before triggering.
    /// </summary>
    public void AdvanceTimer()
    {
        Debug.Log("AdvanceTimer");
        if (running)
        {
            //Don't advance the timer if it has passed 2 seconds
            if (remainingSeconds > 2)
            {
                remainingSeconds = 2;
            }
        }
        else
        {
            remainingSeconds = 2;
        }
        UpdateDisplay();
    }

}
