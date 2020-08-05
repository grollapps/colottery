using UnityEngine;
using UnityEditor;

public class GreenBean : Bean
{

    public GreenBean()
    {
        colorId = "green";
    }

    public override Color GetColorValue()
    {
        return new Color(0.0f, 1.0f, 0.0f);
    }
}