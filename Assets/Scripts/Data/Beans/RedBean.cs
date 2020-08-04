using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="ARedBean", menuName ="RedBean Obj", order = 51)]
public class RedBean : Bean
{

    public RedBean()
    {
        colorId = "red";
    }
}