using UnityEngine;
using UnityEditor;
using System;

/// <summary>
/// A TargetState represents the final disposition of the game state at the
/// end of a round.  Inputs that match the target state are winning inputs
/// (as defined by the game engine).
/// </summary>
public class TargetState : MonoBehaviour
{
    private long seedId;

    private Bean[] draws = new Bean[GameConstants.NUM_GAME_ROWS];

    private bool hasSecondChance = false;
    private (Bean bean, int row) secondChanceDraw;

    private bool hasFlush = false;
    //if there is a flush, this Bean will match all beans in the flush
    private Bean flushBeanMatch = null;


    //TODO - Most of this random generation code can be modularized and 
    //moved out of this class

    /// <summary>
    /// Create a target state from a seed value
    /// </summary>
    /// <param name="seed"></param>
    /// <returns></returns>
    public static TargetState FromSeed(long seed)
    {
        Debug.Log("Create TargetState FromSeed");

        GameObject newGo = new GameObject();
        
        TargetState result = newGo.AddComponent<TargetState>();
        const int rowCount = GameConstants.NUM_GAME_ROWS;
        Bean lastBean = null;
        bool matchesBean = true;
        //Draw initial beans
        for (int i = 0; i < rowCount; i++)
        {
            result.draws[i] = GetBeanForRow(seed, i);
            if (i == 0)
            {
                lastBean = result.draws[i];
            }
            else
            {
                //check for flush -- all beans must match the first
                bool match = lastBean.IsEqual(result.draws[i]);
                matchesBean = matchesBean && match;
            }
        }

        //Set flush params
        result.hasFlush = matchesBean;
        if (result.hasFlush)
        {
            result.flushBeanMatch = lastBean;
        }

        //set second chance as the "last" new row
        var secondChanceBean = GetBeanForSecondChance(seed);
        result.secondChanceDraw.bean = secondChanceBean.bean;
        result.secondChanceDraw.row = secondChanceBean.row;
        result.hasSecondChance = result.secondChanceDraw.row != -1;

        return result;
    }

    public int GetNumRows()
    {
        return draws.Length;
    }

    /// <summary>
    /// Get the drawing result for the given row.
    /// Assumes the TargetState is already initialized.
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    public Bean GetDrawForRow(int row)
    {
        if (row >= draws.Length || draws[row] == null)
        {
            throw new Exception("Invalid draw row " + row + " (forgot to do drawing first?)");
        }
        return draws[row];
    }


    /// <summary>
    /// Do a drawing for the 'second chance' bean given a seed.  It is possible
    /// the second chance does not yield a draw in which case the returned bean
    /// is null and the returned row is -1;
    /// </summary>
    /// <param name="seed"></param>
    /// <param name="row"></param>
    /// <returns></returns>
    private static (Bean bean, int row) GetBeanForSecondChance(long seed)
    {
        const int mockRow = GameConstants.NUM_GAME_ROWS + 1;
        Bean draw = GetBeanForRow(seed, mockRow);
        int row = GetRandomRow(seed);
        //TODO need to check if the given row has the given bean
        Debug.Log("TODO - check bean row for second chance");
        return (draw, row);
    }

    /// <summary>
    /// Performs a single drawing for the specified row
    /// </summary>
    /// <param name="seed"></param>
    /// <param name="rowNum"></param>
    /// <returns></returns>
    private static Bean GetBeanForRow(long seed, int rowNum)
    {
        //TODO randomize return value according to seed
        Debug.Log("TODO - GetBeanForRow - randomize");
        int numRows = 10; //
        int randResult =  UnityEngine.Random.Range(0, numRows);
        Bean resultBean = null;
        //TODO - numColors should be based on colors in row
        int numColors = 5;
        switch (randResult % numColors) {
            case BeanMap.RED_IDX:
                resultBean = ScriptableObject.CreateInstance<RedBean>();
                break;

            case BeanMap.YELLOW_IDX:
                resultBean = ScriptableObject.CreateInstance<YellowBean>();
                break;

            case BeanMap.PURPLE_IDX:
                resultBean = ScriptableObject.CreateInstance<PurpleBean>();
                break;

            case BeanMap.GREEN_IDX:
                resultBean = ScriptableObject.CreateInstance<GreenBean>();
                break;

            case BeanMap.WHITE_IDX:
                resultBean = ScriptableObject.CreateInstance<WhiteBean>();
                break;
        }
        return resultBean;
    }

    /// <summary>
    /// Get a random row number between 0 and max_rows (exclusive)
    /// </summary>
    /// <param name="seed"></param>
    /// <returns></returns>
    private static int GetRandomRow(long seed)
    {
        //TODO - randomize result according to seed
        Debug.Log("TODO - GetRandomRow - randomize");
        return 0;
    }
}