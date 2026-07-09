using UnityEngine;

public readonly struct GridLayoutMath
{
    public float CellSize { get; }
    public float Spacing { get; }
    public float Step => CellSize + Spacing;

    public GridLayoutMath(float cellSize, float spacing)
    {
        CellSize = cellSize;
        Spacing = spacing;
    }

    public Vector2 AnchorToLocal(Vector2Int anchor) =>
        new Vector2(anchor.x * Step, anchor.y * Step);

    public Vector2Int LocalToAnchor(Vector2 localFromOrigin)
    {
        int x = Mathf.FloorToInt(localFromOrigin.x / Step);
        int y = Mathf.FloorToInt(localFromOrigin.y / Step);
        return new Vector2Int(x, y);
    }
}
