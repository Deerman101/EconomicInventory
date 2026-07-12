using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronArtifactEffect : MonoBehaviour
{
    private SpawnArea spawnArea;
    private GameBootstrap bootstrap;

    private void Start()
    {
        spawnArea = FindObjectOfType<SpawnArea>();
        bootstrap = FindObjectOfType<GameBootstrap>();
    }

    public int Modify(ItemInstance item, int amount)
    {
        if (item.Resource.Type != ResourceType.Iron)
            return amount;

        if (Random.value <= 0.1f)
        {
            DestroyItem(item);
            return 0;
        }

        return amount * 10;
    }

    private void DestroyItem(ItemInstance item)
    {
        if (item.View == null)
            return;

        bootstrap.Inventory.Remove(item);
        ItemView view = spawnArea.Spawn(bootstrap.CreateRandomItem());
        view.ConfigureProduction(bootstrap.Inventory);

        Destroy(item.View.gameObject);
        Debug.Log("Железный арт наконец-то отработал!!!!!!");
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
