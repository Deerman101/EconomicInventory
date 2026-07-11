using Enums;
using System.Collections.Generic;
using UnityEngine;

public class DragDropController : MonoBehaviour
{
    public static DragDropController Instance { get; private set; }

    [SerializeField] private RectTransform dragLayer;
    [SerializeField] private GridView gridView;
    [SerializeField] private GameBootstrap bootstrap;
    [SerializeField] private SpawnArea spawnArea;
    [SerializeField] private ItemDestroyZone destroyZone;

    private ItemView draggedItem;
    private RectTransform draggedRect;
    private Vector2 grabOffsetInDragLayer;

    private void Awake()
    {
        Instance = this;
    }

    public void BeginDrag(ItemView item)
    {
        if (draggedItem != null)
            return;

        draggedItem = item;
        draggedRect = item.GetComponent<RectTransform>();
        ItemInstance instance = item.Item;

        if (instance.State == ItemState.Inventory)
            bootstrap.Inventory.Remove(instance);
        else if (instance.State == ItemState.Spawn)
            spawnArea.NotifyTaken(instance);

        instance.State = ItemState.Dragging;

        draggedRect.SetParent(dragLayer, true);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragLayer,
            Input.mousePosition,
            null,
            out Vector2 mouseLocal);

        Vector2 draggedRect2D = draggedRect.localPosition;

        grabOffsetInDragLayer = draggedRect2D - mouseLocal;
    }

    private void Update()
    {
        if (draggedItem == null)
            return;

        if (Input.GetMouseButtonDown(1))
            RotateDraggedItem();

        if (Input.GetMouseButton(0))
            MoveDraggedItem();

        if (Input.GetMouseButtonUp(0))
            Drop();
    }

    private void MoveDraggedItem()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragLayer,
            Input.mousePosition,
            null,
            out Vector2 mouseLocal);

        draggedRect.localPosition = mouseLocal + grabOffsetInDragLayer;
    }

    private void RotateDraggedItem()
    {
        ItemInstance item = draggedItem.Item;
        item.Rotation = ItemShapeMath.RotateClockwise(item.Rotation);
        draggedItem.Redraw();
    }

    private void Drop()
    {
        Vector2 screen = Input.mousePosition;

        if (destroyZone.IsInside(screen))
        {
            DestroyItem();

            ClearDrag();
            return;
        }

        if (TryResolveDropAnchor(screen, out Vector2Int anchor) && bootstrap.Inventory.CanPlace(draggedItem.Item, anchor))
            PlaceOnGrid(anchor);
        else
            ReturnToSpawn();

        ClearDrag();
    }

    private bool TryResolveDropAnchor(Vector2 screenPosition, out Vector2Int anchor)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragLayer,
            screenPosition,
            null,
            out Vector2 mouseInDragLayer);

        Vector2 itemPivotInDragLayer = mouseInDragLayer + grabOffsetInDragLayer;
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(
            null,
            dragLayer.TransformPoint(itemPivotInDragLayer));

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            gridView.ItemsLayer,
            screenPoint,
            null,
            out Vector2 localInItemsLayer);

        return gridView.TryLocalPointToAnchor(localInItemsLayer, out anchor);
    }

    private void PlaceOnGrid(Vector2Int anchor)
    {
        ItemInstance item = draggedItem.Item;
        List<ItemInstance> displaced = bootstrap.Inventory.GetOverlappingItems(item, anchor);

        bootstrap.Inventory.Place(item, anchor);
        gridView.SnapItemToAnchor(draggedRect, anchor);

        foreach (ItemInstance other in displaced)
        {
            if (other == item)
                continue;

            bootstrap.Inventory.Remove(other);
            spawnArea.ReturnInstance(other);
        }
    }

    private void DestroyItem()
    {
        if (!destroyZone.CanDestroy())
        {
            ReturnToSpawn();
            return;
        }

        destroyZone.PayCost();

        ItemInstance item = draggedItem.Item;

        if (item.State == ItemState.Inventory)
            bootstrap.Inventory.Remove(item);

        Destroy(draggedItem.gameObject);

        ItemInstance newItem = bootstrap.CreateRandomItem();
        ItemView view = spawnArea.Spawn(newItem);
        view.ConfigureProduction(bootstrap.Inventory);
    }

    private void ReturnToSpawn()
    {
        spawnArea.ReturnView(draggedItem);
    }

    private void ClearDrag()
    {
        draggedItem = null;
        draggedRect = null;
    }
}
