using System;
using UnityEngine;

public class ArtifactShop : MonoBehaviour
{
    public static ArtifactShop Instance { get; private set; }

    public event Action<ArtifactDefinition> OnArtifactBought;

    [Header("Available artifacts")]
    [SerializeField] private ArtifactDefinition[] artifacts;

    private int currentIndex;

    public ArtifactDefinition CurrentArtifact
    {
        get
        {
            if (currentIndex >= artifacts.Length)
                return null;

            return artifacts[currentIndex];
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public bool BuyCurrentArtifact()
    {
        ArtifactDefinition artifact = CurrentArtifact;

        if (artifact == null)
            return false;

        if (!CanBuy(artifact))
            return false;

        Pay(artifact);

        OnArtifactBought?.Invoke(artifact);

        currentIndex++;

        return true;
    }

    private bool CanBuy(ArtifactDefinition artifact)
    {
        ResourceStorage storage = ResourceStorage.Instance;

        return storage.Wheat >= artifact.WheatCost && storage.Wood >= artifact.WoodCost && storage.Iron >= artifact.IronCost;
    }

    private void Pay(ArtifactDefinition artifact)
    {
        ResourceStorage storage = ResourceStorage.Instance;

        storage.Remove(Enums.ResourceType.Wheat, artifact.WheatCost);
        storage.Remove(Enums.ResourceType.Wood, artifact.WoodCost);
        storage.Remove(Enums.ResourceType.Iron, artifact.IronCost);

        Debug.Log("Bought artifact: " + artifact.ArtifactName);
    }
}