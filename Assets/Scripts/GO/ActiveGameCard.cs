﻿using System.Runtime.CompilerServices;
using UnityEngine;

public class ActiveGameCard : MonoBehaviour
{
    //one index per row. Value indicates which column is selected, -1 is none.
    private int[] rowSelections;
    //one button per row, holds ref to selected button; null if not selected.
    private FillButton[] rowButtons;

    private bool isSecChanceSelected;
    private FillButton secChanceButton;
    
    //-1 for no bet, 1 for first, 2 for second, 3 for third, etc
    private int betIdxSelected;
    private FillButton betButton;

    private const int maxBetIndex = 3; //3 bets, $1, $2, $5
    private int numRows;

    private GameController gameController;

    private User user;

    [SerializeField]
    private ErrorBubble noBetSelectedError;

    [SerializeField]
    private ErrorBubble notAllRowsSelectedError;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start: ActiveGameCard " + gameObject.name);
        numRows = GameConstants.NUM_GAME_ROWS;

        rowSelections = new int[numRows];
        rowButtons = new FillButton[numRows];

        ClearCard();

        gameController = FindObjectOfType<GameController>();
        if (gameController == null)
        {
            throw new System.Exception("Could not find game controller: " + gameObject.name);
        }

        //There's only one user in our prototype
        user = gameController.GetUser();
        if (user == null)
        {
            throw new System.Exception("Could not find User instance: " + gameObject.name);
        }

        if (noBetSelectedError == null)
        {
            throw new System.Exception("missing no bet error: " + gameObject.name);
        }

        if (notAllRowsSelectedError == null)
        {
            throw new System.Exception("missing no row error: " + gameObject.name);
        }


        UpdateTotalWager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Copies the active game card to a game card state, submits
    /// it to the game controller and clears the active card so it
    /// is ready for a new set of entries.
    /// </summary>
    public void handleSubmit()
    {
        Debug.Log("handleSubmit active game card");
        GameCardState gameCard = CreateGameCard();
        if (gameCard == null)
        {
            Debug.Log("Card not submitted due to errors");
        }
        else
        {
            gameController.EnterNextRound(user, this, gameCard);
            ClearCard();
        }
    }

  
    /// <summary>
    /// Clears the active game card of any user selections
    /// </summary>
    public void ClearCard()
    {
        Debug.Log("ClearCard");
        for (int i = 0; i < rowSelections.Length; i++)
        {
            rowSelections[i] = -1;
            if (rowButtons[i] != null)
            {
                rowButtons[i].SetSelected(false);
            }
            rowButtons[i] = null;
        }

        isSecChanceSelected = false;
        if (secChanceButton != null)
        {
            secChanceButton.SetSelected(false);
        }
        secChanceButton = null;

        betIdxSelected = -1;
        if (betButton != null)
        {
            betButton.SetSelected(false);
        }
        betButton = null;
    }

    /// <summary>
    /// Takes the current user selections and turns them into
    /// a GameCardState for submission.
    /// </summary>
    /// <returns></returns>
    private GameCardState CreateGameCard()
    {
        GameCardState gc = new GameCardState();
        Bean[] choices = new Bean[numRows];
        int numNull = 0;
        for (int r = 0; r < choices.Length; r++)
        {
            Bean curBean = null;
            switch (rowSelections[r])
            {
                case BeanMap.RED_IDX:
                    curBean = ScriptableObject.CreateInstance<RedBean>();
                    break;

                case BeanMap.YELLOW_IDX:
                    curBean = ScriptableObject.CreateInstance<YellowBean>();
                    break;

                case BeanMap.PURPLE_IDX:
                    curBean = ScriptableObject.CreateInstance<PurpleBean>();
                    break;

                case BeanMap.GREEN_IDX:
                    curBean = ScriptableObject.CreateInstance<GreenBean>();
                    break;

                case BeanMap.WHITE_IDX:
                    curBean = ScriptableObject.CreateInstance<WhiteBean>();
                    break;

                default:
                    numNull++;
                    break;
            }
            choices[r] = curBean;
        }
        if (numNull > 0)
        {
            ShowCardNotFilledError();
            gc = null; 
        }
        else
        {
            if (betIdxSelected < 0)
            {
                ShowBetNotSelectedError();
                gc = null;
            }
            else
            {
                gc.FillCard(choices, isSecChanceSelected, betIdxSelected);
            }
        }

        UpdateTotalWager();

        if (gc != null)
        {
            HideAllErrorMessages();
        }
        return gc;
    }

