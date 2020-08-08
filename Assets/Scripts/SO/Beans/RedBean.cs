using UnityEngine;

public class RedBean : Bean
{

    public RedBean()
    {
        colorId = "red";
    }
    public override Color GetColorValue()
    {
        return new Color(1.0f, 0.0f, 0.0f);
    }
}