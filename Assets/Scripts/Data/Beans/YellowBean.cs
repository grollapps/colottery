using UnityEngine;
using UnityEditor;

public class YellowBean : Bean
{

    public YellowBean()
    {
        colorId = "yellow";
    }

    public override Color GetColorValue()
    {
        return new Color(1.0f, 1.0f, 0.0f);
    }
}