using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// User gamecard fill "button".  Represents a generic area on the
/// card that can be filled in (selected) by the user.
/// </summary>
[RequireComponent(typeof(Image))]
public class FillButton : MonoBehaviour
{
    private bool isSelected = false;

    //Set to the game card being filled
    [SerializeField]
    protected ActiveGameCard gameCard;

    private Image myImage;

    private Color unSelectedColor;
    private Color selectedColor;

    void Start()
    {
        if (gameCard == null)
        {
            throw new System.Exception("No gameCard assigned to FillButton: " + gameObject.name);
        }

        myImage = GetComponent<Image>();
        if (myImage == null)
        {
            throw new System.Exception("No image on FillButton: " + gameObject.name);
        }

        Color baseColor = myImage.color;
        unSelectedColor = new Color(baseColor.r, baseColor.g, baseColor.b, 0.0f);
        selectedColor = new Color(baseColor.r, baseColor.g, baseColor.b, 1.0f);
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void SetSelected(bool isSet)
    {
        isSelected = isSet;
        if (isSelected)
        {
            myImage.color = selectedColor;
        }
        else
        {
            myImage.color = unSelectedColor;
        }
    }

    public void DisableButton()
    {
        Button btn = GetComponent<Button>();
        btn.interactable = false;
    }
}
