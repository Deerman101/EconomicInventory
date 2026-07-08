using Enums;
using UnityEngine;

public class GridCell
{
    public Vector2Int Position;

    public TileDefinition Tile;

    public ItemInstance Occupant;

    public bool IsFree => Occupant == null;
}