using UnityEngine;
using System.Collections;

/// <summary>
/// Fill buttons, specifically in the color selection grid.
/// </summary>
public class GridFillButton : FillButton
{
    //Identifies the row of this button, 0-based
    [SerializeField]
    private int rowIndex;

    //Identifies the column of this button, 0-based
    [SerializeField]
    private int colIndex;


    public void handleClick()
    {
        if (IsSelected())
        {
            //already selected, do nothing
        }
        else
        {
            //SetSelected(true); //done by game card
            gameCard.SelectRowCol(this, rowIndex, colIndex);
        }
    }
}
