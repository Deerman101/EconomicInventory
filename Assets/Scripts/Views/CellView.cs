using Enums;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] private Image image;

    public void Setup(TileDefinition tile)
    {
        image.color = tile.Color;
    }
}