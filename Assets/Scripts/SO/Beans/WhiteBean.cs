using UnityEngine;

public class WhiteBean : Bean
{

    public WhiteBean()
    {
        colorId = "white";
    }
    public override Color GetColorValue()
    {
        return new Color(1.0f, 1.0f, 1.0f);
    }
}