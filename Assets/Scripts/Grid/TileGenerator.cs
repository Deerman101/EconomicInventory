using UnityEngine;

public class TileGenerator
{
    private readonly TileDefinition _green;
    private readonly TileDefinition _yellow;
    private readonly TileDefinition _red;

    public TileGenerator(TileDefinition green, TileDefinition yellow, TileDefinition red)
    {
        _green = green;
        _yellow = yellow;
        _red = red;
    }

    public void Generate(InventoryGrid grid)
    {
        int width = grid.Width;
        int height = grid.Height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid.GetCell(x, y).Tile =
                    CalculateTile(
                        x,
                        y,
                        width,
                        height
                    );
            }
        }
    }

    private TileDefinition CalculateTile(int x, int y, int width, int height)
    {
        int distance = Mathf.Min(x, y, width - 1 - x, height - 1 - y);

        if (distance == 0)
            return _red;

        if (distance == 1)
            return _yellow;

        return _green;
    }
}