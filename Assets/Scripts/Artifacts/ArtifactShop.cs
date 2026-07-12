using System;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactShop : MonoBehaviour
{
    public static ArtifactShop Instance { get; private set; }

    public event Action OnShopChanged;

    [Header("Артефакты")]
    [SerializeField] private List<ArtifactDefinition> artifacts;
    [SerializeField] private Transform artifactParent;
    [SerializeField] private float priceMultiplier = 1.2f;

    [Header("Цена предмета после покупки всех артов")]
    [SerializeField] private int itemWheatCost;
    [SerializeField] private int itemWoodCost;
    [SerializeField] private int itemIronCost;

    [Header("Покупка предметов")]
    [SerializeField] private SpawnArea spawnArea;
    [SerializeField] private GameBootstrap bootstrap;

    private readonly List<ArtifactDefinition> availableArtifacts = new();
    private readonly Dictionary<ArtifactDefinition, ArtifactPrice> artifactPrices = new();

    private ArtifactDefinition currentArtifact;

    public bool HasArtifacts => availableArtifacts.Count > 0;

    public ArtifactDefinition CurrentArtifact => currentArtifact;

    public int ItemWheatCost => itemWheatCost;
    public int ItemWoodCost => itemWoodCost;
    public int ItemIronCost => itemIronCost;

    private void Awake()
    {
        Instance = this;

        availableArtifacts.AddRange(artifacts);

        foreach (ArtifactDefinition artifact in availableArtifacts)
        {
            artifactPrices.Add(artifact,
                new ArtifactPrice(
                    artifact.WheatCost,
                    artifact.WoodCost,
                    artifact.IronCost
                )
            );
        }

        SelectRandomArtifact();
    }

    public ArtifactPrice GetArtifactPrice(ArtifactDefinition artifact)
    {
        if (artifact == null)
            return null;

        return artifactPrices[artifact];
    }

    public void Buy()
    {
        if (HasArtifacts)
            BuyArtifact();
        else
            BuyItem();
    }

    private void BuyArtifact()
    {
        if (currentArtifact == null)
            return;

        if (!CanBuy(currentArtifact))
            return;

        Pay(currentArtifact);
        ActivateArtifact(currentArtifact);
        availableArtifacts.Remove(currentArtifact);
        IncreaseArtifactPrices();
        SelectRandomArtifact();

        OnShopChanged?.Invoke();
    }

    private void SelectRandomArtifact()
    {
        if (availableArtifacts.Count == 0)
        {
            currentArtifact = null;
            return;
        }

        currentArtifact = availableArtifacts[UnityEngine.Random.Range(0, availableArtifacts.Count)];
    }

    private void ActivateArtifact(ArtifactDefinition artifact)
    {
        if (artifact.ArtifactPrefab == null)
        {
            Debug.LogError("Не назначен префаб артефакта: " + artifact.ArtifactName);

            return;
        }

        GameObject obj = Instantiate(artifact.ArtifactPrefab, artifactParent);

        obj.SetActive(true);
    }

    private void IncreaseArtifactPrices()
    {
        foreach (ArtifactDefinition artifact in availableArtifacts)
            artifactPrices[artifact].Multiply(priceMultiplier);
    }

    private void BuyItem()
    {
        if (!CanBuyItem())
            return;

        PayItem();

        ItemInstance item = bootstrap.CreateRandomItem();
        ItemView view = spawnArea.Spawn(item);

        view.ConfigureProduction(bootstrap.Inventory);

        IncreaseItemPrice();

        OnShopChanged?.Invoke();

        Debug.Log("Покупка предмета прошла успешно");
    }

    private bool CanBuy(ArtifactDefinition artifact)
    {
        ArtifactPrice price = artifactPrices[artifact];

        return ResourceStorage.Instance.Wheat >= price.Wheat &&
            ResourceStorage.Instance.Wood >= price.Wood &&
            ResourceStorage.Instance.Iron >= price.Iron;
    }

    private void Pay(ArtifactDefinition artifact)
    {
        ArtifactPrice price = artifactPrices[artifact];

        ResourceStorage.Instance.Remove(Enums.ResourceType.Wheat, price.Wheat);
        ResourceStorage.Instance.Remove(Enums.ResourceType.Wood, price.Wood);
        ResourceStorage.Instance.Remove(Enums.ResourceType.Iron, price.Iron);
    }

    private bool CanBuyItem()
    {
        return ResourceStorage.Instance.Wheat >= itemWheatCost &&
            ResourceStorage.Instance.Wood >= itemWoodCost &&
            ResourceStorage.Instance.Iron >= itemIronCost;
    }

    private void PayItem()
    {
        ResourceStorage.Instance.Remove(Enums.ResourceType.Wheat, itemWheatCost);
        ResourceStorage.Instance.Remove(Enums.ResourceType.Wood, itemWoodCost);
        ResourceStorage.Instance.Remove(Enums.ResourceType.Iron, itemIronCost);
    }

    private void IncreaseItemPrice()
    {
        itemWheatCost = Mathf.RoundToInt(itemWheatCost * priceMultiplier);
        itemWoodCost = Mathf.RoundToInt(itemWoodCost * priceMultiplier);
        itemIronCost = Mathf.RoundToInt(itemIronCost * priceMultiplier);
    }
}