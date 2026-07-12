using Enums;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private ItemView itemPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private float itemSpacing = 90f;

    private readonly List<ItemInstance> items = new();

    private GameConfig config;

    public void Configure(GameConfig gameConfig)
    {
        config = gameConfig;
    }

    public ItemView Spawn(ItemInstance item)
    {
        if (!items.Contains(item))
            items.Add(item);

        ItemView view = Instantiate(itemPrefab, content);

        view.Configure(config);
        view.Bind(item);

        item.State = ItemState.Spawn;

        Relayout();

        return view;
    }

    public void NotifyTaken(ItemInstance item)
    {
        items.Remove(item);
    }

    public void ReturnView(ItemView view)
    {
        ItemInstance item = view.Item;
        item.State = ItemState.Spawn;

        if (!items.Contains(item))
            items.Add(item);

        view.transform.SetParent(content, false);

        Relayout();
    }

    public void ReturnInstance(ItemInstance item)
    {
        item.State = ItemState.Spawn;

        if (!items.Contains(item))
            items.Add(item);

        if (item.View == null)
        {
            Spawn(item);
            return;
        }

        item.View.transform.SetParent(content, false);

        Relayout();
    }

    private void Relayout()
    {
        for (int i = 0; i < items.Count; i++)
        {
            ItemView view = items[i].View;

            if (view == null)
                continue;

            RectTransform rect = view.GetComponent<RectTransform>();

            rect.pivot = Vector2.zero;
            rect.anchorMin = new Vector2(0, 0.5f);
            rect.anchorMax = new Vector2(0, 0.5f);
            rect.anchoredPosition = new Vector2(i * itemSpacing, 0);
        }
    }
}