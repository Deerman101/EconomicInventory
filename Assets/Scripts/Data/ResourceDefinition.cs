using Enums;
using UnityEngine;

[CreateAssetMenu(
    fileName = "ResourceDefinition",
    menuName = "Inventory/Resource Definition"
)]
public class ResourceDefinition : ScriptableObject
{
    public ResourceType Type;

    public string DisplayName;

    public Sprite Icon;

    public Color Color;
}