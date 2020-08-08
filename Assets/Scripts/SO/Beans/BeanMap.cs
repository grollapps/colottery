using UnityEngine;
using System;

/// <summary>
/// Maps a column number to a bean color and vice-versa.
/// This assumes beans of a certain color are always located in the 
/// same column number (0-based).
/// </summary>
public class BeanMap
{
    //Quick LUT for color indices (map color index to column). Must be unique.
    public const short RED_IDX = 0;
    public const short YELLOW_IDX = 1; 
    public const short PURPLE_IDX = 2;
    public const short GREEN_IDX = 3;
    public const short WHITE_IDX = 4;

    public static string GetColorForIndex(short idx)
    {
        //name must match bean name string
        switch (idx)
        {
            case 0:
                return "red";
                //break;

            case 1:
                return "yellow";
                //break;

            case 2:
                return "purple";
                //break;

            case 3:
                return "green";
                //break;

            case 4:
                return "white";
                //break;
        }
        throw new Exception("Invalid color index: " + idx);
    }

    public static int GetColIndex(Bean bean)
    {
        switch (bean.GetColorName())
        {
            case "red":
                return RED_IDX;

            case "yellow":
                return YELLOW_IDX;

            case "purple":
                return PURPLE_IDX;

            case "green":
                return GREEN_IDX;

            case "white":
                return WHITE_IDX;
        }
        throw new Exception("Invalid bean color: " + bean.GetColorName());
    }
}