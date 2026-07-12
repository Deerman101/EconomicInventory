using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyArea : MonoBehaviour
{
    [Header("Цена удаления в ресах:")]
    [SerializeField] private int wheatCost = 10;

    public bool IsInside(Vector2 screenPosition)
    {
        RectTransform rect = GetComponent<RectTransform>();

        return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPosition, null);
    }

    public bool CanDestroy()
    {
        if (ResourceStorage.Instance == null)
            return false;

        return ResourceStorage.Instance.Wheat >= wheatCost;
    }

    public void PayCost() => ResourceStorage.Instance.Remove(Enums.ResourceType.Wheat, wheatCost);
}