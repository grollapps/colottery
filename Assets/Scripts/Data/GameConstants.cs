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

    public const int SEC_BETWEEN_ROUNDS = 4 * 60; //4 mins

    //This is arbitrary but helps keep the screen tidy
    public const int MAX_ENTRIES_PER_ROUND = 6;

}
