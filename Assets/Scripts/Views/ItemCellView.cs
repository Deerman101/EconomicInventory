using UnityEngine;
using UnityEngine.UI;

public class ItemCellView : MonoBehaviour
{
    [SerializeField]
    private Image image;

    public void Setup(Color color)
    {
        image.color = color;
    }
}