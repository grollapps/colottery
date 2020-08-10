using UnityEngine;
using System.Collections;

/// <summary>
/// A pop up error message that shows for some amount of time.
/// </summary>
public class ErrorBubble : MonoBehaviour
{
    [SerializeField]
    private float timeToLive = 3.0f;

    private bool isShowing = false;
    private float endTime = 0.0f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowing)
        {
            if (Time.time > endTime)
            {
                isShowing = true;
                gameObject.SetActive(false);
            }
        }

    }

    public void Show()
    {
        isShowing = true;
        endTime = Time.time + timeToLive;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        isShowing = false;
        endTime = Time.time;
        gameObject.SetActive(false);
    }
}
