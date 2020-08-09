using UnityEngine;

/// <summary>
/// Defines the payout structure.  Payouts are in the form of a table
/// with each index in a table corresponding to the bet amount (as
/// a bet index).
/// Value at an index is the total amount payed out for that specific
/// win.  Note it is possible for multiple wins to stack across win types.
/// 
/// </summary>
public class Payouts : ScriptableObject
{
    //Number of entries should always match number of possible raw bet indices

    //TODO - add in to payout equation.  Probability of a flush is roughly the
    //same as 4 unordered picks.
    public static float[] flushWin = new float[]{ 100.0f, 200.0f, 500.0f };

    //Any order of rows containing a match.  These are mutually exclusive
    //of eachother (i.e don't add a match_1 and match_2 if 2 are matches).
    //Assumes bets of 1, 2, 5 dollars, respectively
    // TODO - currently odds based on a 0.65% win rate, rounded down to a whole number
    private static float[] anyOrder_0_match = new float[] { 1.0f, 2.0f, 5.0f };
    private static float[] anyOrder_1_match = new float[] { 6.0f, 13.0f, 35.0f };
    private static float[] anyOrder_2_match = new float[] { 30.0f, 60.0f, 150.0f };
    private static float[] anyOrder_3_match = new float[] { 125.0f, 250.0f, 600.0f };
    private static float[] anyOrder_4_match = new float[] { 500.0f, 1000.0f, 2500.0f };
    private static float[] anyOrder_5_match = new float[] { 2000.0f, 4000.0f, 10150.0f };

    //Combined anyOrder matches for easy lookup by # of matches
    public static float[][] anyOrder_match = new float[][]{
        anyOrder_0_match,
        anyOrder_1_match,
        anyOrder_2_match,
        anyOrder_3_match,
        anyOrder_4_match,
        anyOrder_5_match };







}
