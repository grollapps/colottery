using UnityEngine;

/// <summary>
/// Game object representing a bean.
/// </summary>
public class BeanGo : MonoBehaviour
{
    private Bean props;

    public void SetProps(Bean bean)
    {
        props = bean;
        gameObject.GetComponent<Renderer>().material.color = props.GetColorValue();

    }

}
