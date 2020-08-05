using UnityEngine;
using System.Collections;

public class BeanGo : MonoBehaviour
{

    private Bean props;

    public void SetProps(Bean bean)
    {
        props = bean;
        gameObject.GetComponent<Material>().color = props.GetColorValue();

    }

    void Start()
    {

    }

    void Update()
    {

    }
}
