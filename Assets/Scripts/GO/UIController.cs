using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles tracking and updating the UI
/// </summary>
public class UIController : MonoBehaviour
{

    private static UIController _instance;
    public static UIController Instance { get { return _instance; } }

    [SerializeField]
    private User user;

    [SerializeField]
    private Text bankText;

    [SerializeField]
    private Text lastWinText;

    [SerializeField]
    private Text wagerText;

    [SerializeField]
    private Text timerText;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError("Duplicate UIController detected");
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (user == null)
        {
            throw new System.Exception("No user: " + gameObject.name);
        }
        
        if (bankText == null)
        {
            throw new System.Exception("No bankText: " + gameObject.name);
        }
        
        if (lastWinText == null)
        {
            throw new System.Exception("No lastWinText: " + gameObject.name);
        }
        
        if (wagerText == null)
        {
            throw new System.Exception("No wagerText: " + gameObject.name);
        }
        
        if (timerText == null)
        {
            throw new System.Exception("No timerText: " + gameObject.name);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRemainingTimeText(float remainingSeconds)
    {
        timerText.text = FormatTime(remainingSeconds);
    }

    public void SetWagerText(float value)
    {
        wagerText.text = FormatMoney(value);
    }

    public void SetBankText(float value)
    {
        bankText.text = FormatMoney(value);
    }

    public void SetLastWinText(float value)
    {
        lastWinText.text = FormatMoney(value);
    }

    private string FormatMoney(float input)
    {
        return "$" + input.ToString("F");
    }

    private string FormatTime(float seconds)
    {
        int wholeSec = Mathf.CeilToInt(seconds);
        int mins = Mathf.FloorToInt(wholeSec / 60);
        int secPart = wholeSec % 60;
        //Debug.Log("Seconds: " + seconds);
        //Debug.Log("mins: " + mins + ", secPart: " + secPart);
        return string.Format("{0,0:D1}:{1,0:D2}", mins, secPart);

    }
}
