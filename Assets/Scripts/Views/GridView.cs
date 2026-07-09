using UnityEngine;

public class GridView : MonoBehaviour
{
    [SerializeField] private CellView cellPrefab;
    [SerializeField] private Transform cellsRoot;
    [SerializeField] private RectTransform itemsLayer;

    public RectTransform ItemsLayer => itemsLayer;

    private GridLayoutMath _layout;
    private int _width;
    private int _height;

    public void Build(InventoryGrid grid, GameConfig config)
    {
        _layout = new GridLayoutMath(config.CellSize, config.CellSpacing);
        _width = grid.Width;
        _height = grid.Height;

        ClearCells();

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
                CreateCell(grid.GetCell(x, y));
        }
    }

    public Vector2 GetGridOriginLocal()
    {
        Rect rect = itemsLayer.rect;
        return new Vector2(rect.xMin, rect.yMin);
    }

    public Vector2 AnchorToLocalPosition(Vector2Int anchor) =>
        GetGridOriginLocal() + _layout.AnchorToLocal(anchor);

    public Vector3 AnchorToWorldPosition(Vector2Int anchor) =>
        itemsLayer.TransformPoint(AnchorToLocalPosition(anchor));

    public bool TryScreenToAnchor(Vector2 screenPosition, out Vector2Int anchor)
    {
        anchor = default;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                itemsLayer,
                screenPosition,
                null,
                out Vector2 local))
            return false;

        Vector2 relative = local - GetGridOriginLocal();
        anchor = _layout.LocalToAnchor(relative);

        return anchor.x >= 0 &&
               anchor.y >= 0 &&
               anchor.x < _width &&
               anchor.y < _height;
    }

    public bool TryLocalPointToAnchor(Vector2 localPointInItemsLayer, out Vector2Int anchor)
    {
        Vector2 relative = localPointInItemsLayer - GetGridOriginLocal();
        anchor = _layout.LocalToAnchor(relative);

        return anchor.x >= 0 &&
               anchor.y >= 0 &&
               anchor.x < _width &&
               anchor.y < _height;
    }

    public void SnapItemToAnchor(RectTransform itemRect, Vector2Int anchor)
    {
        itemRect.SetParent(itemsLayer, false);
        itemRect.pivot = Vector2.zero;
        itemRect.anchorMin = Vector2.zero;
        itemRect.anchorMax = Vector2.zero;
        itemRect.localPosition = AnchorToLocalPosition(anchor);
    }

    public float GetCellSize() => _layout.CellSize;
    public float GetCellStep() => _layout.Step;

    private void ClearCells()
    {
        for (int i = cellsRoot.childCount - 1; i >= 0; i--)
            Destroy(cellsRoot.GetChild(i).gameObject);
    }

    private void CreateCell(GridCell cell)
    {
        CellView view = Instantiate(cellPrefab, cellsRoot);
        RectTransform cellRect = view.GetComponent<RectTransform>();

        cellRect.pivot = Vector2.zero;
        cellRect.anchorMin = Vector2.zero;
        cellRect.anchorMax = Vector2.zero;
        cellRect.sizeDelta = new Vector2(_layout.CellSize, _layout.CellSize);

        Vector2 localInItemsLayer = AnchorToLocalPosition(cell.Position);
        Vector3 world = itemsLayer.TransformPoint(localInItemsLayer);
        cellRect.position = world;

        view.Setup(cell.Tile);
    }
}
