using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles complex animations appearing on the main game screen.
/// </summary>
public class BoardAnimController : MonoBehaviour
{
    //Time it takes to do one round of drawing all beans
    [SerializeField]
    private float drawingTimeSec = 7.0f;

    [SerializeField]
    private float secChanceDrawingTimeSec = 3.0f;

    private int numBeans = GameConstants.NUM_GAME_ROWS;
    private int numSecChance = 1;

    private List<BeanRow> curRows;
    private int curIdx;
    private int maxIdx;
    private bool animDone;
    private bool waitingOnCurIdx;
    private float startParamT;

    // Start is called before the first frame update
    void Start()
    {
        ResetAnim();
    }

    // Update is called once per frame
    void Update()
    {
        if (animDone == false)
        {
            AnimateCurrentIndex();
        }
        
    }

    private void ResetAnim()
    {
        curIdx = -1;
        maxIdx = -1;
        animDone = true;
        waitingOnCurIdx = false;
    }

    /// <summary>
    /// Animate the drawing of all beans in beanRows.
    /// The objects must already exist and be in their initial
    /// position to animate from.  startParamT can be used to skip
    /// animation by setting this parameter to 1 (all animations start
    /// in their target state).
    /// </summary>
    /// <param name="beanRows"></param>
    /// <param name="startParamT">Value between 0 and 1</param>
    public void AnimateBeanDraws(List<BeanRow> beanRows, float startParamT)
    {
        Debug.Log("AnimateBeanDraws");
        Debug.Log("TODO -- second chance draw");
        curIdx = 0;
        curRows = beanRows;
        if (beanRows == null || beanRows.Count == 0)
        {
            Debug.Log("No beans, no animation to do");
            maxIdx = 0;
            animDone = true;
            this.startParamT = 1;
        }
        else
        {
            maxIdx = beanRows.Count;
            animDone = false;
            this.startParamT = startParamT;
        }
        waitingOnCurIdx = false;
        Debug.Log("curIdx:" + curIdx + ", maxIdx:" + maxIdx);
    }

    private void AnimateCurrentIndex()
    {
        if (curIdx >= maxIdx) {
            Debug.Log("Done Animating all indices");
            //all done
            animDone = true;
        } else {
            BeanRow curRow = curRows[curIdx];
            if (waitingOnCurIdx)
            {
                //we were waiting on the current animation. Is it done?
                if (curRow.IsAnimating() == false)
                {
                    Debug.Log("Finished anim for index " + curIdx);
                    //curRow done, move to next
                    waitingOnCurIdx = false;
                    curIdx++;
                }
            }
            else
            {
                //the current index has not started yet
                float perBeanTime = drawingTimeSec / numBeans;
                Debug.Log("Starting Animation for index: " + curIdx + ", durationSec:" + perBeanTime + ", paramT:" + startParamT);
                curRow.BeginAnimation(perBeanTime, startParamT);
                waitingOnCurIdx = true;
            }
        }
        
    }

    public bool IsAnimDone()
    {
        return animDone;
    }

}
