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
    private float cellGap;

    //These keep quick references to the current content
    private int beanColIdx = -1;
    private Bean beanObj = null;
    private BeanGo beanGo = null;

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
        if (beanGo != null)
        {
            throw new System.Exception("Previous bean in row not cleaned up: " + beanGo.name);
        }

        this.beanObj = bean;
        beanGo = GameObject.Instantiate<BeanGo>(beanGoPrefab);
        beanGo.SetProps(bean);
        //Validate col num here?
        beanColIdx = BeanMap.GetColIndex(bean);
        SetPosition(beanGo.gameObject, beanColIdx);
    }

    private void SetPosition(GameObject go, int colNum)
    {
        //assumes rows span across the page
        float x = anchorPos.x + (colNum * cellGap);
        float y = anchorPos.y;
        float z = anchorPos.z;
        go.transform.position = new Vector3(x, y, z);
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
