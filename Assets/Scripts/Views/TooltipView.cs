using TMPro;
using UnityEngine;


public class TooltipView : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

    public void Setup(string title, string description)
    {
        titleText.text = title;
        descriptionText.text = description;
    }
}