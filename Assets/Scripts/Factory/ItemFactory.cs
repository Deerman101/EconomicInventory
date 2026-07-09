using Enums;
using UnityEngine;

public class ItemFactory
{
    private int _nextId = 1;
    private readonly ItemShapeDefinition[] _shapes;
    private readonly ResourceDefinition[] _resources;

    public ItemFactory(
    ItemShapeDefinition[] shapes,
    ResourceDefinition[] resources)
    {
        _shapes = shapes;
        _resources = resources;
    }

    /// <summary>
    /// Создать полностью случайный предмет.
    /// </summary>
    public ItemInstance CreateRandom()
    {
        ItemShapeDefinition shape =
            _shapes[
                Random.Range(0, _shapes.Length)
            ];

        return Create(shape);
    }

    /// <summary>
    /// Создать предмет определённой формы.
    /// </summary>
    public ItemInstance Create(ItemShapeDefinition shape)
    {
        ItemInstance item = new ItemInstance();

        item.ID = _nextId++;
        item.Shape = shape;

        item.Resource = GetRandomResource();
        item.Rotation = RotationState.Deg0;
        item.CoreCell = GetRandomCoreCell(shape);
        item.State = ItemState.Spawn;

        return item;
    }

    private ResourceDefinition GetRandomResource()
    {
        return _resources[
            Random.Range(
                0,
                _resources.Length
            )
        ];
    }

    private Vector2Int GetRandomCoreCell(ItemShapeDefinition shape)
    {
        return shape.Cells[
            Random.Range(
                0,
                shape.Cells.Count
            )
        ];
    }
}