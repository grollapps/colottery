using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PayoutTableDisplay : MonoBehaviour
{
    [SerializeField]
    private Image payoutTable_nonSecc;

    [SerializeField]
    private Button showNonSeccButton;

    [SerializeField]
    private Image payoutTable_secc;

    [SerializeField]
    private Button showSeccButton;


    public void ShowNonSeccTable()
    {
        payoutTable_nonSecc.gameObject.SetActive(true);
        payoutTable_secc.gameObject.SetActive(false);
        //since we see the non-secc table we need the opposite button to show
        showSeccButton.gameObject.SetActive(true);
        showNonSeccButton.gameObject.SetActive(false);
    }

    public void ShowSeccTable()
    {
        payoutTable_nonSecc.gameObject.SetActive(false);
        payoutTable_secc.gameObject.SetActive(true);
        //show opposite button
        showSeccButton.gameObject.SetActive(false);
        showNonSeccButton.gameObject.SetActive(true);
    }

}
