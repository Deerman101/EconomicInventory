using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalArtifactEffect : MonoBehaviour
{
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            timer = 0;

            if (GetItemCount() >= 3)
                ProduceRandom();
        }
    }

    public int Modify(ItemInstance item, int amount)
    {
        return amount;
    }

    private int GetItemCount()
    {
        ItemView[] items = FindObjectsOfType<ItemView>();

        int count = 0;

        foreach (ItemView view in items)
            if (view.Item != null && view.Item.State == ItemState.Inventory)
                count++;

        return count;
    }

    private void ProduceRandom()
    {
        ResourceType type = (ResourceType)Random.Range(0, 3);

        ResourceStorage.Instance.Add(type, 1);
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
