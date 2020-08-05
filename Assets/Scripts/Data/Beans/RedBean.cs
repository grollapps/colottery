using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="ARedBean", menuName ="RedBean Obj", order = 51)]
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