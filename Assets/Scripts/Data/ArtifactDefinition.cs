using UnityEngine;

[CreateAssetMenu(
    fileName = "ArtifactDefinition",
    menuName = "Inventory/Artifact Definition"
)]
public class ArtifactDefinition : ScriptableObject
{
    [Header("Info")]
    public string ArtifactName;
    
    [TextArea(3, 6)]
    public string Description;

    [Header("Visual")]
    public Sprite Icon;

    [Header("Logic")]
    public GameObject ArtifactPrefab;
    //public ArtifactView ArtifactPrefab;

    [Header("Price")]
    public int WheatCost;
    public int WoodCost;
    public int IronCost;
}