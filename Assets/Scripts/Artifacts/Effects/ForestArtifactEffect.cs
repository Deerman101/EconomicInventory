using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestArtifactEffect : MonoBehaviour
{
    [Header("Дополнительный ресурс")]
    [SerializeField] private ResourceDefinition additionalResource;       
    [SerializeField] private int additionalResourceSpacing = 40;       

    public int Modify(ItemInstance item, int amount)
    {
        if (item.Resource.Type != ResourceType.Wood)
            return amount;

        if (Random.value <= 0.5f)
        {
            ResourceStorage.Instance.Add(ResourceType.Wheat, amount);
            ResourcePopupSpawner.Instance.Spawn(item.View.GetCorePopupPosition() + Vector3.down * additionalResourceSpacing, additionalResource, amount);
        }

        return amount;
    }

    private void OnEnable()
    {
        ItemProducer.OnProductionCalculate += Modify;
    }

    private void OnDisable()
    {
        ItemProducer.OnProductionCalculate -= Modify;
    }
}
