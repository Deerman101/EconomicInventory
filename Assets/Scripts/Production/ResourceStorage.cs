using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    public static ResourceStorage Instance { get; private set; }

    public event Action OnResourcesChanged;

    public int Wheat { get; private set; }
    public int Wood { get; private set; }
    public int Iron { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.Wheat:
                Wheat += amount;
                break;

            case ResourceType.Wood:
                Wood += amount;
                break;

            case ResourceType.Iron:
                Iron += amount;
                break;
        }

        OnResourcesChanged?.Invoke();
    }

    public bool Remove(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.Wheat:

                if (Wheat < amount)
                    return false;

                Wheat -= amount;
                break;

            case ResourceType.Wood:

                if (Wood < amount)
                    return false;

                Wood -= amount;
                break;

            case ResourceType.Iron:

                if (Iron < amount)
                    return false;

                Iron -= amount;
                break;
        }

        OnResourcesChanged?.Invoke();

        return true;
    }
}
