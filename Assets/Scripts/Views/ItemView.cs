using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemView : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ItemCellView cellPrefab;
    [SerializeField] private ResourceIconView resourceIconPrefab;

    private ItemInstance _item;
    private GameConfig _config;

    public ItemInstance Item => _item;

    public void Configure(GameConfig config)
    {
        _config = config;
    }

    public void Bind(ItemInstance item)
    {
        _item = item;
        item.View = this;
        Redraw();
    }

    public void Redraw()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        float step = _config != null ? _config.CellStep : 80f;
        float cellSize = _config != null ? _config.CellSize : 60f;
        Vector2Int rotatedCore = ItemShapeMath.RotateCell(_item.CoreCell, _item.Rotation);

        foreach (Vector2Int cell in ItemShapeMath.GetRotatedCells(_item.Shape.Cells, _item.Rotation))
        {
            ItemCellView cellView = Instantiate(cellPrefab, transform);
            RectTransform cellRect = cellView.GetComponent<RectTransform>();

            cellRect.pivot = Vector2.zero;
            cellRect.anchorMin = Vector2.zero;
            cellRect.anchorMax = Vector2.zero;
            cellRect.sizeDelta = new Vector2(cellSize, cellSize);
            cellRect.anchoredPosition = new Vector2(cell.x * step, cell.y * step);

            cellView.Setup(_item.Resource.Color);

            if (cell == rotatedCore)
                CreateResourceIcon(cellView.transform);
        }

        RectTransform root = GetComponent<RectTransform>();
        root.pivot = Vector2.zero;
        root.anchorMin = Vector2.zero;
        root.anchorMax = Vector2.zero;
    }

    private void CreateResourceIcon(Transform cellParent)
    {
        ResourceIconView icon = Instantiate(resourceIconPrefab, cellParent);
        icon.Setup(_item.Resource);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        DragDropController.Instance.BeginDrag(this);
    }
}
