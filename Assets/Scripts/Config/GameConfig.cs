using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Game Config")]
public class GameConfig : ScriptableObject
{
    [Header("Grid")]
    public int Width = 8;
    public int Height = 8;

    [Header("Visual")]
    public float CellSize = 64f;

    [Header("Spawn")]
    public int StartItems = 5;
}
