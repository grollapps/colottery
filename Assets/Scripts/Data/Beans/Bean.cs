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

    protected string colorId;

    public bool IsEqual(Bean other)
    {
        if (other == null)
        {
            return false;
        }
        return colorId.Equals(other.colorId);
    }

}