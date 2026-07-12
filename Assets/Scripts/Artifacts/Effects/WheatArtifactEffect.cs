using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatArtifactEffect : MonoBehaviour
{
    public int Modify(ItemInstance item, int amount)
    {
        if (item.Resource.Type != ResourceType.Wheat)
            return amount;

        return amount + item.CellCount;
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
