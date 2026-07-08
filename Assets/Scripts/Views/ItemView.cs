using Enums;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField]
    private Image cellPrefab;

    private ItemInstance _item;

    public void Bind(ItemInstance item)
    {
        _item = item;
        item.View = this;

        Draw();
    }

    private void Draw()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        foreach (Vector2Int cell in _item.Shape.Cells)
        {
            Image image =
                Instantiate(
                    cellPrefab,
                    transform
                );

            image.rectTransform.localPosition =
                new Vector3(
                    cell.x * 64,
                    cell.y * 64,
                    0
                );

            image.color =
                GetColor(
                    _item.ResourceType
                );
        }
    }



    private Color GetColor(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Wheat:
                return Color.cyan;

            case ResourceType.Wood:
                return Color.black;

            case ResourceType.Iron:
                return Color.gray;
        }

        return Color.white;
    }
}