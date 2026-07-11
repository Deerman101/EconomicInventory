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
    [SerializeField] private ResourceDefinition[] resources;

    private ItemFactory factory;

    public InventoryGrid Inventory { get; private set; }

    public GameConfig Config => config;

    private void Start()
    {
        CreateGrid();

        spawnArea.Configure(config);

        gridView.Build(Inventory, config);

        factory = new ItemFactory(shapes, resources);

        for (int i = 0; i < config.StartItems; i++)
        {
            ItemInstance item = factory.CreateRandom();
            ItemView view = spawnArea.Spawn(item);
            view.ConfigureProduction(Inventory);
        }
    }

    private void CreateGrid()
    {
        Inventory = new InventoryGrid(config.Width, config.Height);

        GenerateTiles();
    }

    private void GenerateTiles()
    {
        TileGenerator generator = new TileGenerator(greenTile, yellowTile, redTile);

        generator.Generate(Inventory);
    }

    public ItemInstance CreateRandomItem()
    {
        return factory.CreateRandom();
    }
}