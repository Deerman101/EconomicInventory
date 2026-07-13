using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ItemView : MonoBehaviour, IPointerDownHandler
{
    [Header("Префабы")]
    [SerializeField] private ItemCellView cellPrefab;
    [SerializeField] private ResourceIconView resourceIconPrefab;

    private ItemInstance _item;
    private GameConfig _config;
    private ItemProducer producer;

    public ItemInstance Item => _item;

    [Header("Тесты")]
    public ResourceIconView currentResourceIcon; 
    private InventoryGrid inventory; 

    private void Awake()
    {
        producer = GetComponent<ItemProducer>();
    }

    private void OnEnable()
    {
        InventoryEvents.OnItemPlaced += StartIconAnimation;
        InventoryEvents.OnItemRemoved += StopIconAnimation;
    }


    private void OnDisable()
    {
        InventoryEvents.OnItemPlaced -= StartIconAnimation;
        InventoryEvents.OnItemRemoved -= StopIconAnimation;
    }

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

    public void ConfigureProduction(InventoryGrid grid)
    {
        inventory = grid; 

        producer = gameObject.AddComponent<ItemProducer>();

        producer.Configure(grid);
    }

    private void CreateResourceIcon(Transform cellParent)
    {
        currentResourceIcon = Instantiate(resourceIconPrefab, cellParent);
        currentResourceIcon.Setup(_item.Resource);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        DragDropController.Instance.BeginDrag(this);
    }

    private float GetProductionSpeed()
    {
        Vector2Int core = ItemShapeMath.RotateCell(_item.CoreCell, _item.Rotation);

        GridCell cell = FindCell(_item.Anchor + core);

        if (cell == null)
            return 1f;

        return cell.Tile.ProductionDelay;
    }

    private GridCell FindCell(Vector2Int position)
    {
        if (inventory == null)
            return null;

        return inventory.GetCell(position.x, position.y);
    }

    private void StartIconAnimation(ItemInstance item)
    {
        if (item != _item)
            return;

        if (item.State != ItemState.Inventory)
            return;

        if (currentResourceIcon == null)
            return;

        currentResourceIcon.PlayProductionAnimation(GetProductionSpeed());
    }

    private void StopIconAnimation(ItemInstance item)
    {
        if (item != _item)
            return;

        if (currentResourceIcon == null)
            return;

        currentResourceIcon.StopAnimation();
    }

    public Vector3 GetCorePopupPosition()
    {
        Vector2Int rotatedCore =
            ItemShapeMath.RotateCell(
                _item.CoreCell,
                _item.Rotation
            );

        float step = _config.CellStep;

        Vector3 localPosition = new Vector3(
                rotatedCore.x * step + _config.CellSize / 2,
                rotatedCore.y * step + _config.CellSize / 2,
                0
            );

        return transform.TransformPoint(localPosition);
    }
}
