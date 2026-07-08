using UnityEngine;

public class GridView : MonoBehaviour
{
    [SerializeField] private CellView cellPrefab;

    private float _cellSize;


    public void Build(InventoryGrid grid, float cellSize)
    {
        _cellSize = cellSize;


        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                CreateCell(grid.GetCell(x, y));
            }
        }
    }


    private void CreateCell(GridCell cell)
    {
        CellView view = Instantiate(
            cellPrefab,
            transform
        );


        view.transform.localPosition =
            new Vector3(
                cell.Position.x * _cellSize,
                cell.Position.y * _cellSize,
                0
            );


        view.Setup(cell.Tile);
    }
}