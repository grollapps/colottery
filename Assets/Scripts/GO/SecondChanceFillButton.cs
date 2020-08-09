using UnityEngine;

public class SecondChanceFillButton : FillButton
{
    public void handleClick()
    {
        gameCard.SelectSecondChance(this);
    }
}
