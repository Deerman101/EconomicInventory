using Enums;
using UnityEngine;

[CreateAssetMenu(
    fileName = "TileDefinition",
    menuName = "Inventory/Tile Definition"
)]
public class TileDefinition : ScriptableObject
{
    public TileType Type;

    [Header("Visual")]
    public Color Color;


    [Header("Production")]
    [Range(0f, 1f)]
    public float ProductionMultiplier;

    [Range(0f, 10f)]
    public float ProductionDelay;
}