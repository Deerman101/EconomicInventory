using Enums;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private TileDefinition greenTile;
    [SerializeField] private TileDefinition yellowTile;
    [SerializeField] private TileDefinition redTile;

    [SerializeField] private GameConfig config;
    [SerializeField] private GridView gridView;

    [SerializeField] private SpawnArea spawnArea;
    [SerializeField] private ItemShapeDefinition[] shapes;

    private ItemFactory factory;

    public InventoryGrid Inventory { get; private set; }


    private void Start()
    {
        CreateGrid();

        factory = new ItemFactory(shapes); // вот тут мб всё пойдёт по 3.14...

        for (int i = 0; i < config.StartItems; i++)
        {
            ItemInstance item =
                factory.CreateRandom();

            spawnArea.Spawn(item);
        }

        gridView.Build(
            Inventory,
            config.CellSize
        );
    }


    private void CreateGrid()
    {
        Inventory = new InventoryGrid(
            config.Width,
            config.Height
        );


        GenerateTiles();
    }


    //private void GenerateTiles()
    //{
    //    for (int x = 0; x < Inventory.Width; x++)
    //    {
    //        for (int y = 0; y < Inventory.Height; y++)
    //        {
    //            Inventory
    //                .GetCell(x, y)
    //                .TileType = TileType.Green;
    //        }
    //    }
    //}

    private void GenerateTiles()
    {
        TileGenerator generator = new TileGenerator(greenTile,
        yellowTile,
        redTile);

        generator.Generate(Inventory);
    }
}