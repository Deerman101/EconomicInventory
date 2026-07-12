using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArtifactShopUI : MonoBehaviour
{
    [SerializeField] private ArtifactShop shop;

    [Header("UI")]
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private TMP_Text priceText;

    private void Start()
    {
        Refresh();
        shop.OnShopChanged += Refresh;
    }

    public void Refresh()
    {
        if (shop.HasArtifacts)
        {
            buttonText.text = "Купить артефакт";
            ArtifactDefinition artifact = shop.CurrentArtifact;

            if (artifact == null)
            {
                priceText.text = "";
                return;
            }

            ArtifactPrice price = shop.GetArtifactPrice(artifact);
            priceText.text = $"Цена: wheat {price.Wheat} wood {price.Wood} iron {price.Iron}";
        }
        else
        {
            buttonText.text = "Купить предмет";
            priceText.text = $"Цена: wheat {shop.ItemWheatCost} wood {shop.ItemWoodCost} iron {shop.ItemIronCost}";
        }
    }
}
