using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

/// <summary>
/// Button designed to show a game board entry when pressed.
/// This button also has highlighting to show when a win has
/// occurred on the board.
/// </summary>
[RequireComponent(typeof(Button))]
public class EntryButton : MonoBehaviour
{
    [SerializeField]
    private Color normalColor;

    [SerializeField]
    private Color winColor;

    [SerializeField]
    private EntriesDisplay entriesDisplay;

    private Button myButton;
    private int myClickIndex;

    void Start()
    {
        myButton = GetComponent<Button>();
        if (entriesDisplay == null)
        {
            throw new System.Exception("No entries display assigned: " + gameObject.name);
        }
    }

    /// <summary>
    /// Generic handler that can be assigned to fire when the button is selected
    /// </summary>
    public void HandleClick()
    {
        Debug.Log("HandleClick:" + myClickIndex);
        entriesDisplay.ClickCallback(myClickIndex);
    }

    /// <summary>
    /// Hides the button from view
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(bool showWin)
    {
        gameObject.SetActive(true);
        if (myButton != null)
        {
            ColorBlock cb = myButton.colors;
            if (showWin)
            {
                cb.normalColor = winColor;
            }
            else
            {
                cb.normalColor = normalColor;
            }
            myButton.colors = cb;
        }
    }

    public void SetClickIndex(int idx)
    {
        myClickIndex = idx;
    }
}
