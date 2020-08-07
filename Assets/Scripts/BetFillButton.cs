using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetFillButton : FillButton
{
    //left-to-right, 0-based index on the card
    [SerializeField]
    private int betIndex;

    public void handleClick()
    {
        if (IsSelected())
        {
            //already selected, do nothing
        }
        else
        {
            //SetSelected(true); //done by game card
            gameCard.SelectBetIndex(this, betIndex);
        }
    }
}
