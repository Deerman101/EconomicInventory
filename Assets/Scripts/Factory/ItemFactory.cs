using Enums;
using UnityEngine;

public class ItemFactory
{
    private int _idCounter = 1;

    private readonly ItemShapeDefinition[] _shapes;

    public ItemFactory(ItemShapeDefinition[] shapes)
    {
        _shapes = shapes;
    }

    public ItemInstance CreateRandom()
    {
        ItemShapeDefinition shape =
            _shapes[
                Random.Range(
                    0,
                    _shapes.Length
                )
            ];

        ItemInstance item = new ItemInstance();

        item.ID = _idCounter++;

        item.Shape = shape;

        item.ResourceType =
            (ResourceType)
            Random.Range(
                0,
                3
            );

        item.Rotation =
            RotationState.Deg0;

        item.CoreCell =
            shape.Cells[
                Random.Range(
                    0,
                    shape.Cells.Count
                )
            ];

        item.State =
            ItemState.Spawn;

        return item;
    }
}