    private void ShowCardNotFilledError()
    {
        Debug.Log("Show not filled Error");
        notAllRowsSelectedError.Show();
    }

    private void ShowBetNotSelectedError()
    {
        Debug.Log("Show no bet error");
        noBetSelectedError.Show();
    }

    private void HideAllErrorMessages()
    {
        notAllRowsSelectedError.Hide();
        noBetSelectedError.Hide();
    }

    /// <summary>
    /// Selects a position on the gamecard givena button corresponding
    /// to that position.  Unselects other buttons in the same row if
    /// necessary.
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    public void SelectRowCol(FillButton btn, int row, int col)
    {
        if (row < 0 || row >= numRows)
        {
            throw new System.Exception("Invalid row: " + row);
        }
        if (btn == null)
        {
            throw new System.Exception("Invalid button: null");
        }

        Debug.Log("Select fill button row:" + row + ", col:" + col);
        if (rowButtons[row] != null)
        {
            //some other button is already set; it will be unset first
            Debug.Log("Unset button in same row");
            rowButtons[row].SetSelected(false);
        }
        btn.SetSelected(true);
        rowButtons[row] = btn;
        rowSelections[row] = col;
        UpdateTotalWager();

    }
    public void SelectBetIndex(FillButton btn, int betIndex)
    {
        if (betIndex < 0 || betIndex >= maxBetIndex)
        {
            throw new System.Exception("Invalid bet index: " + betIndex);
        }
        if (btn == null)
        {
            throw new System.Exception("Invalid button: null");
        }

        Debug.Log("Select fill button betIndex:" + betIndex);
        if (betButton != null)
        {
            //some other button is already set; it will be unset first
            Debug.Log("Unset bet button in same row");
            betButton.SetSelected(false);
        }
        btn.SetSelected(true);
        betButton = btn;
        betIdxSelected = betIndex;
        UpdateTotalWager();
    }

    public void SelectSecondChance(FillButton btn)
    {
        //toggle on select
        if (secChanceButton != null)
        {
            Debug.Log("Unselecting second chance button");
            secChanceButton.SetSelected(false);
            isSecChanceSelected = false;
            secChanceButton = null;
        } else {
            Debug.Log("Selecting second change button");
            btn.SetSelected(true);
            isSecChanceSelected = true;
            this.secChanceButton = btn;
        }
        UpdateTotalWager();
    }

    private void UpdateTotalWager()
    {
        float betVal = BetMap.GetBetFromIdx(betIdxSelected);
        float secChanceVal = isSecChanceSelected ? GameConstants.SECOND_CHANCE_COST : 0;
        float totalWager = betVal + secChanceVal;
        UIController.Instance.SetWagerText(totalWager);
    }


    /// <summary>
    /// Clones this board as-is.  The returned clone will look the same but
    /// all buttons will be disabled.
    /// </summary>
    /// <returns></returns>
    public ActiveGameCard CreateClone()
    {
        GameObject cloneObj = Instantiate<GameObject>(gameObject);
        ActiveGameCard cloneCard = cloneObj.GetComponent<ActiveGameCard>();
        cloneCard.DisableInput();
        return cloneCard;
    }

    public void DisableInput()
    {
        Debug.Log("DisableInput: " + gameObject.name);
        FillButton[] allButtons = GetComponentsInChildren<FillButton>();
        if (allButtons != null)
        {
            for (int i = 0; i < allButtons.Length; i++)
            {
                allButtons[i].DisableButton();
            }
        }
    }
}
