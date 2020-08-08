using UnityEngine;

public class PurpleBean : Bean
{

    public PurpleBean()
    {
        colorId = "purple";
    }
    public override Color GetColorValue()
    {
        return new Color(1.0f, 0.0f, 1.0f);
    }
}