using Enums;
using System.Collections.Generic;
using UnityEngine;

public static class ItemShapeMath
{
    public static Vector2Int RotateCell(Vector2Int cell, RotationState rotation)
    {
        return rotation switch
        {
            RotationState.Deg90 => new Vector2Int(cell.y, -cell.x),
            RotationState.Deg180 => new Vector2Int(-cell.x, -cell.y),
            RotationState.Deg270 => new Vector2Int(-cell.y, cell.x),
            _ => cell
        };
    }

    public static IEnumerable<Vector2Int> GetRotatedCells(
        IReadOnlyList<Vector2Int> shape,
        RotationState rotation)
    {
        foreach (Vector2Int cell in shape)
            yield return RotateCell(cell, rotation);
    }

    public static RotationState RotateClockwise(RotationState rotation) =>
        rotation switch
        {
            RotationState.Deg0 => RotationState.Deg90,
            RotationState.Deg90 => RotationState.Deg180,
            RotationState.Deg180 => RotationState.Deg270,
            _ => RotationState.Deg0
        };
}
