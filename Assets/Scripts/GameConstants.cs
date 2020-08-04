using UnityEngine;
using System.Collections;

/// <summary>
/// Global game constants. 
/// </summary>
public class GameConstants
{
    //Number of rows of beans; does not change
    public const int NUM_GAME_ROWS = 5;

    //Currently this is a fixed cost but it might need to be linear
    //or otherwise scale to calculated correct payouts -- TBD
    public const int SECOND_CHANCE_COST = 1;

}
