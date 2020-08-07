using System.Collections;
using System.Collections.Generic;
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

    private const int maxBetIndex = 2; //3 bets, $1, $2, $5
    private int numRows;

    // Start is called before the first frame update
    void Start()
    {
        numRows = GameConstants.NUM_GAME_ROWS;

        rowSelections = new int[numRows];
        rowButtons = new FillButton[numRows];
        for (int i = 0; i < rowSelections.Length; i++)
        {
            rowSelections[i] = -1;
            rowButtons[i] = null;
        }

        isSecChanceSelected = false;
        secChanceButton = null;

        betIdxSelected = -1;
        betButton = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    public void SelectSecondChance(FillButton btn)
    {
        //toggle on select
        if (secChanceButton != null)
        {
            Debug.Log("Unselecting second chance button");
            secChanceButton.SetSelected(false);
            secChanceButton = null;
        } else {
            Debug.Log("Selecting second change button");
            btn.SetSelected(true);
            this.secChanceButton = btn;
        }
    }
}
