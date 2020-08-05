using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lays out virtual bean columns
/// </summary>
public class BeanRow : MonoBehaviour
{
    [SerializeField]
    private BeanGo beanGoPrefab;

    private Vector3 anchorPos;

    [SerializeField]
    private float rowHeight;

    [SerializeField]
    private float cellGap;

    private int beanIndex = -1;
    private Bean beanObj;

    // Start is called before the first frame update
    void Start()
    {
        anchorPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// After creating the bean this should be called to populate its properties.
    /// </summary>
    /// <param name="bean"></param>
    public void SetBean(Bean bean)
    {
        BeanGo go = GameObject.Instantiate<BeanGo>(beanGoPrefab);
        go.SetProps(bean);
        //Validate col num here?
        int colNum = BeanMap.GetColIndex(bean);
        SetPosition(go.gameObject, colNum);
    }

    private void SetPosition(GameObject go, int colNum)
    {
        //assumes rows span across the page
        float x = anchorPos.x + (colNum * cellGap);
        float y = anchorPos.y;
        float z = anchorPos.z;
        go.transform.position = new Vector3(x, y, z);
    }
}
