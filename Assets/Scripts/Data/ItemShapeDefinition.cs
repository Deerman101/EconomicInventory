using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Shape")]
public class ItemShapeDefinition : ScriptableObject
{
    public string ShapeName;

    public List<Vector2Int> Cells = new();
}
