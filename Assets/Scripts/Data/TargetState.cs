using UnityEngine;
using UnityEditor;

/// <summary>
/// A TargetState represents the final disposition of the game state at the
/// end of a round.  Inputs that match the target state are winning inputs
/// (as defined by the game engine).
/// </summary>
public class TargetState : ScriptableObject
    
    //TODO fields:
    //Seed ID, Round selections, second chance selection, hasFlush flag & color, etc
{
    /// <summary>
    /// Create a target state from a seed value
    /// </summary>
    /// <param name="seed"></param>
    /// <returns></returns>
    public static TargetState FromSeed(long seed)
    {
        //TODO
        Debug.Log("TODO - From Seed");
        return null;
    }
}