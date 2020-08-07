using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondChanceFillButton : FillButton
{
    public void handleClick()
    {
        gameCard.SelectSecondChance(this);
    }
}
