using UnityEngine;

/// <summary>
/// Lays out virtual bean columns
/// </summary>
public class BeanRow : MonoBehaviour
{
    [SerializeField]
    private BeanGo beanGoPrefab;

    protected Vector3 anchorPos;

    [SerializeField]
    protected float cellGap;

    //These keep quick references to the current content
    private int beanColIdx = -1;
    private Bean beanObj = null;
    //This is the actual model of the bean as seen in game
    private BeanGo beanGo = null;

    //Where the bean starts its life before animation
    private Vector3 startAnimPos;
    //Target position where the next bean will transition to
    private Vector3 targetPos;

    private bool isAnimating = false;
    private float animStartTime = 0.0f;
    private float endAnimTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        anchorPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating)
        {
            AnimateFrame();
        }
    }

    /// <summary>
    /// Start an animation on the bean for this row
    /// </summary>
    /// <param name="durationSec">max length of animation in seconds</param>
    /// <param name="startingT">value between 0 and 1, parameter of where
    /// to start the animation (0 is start, 1 is end)</param>
    public void BeginAnimation(float durationSec, float startingT)
    {
        animStartTime = Time.time;
        endAnimTime = Time.time + (durationSec * (1.0f - startingT));
        isAnimating = true;
        //actual animation happens via Update

        if (startingT >= 1.0f || durationSec <= 0)
        {
            isAnimating = false;
            SetPosition(targetPos);
        }
    }

    private void AnimateFrame()
    {
        float paramT = (Time.time - animStartTime) / (endAnimTime - animStartTime);
        if (paramT >= 1.0f)
        {
            isAnimating = false;
            SetPosition(targetPos);
        }
        else
        {
            Vector3 pos = Vector3.Lerp(startAnimPos, targetPos, paramT);
            SetPosition(pos);
        }
    }

    public bool IsAnimating()
    {
        return isAnimating;
    }

    /// <summary>
    /// After creating the bean this should be called to populate its properties.
    /// This will create the appropriate bean color and position.
    /// </summary>
    /// <param name="bean"></param>
    public void SetBean(Bean bean){
        if (beanGo != null)
        {
            throw new System.Exception("Previous bean in row not cleaned up: " + beanGo.name);
        }
        int idx = BeanMap.GetColIndex(bean);
        SetBean(bean, idx);
    }

    /// <summary>
    /// After creating the bean this should be called to populate its properties.
    /// </summary>
    /// <param name="bean"></param>
    /// <param name="idx">Specify grid to place bean in.</param>
    public void SetBean(Bean bean, int idx)
    {
        if (beanGo != null)
        {
            throw new System.Exception("Previous bean in row not cleaned up: " + beanGo.name);
        }

        this.beanObj = bean;
        beanGo = GameObject.Instantiate<BeanGo>(beanGoPrefab);
        beanGo.SetProps(bean);
        //Validate col num here?
        isAnimating = false;
        beanColIdx = idx;
        targetPos = GetTargetPosition(beanColIdx);
        //TODO initial pos should depend on where bean is coming from
        startAnimPos = new Vector3(targetPos.x, targetPos.y + 20, targetPos.z);
        SetPosition(startAnimPos);
    }

    protected virtual Vector3 GetTargetPosition(int colNum)
    {
        //assumes rows span across the page
        float x = anchorPos.x + (colNum * cellGap);
        float y = anchorPos.y;
        float z = anchorPos.z;
        return new Vector3(x, y, z);
    }

    private void SetPosition(Vector3 pos)
    {
        beanGo.transform.position = pos;
    }

    /// <summary>
    /// Clear Row data.
    /// </summary>
    public void Reset()
    {
        Destroy(beanGo.gameObject);
        beanGo = null;
        beanColIdx = -1;
        //unsure if the SO needs to be deleted or if it is one shared instance?
        beanObj = null;
    }
}
