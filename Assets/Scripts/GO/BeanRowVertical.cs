using UnityEngine;
using System.Collections;

/// <summary>
/// Same as a bean row but the boxes are stacked vertically 
/// instead of horizontally.
/// </summary>
public class BeanRowVertical : BeanRow
{

    protected override Vector3 GetTargetPosition(int colNum)
    {
        //assumes rows span across the page
        float x = anchorPos.x;
        float y = anchorPos.y - (colNum * cellGap);
        float z = anchorPos.z;
        return new Vector3(x, y, z);
    }
}
