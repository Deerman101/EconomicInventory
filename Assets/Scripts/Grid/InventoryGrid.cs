using Enums;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid
{
    public int Width { get; }
    public int Height { get; }

    private readonly GridCell[,] _cells;

    public InventoryGrid(int width, int height)
    {
        Width = width;
        Height = height;

        _cells = new GridCell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _cells[x, y] = new GridCell
                {
                    Position = new Vector2Int(x, y)
                };
            }
        }
    }

    public GridCell GetCell(int x, int y)
    {
        if (!Inside(x, y))
            return null;

        return _cells[x, y];
    }

    public bool Inside(int x, int y)
    {
        return x >= 0 &&
               y >= 0 &&
               x < Width &&
               y < Height;
    }

    public List<Vector2Int> GetItemCells(ItemInstance item, Vector2Int anchor)
    {
        List<Vector2Int> result = new();

        foreach (Vector2Int cell in ItemShapeMath.GetRotatedCells(item.Shape.Cells, item.Rotation))
            result.Add(anchor + cell);

        return result;
    }

    public bool CanPlace(ItemInstance item, Vector2Int anchor)
    {
        foreach (var pos in GetItemCells(item, anchor))
        {
            if (!Inside(pos.x, pos.y))
                return false;
        }

        return true;
    }

    public List<ItemInstance> GetOverlappingItems(ItemInstance item, Vector2Int anchor)
    {
        HashSet<ItemInstance> result = new();

        foreach (var pos in GetItemCells(item, anchor))
        {
            var occupant = _cells[pos.x, pos.y].Occupant;

            if (occupant != null)
                result.Add(occupant);
        }

        return new List<ItemInstance>(result);
    }

    public void Remove(ItemInstance item)
    {
        foreach (var cell in _cells)
        {
            if (cell.Occupant == item)
                cell.Occupant = null;
        }

        InventoryEvents.OnItemRemoved?.Invoke(item); 
    }

    public void Place(ItemInstance item, Vector2Int anchor)
    {
        Remove(item);

        item.Anchor = anchor;
        item.State = ItemState.Inventory;

        foreach (var pos in GetItemCells(item, anchor))
        {
            _cells[pos.x, pos.y].Occupant = item;
        }

        InventoryEvents.OnItemPlaced?.Invoke(item); 
    }
}