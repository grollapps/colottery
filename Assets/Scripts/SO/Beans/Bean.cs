using UnityEngine;
using UnityEditor;

/// <summary>
/// Attributes of a single object in the game. Represents a "bean" in the literal
/// sense but more abstractly it is the object under test; the user is trying to
/// guess the properties of a certain "bean" and success is measured by correctly
/// guessing those properties per bean.
/// </summary>
public abstract class Bean : ScriptableObject
{

    //colorId is a string like "red" or "yellow" that specifically identifies this bean (unique per row, only)
    protected string colorId;

    /// <summary>
    /// Returns named color, unique to this bean.
    /// </summary>
    /// <returns></returns>
    public string GetColorName()
    {
        return colorId;
    }

    public abstract Color GetColorValue();

    public bool IsEqual(Bean other)
    {
        if (other == null)
        {
            return false;
        }
        return colorId.Equals(other.colorId);
    }

}