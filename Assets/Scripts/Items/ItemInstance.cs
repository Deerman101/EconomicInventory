using Enums;
using UnityEngine;

public class ItemInstance
{
    public int ID;

    // форма предмета
    public ItemShapeDefinition Shape;

    // какой ресурс производит
    //public ResourceType ResourceType;
    public ResourceDefinition Resource;

    // поворот
    public RotationState Rotation;

    // главная клетка производства
    public Vector2Int CoreCell;

    // позиция в инвентаре
    public Vector2Int Anchor;

    public ItemState State;

    // отображение
    public ItemView View;

    public int CellCount =>
        Shape.Cells.Count;
}