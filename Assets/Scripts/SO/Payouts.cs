using UnityEngine;

/// <summary>
/// Defines the payout structure.  Payouts are in the form of a table
/// with each index in a table corresponding to the bet amount (as
/// a bet index).
/// Value at an index is the total amount payed out for that specific
/// win.  Note it is possible for multiple wins to stack across win types.
/// 
/// If second chance is played the odds are different so a different table
/// is used to calculate the win.
/// </summary>
public class Payouts : ScriptableObject
{
    //Number of entries should always match number of possible raw bet indices

    //Probability of a flush is roughly the ame as 4 unordered picks.
    public static float[] flushWin = new float[]{ 75.0f, 150.0f, 400.0f };

    //Currently same as regular flush
    public static float[] secc_flushWin = new float[]{ 75.0f, 150.0f, 400.0f };

    //Any order of rows containing a match.  These are mutually exclusive
    //of eachother (i.e don't add a match_1 and match_2 if 2 are matches).
    //Assumes bets of 1, 2, 5 dollars, respectively
    // currently odds based on a 0.65% win rate, rounded down to a whole number
    private static float[] anyOrder_0_match = new float[] { 0.0f, 1.0f, 3.0f };
    private static float[] anyOrder_1_match = new float[] { 5.0f, 10.0f, 25.0f };
    private static float[] anyOrder_2_match = new float[] { 20.0f, 50.0f, 120.0f };
    private static float[] anyOrder_3_match = new float[] { 100.0f, 200.0f, 500.0f };
    private static float[] anyOrder_4_match = new float[] { 400.0f, 800.0f, 2000.0f };
    private static float[] anyOrder_5_match = new float[] { 1600.0f, 3300.0f, 8000.0f };

    //Combined anyOrder matches for easy lookup by # of matches
    public static float[][] anyOrder_match = new float[][]{
        anyOrder_0_match,
        anyOrder_1_match,
        anyOrder_2_match,
        anyOrder_3_match,
        anyOrder_4_match,
        anyOrder_5_match };



    //Second chance win tables
    private static float[] secc_anyOrder_0_match = new float[] { 1.0f, 2.0f, 5.0f };
    private static float[] secc_anyOrder_1_match = new float[] { 5.0f, 10.0f, 25.0f };
    private static float[] secc_anyOrder_2_match = new float[] { 20.0f, 40.0f, 100.0f };
    private static float[] secc_anyOrder_3_match = new float[] { 65.0f, 130.0f, 320.0f };
    private static float[] secc_anyOrder_4_match = new float[] { 200.0f, 400.0f, 1000.0f };
    private static float[] secc_anyOrder_5_match = new float[] { 650.0f, 1300.0f, 3200.0f };

    //Combined anyOrder matches for easy lookup by # of matches
    public static float[][] secc_anyOrder_match = new float[][]{
        secc_anyOrder_0_match,
        secc_anyOrder_1_match,
        secc_anyOrder_2_match,
        secc_anyOrder_3_match,
        secc_anyOrder_4_match,
        secc_anyOrder_5_match };



}
