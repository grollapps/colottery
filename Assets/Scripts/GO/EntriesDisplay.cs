using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Handles logic to display current and past entries
/// </summary>
public class EntriesDisplay : MonoBehaviour
{
    const int MAX_ENTRIES = GameConstants.MAX_ENTRIES_PER_ROUND;

    //One button per entry, ordered by #
    [SerializeField]
    private List<EntryButton> entryButtons;

    [SerializeField]
    private Text lastRoundTextHeader;

    [SerializeField]
    private Text curRoundTextHeader;

    //Determines where the entry card is displayed
    [SerializeField]
    private Vector3 entryCardPos;

    [SerializeField]
    private float entryCardScale = 0.55f;

    //private List<ActiveGameCard> curBoards = new List<ActiveGameCard>();
    private ActiveGameCard[] curBoards = new ActiveGameCard[MAX_ENTRIES];
    private int curBoardCount = 0;
    private ActiveGameCard curBoardVisible = null;
    private int lastIndexSelected = -1;

    private Canvas canvas;

    void Start(){
        if (entryButtons == null) {
            throw new System.Exception("missing entry buttons: " + gameObject.name);
        }
        if (lastRoundTextHeader == null) {
            throw new System.Exception("missing last round text: " + gameObject.name);
        }
        if (curRoundTextHeader == null) {
            throw new System.Exception("missing current round text: " + gameObject.name);
        }
        if (entryCardPos == null) {
            throw new System.Exception("missing entry card pos: " + gameObject.name);
        }

        //Get a reference to the main UI canvas.  Note this probably won't work right if
        //there are multiple Canvases
        canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            throw new System.Exception("Could not find Canvas object");
        }

    }

    /// <summary>
    /// Resets the Display: clears all boards and goes back to current round text.
    /// </summary>
    public void Reset()
    {
        Debug.Log("EntriesDisplay Reset");
        for(int i = 0; i < curBoards.Length; i++){
            if (curBoards[i] != null)
            {
                Destroy(curBoards[i].gameObject);
                curBoards[i] = null;
            }
        }
        curBoardCount = 0;
        curBoardVisible = null;
        lastIndexSelected = -1;
        SetShowCurrentRoundText();
        UpdateButtons();
    }

    public void SetShowCurrentRoundText()
    {
        curRoundTextHeader.gameObject.SetActive(true);
        lastRoundTextHeader.gameObject.SetActive(false);
    }

    public void SetShowLastRoundText()
    {
        curRoundTextHeader.gameObject.SetActive(false);
        lastRoundTextHeader.gameObject.SetActive(true);
    }

    public void AddBoardToDisplay(ActiveGameCard board)
    {
        board.gameObject.name = "EntryDisplay Board " + curBoardCount;
        Debug.Log("AddBoardToDisplay: " + board.gameObject.name);
        Transform boardT = board.transform;
        //boardT.SetParent(canvas.gameObject.transform, false);
        boardT.localScale = new Vector3(entryCardScale, entryCardScale, entryCardScale);
        boardT.SetParent(gameObject.transform, false);
        RectTransform rt = board.GetComponent<RectTransform>();
        rt.localPosition = entryCardPos;
        //boardT.position = entryCardPos;
        curBoards[curBoardCount++] = board;
        board.gameObject.SetActive(false); //initially hidden
        UpdateButtons();

        //Show the last board added
        ShowBoard(curBoardCount - 1);
    }

    private void UpdateButtons()
    {
        //Hide any buttons that don't have a corresponding card to show
        for (int i = curBoardCount; i < MAX_ENTRIES; i++)
        {
            entryButtons[i].Hide();
        }

        //The rest of the buttons are visible
        for (int i = 0; i < curBoardCount; i++)
        {
            entryButtons[i].Show(false);
            entryButtons[i].SetClickIndex(i);
        }

    }

    public void ShowBoard(int index)
    {
        Debug.Log("ShowBoard index:" + index);
        if (index < 0 || index >= MAX_ENTRIES)
        {
            throw new System.Exception("Invalid index to show: " + index);
        }

        if (curBoardVisible != null)
        {
            curBoardVisible.gameObject.SetActive(false); //hide exising
        }
        curBoardVisible = curBoards[index];
        if (curBoardVisible != null)
        {
            curBoardVisible.gameObject.SetActive(true);
        }

        lastIndexSelected = index;
    }

    public void ClickCallback(int clickIndex)
    {
        ShowBoard(clickIndex);
    }

}
