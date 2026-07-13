using Enums;
using UnityEngine;

public class MyArtifactEffect : MonoBehaviour
{
    private int Modify(ItemInstance item, int amount)
    {
        int newAmount = amount * 2;

        return newAmount;
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