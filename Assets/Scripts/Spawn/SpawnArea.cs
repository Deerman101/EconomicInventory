using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField]
    private ItemView itemPrefab;

    [SerializeField]
    private Transform parent;

    private List<ItemInstance> items = new();

    public void Spawn(ItemInstance item)
    {
        items.Add(item);

        ItemView view =
            Instantiate(
                itemPrefab,
                parent
            );

        view.Bind(item);

        view.transform.localPosition =
            new Vector3(
                items.Count * 80,
                0,
                0
            );
    }
}