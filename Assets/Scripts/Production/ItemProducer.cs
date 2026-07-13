using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProducer : MonoBehaviour
{
    public static event Func<ItemInstance, int, int> OnProductionCalculate;

    private InventoryGrid inventory;
    private ItemView view;

    public void Configure(InventoryGrid grid)
    {
        inventory = grid;
    }

    private void Awake()
    {
        view = GetComponent<ItemView>();
    }

    private void Update() // Переделать - плохо!!!!
    {
        if (inventory == null)
            return;

        ItemInstance item = view.Item;

        if (item == null)
            return;

        if (item.State != ItemState.Inventory)
            return;

        GridCell productionCell = GetProductionCell(item);

        if (productionCell == null)
            return;

        if (productionCell.Tile.Type == TileType.Red)
            return;

        item.ProductionTimer += Time.deltaTime;

        if (item.ProductionTimer >= productionCell.Tile.ProductionDelay)
        {
            item.ProductionTimer = 0f;

            Produce(item);
        }
    }

    private void Produce(ItemInstance item)
    {
        GridCell productionCell = GetProductionCell(item);

        if (productionCell != null && productionCell.Tile.Type == TileType.Red)
            if (FindObjectOfType<MyArtifactEffect>() == null)
                return;

        int amount = 1;

        if (OnProductionCalculate != null)
        {
            foreach (Func<ItemInstance, int, int> modifier
                in OnProductionCalculate.GetInvocationList())
            {
                amount = modifier(item, amount);
            }
        }

        ResourceStorage.Instance.Add(
            item.Resource.Type,
            amount
        );

        SpawnPopup(item, amount);
    }

    private GridCell GetProductionCell(ItemInstance item)
    {
        Vector2Int rotatedCore = ItemShapeMath.RotateCell(item.CoreCell, item.Rotation);
        Vector2Int gridPos = item.Anchor + rotatedCore;

        return inventory.GetCell(gridPos.x, gridPos.y);
    }

    private void SpawnPopup(ItemInstance item, int amount)
    {
        ResourcePopupSpawner.Instance.Spawn(view.GetCorePopupPosition(), item.Resource, amount);
    }
}
