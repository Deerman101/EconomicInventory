using UnityEngine;

public class ResourcePopupSpawner : MonoBehaviour
{
    public static ResourcePopupSpawner Instance { get; private set; }

    [SerializeField] private ResourcePopupView popupPrefab;
    [SerializeField] private Canvas canvas;

    private void Awake() => Instance = this;

    public void Spawn(Vector3 worldPosition, ResourceDefinition resource, int amount)
    {
        ResourcePopupView popup = Instantiate(popupPrefab, canvas.transform);
        RectTransform rect = popup.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            worldPosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        rect.localPosition = localPoint;

        popup.Setup(resource, amount);
    }
}