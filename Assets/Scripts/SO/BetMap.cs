using UnityEngine;

public class BetMap : ScriptableObject
{

    public const int BET_ONE_IDX = 0;
    public const int BET_TWO_IDX = 1;
    public const int BET_FIVE_IDX = 2;

    public const int MAX_BET_IDX = 3; //max index + 1

    /// <summary>
    /// Converts a bet index (arbitrary assignment) into
    /// a dollar amount represented by that index.
    /// If the index does not represent a valid bet then 
    /// zero is returned.
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public static float GetBetFromIdx(int idx)
    {
        float result = 0.0f;
        switch (idx)
        {
            case BET_ONE_IDX:
                result = 1.0f;
                break;

            case BET_TWO_IDX:
                result = 2.0f;
                break;

            case BET_FIVE_IDX:
                result = 5.0f;
                break;
        }
        return result;
    }

}