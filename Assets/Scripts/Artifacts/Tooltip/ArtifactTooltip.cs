using TMPro;
using UnityEngine;


public class ArtifactTooltip : MonoBehaviour
{
    [Header("Ссылки тултипа")]
    [SerializeField] private GameObject tooltipPrefab;
    [SerializeField] private Canvas canvas;

    [Header("Тесты")]
    [SerializeField] private GameObject currentTooltip;

    private RectTransform tooltipRect;

    private void Update()
    {
        if (currentTooltip == null)
            return;

        Move(Input.mousePosition);
    }

    private void Show(ArtifactDefinition artifact, Vector2 position)
    {
        if (currentTooltip != null)
            Destroy(currentTooltip);

        currentTooltip = Instantiate(tooltipPrefab, canvas.transform);
        tooltipRect = currentTooltip.GetComponent<RectTransform>();
        TooltipView view = currentTooltip.GetComponent<TooltipView>();

        view.Setup(artifact.ArtifactName, artifact.Description);
        Move(position);
    }

    private void Move(Vector2 mousePosition)
    {
        Vector2 local;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePosition,
            canvas.worldCamera,
            out local
        );

        tooltipRect.localPosition = local + new Vector2(20, -20);
    }

    private void Hide()
    {
        if (currentTooltip != null)
        {
            Destroy(currentTooltip);
            currentTooltip = null;
        }
    }

    private void OnEnable()
    {
        ArtifactView.OnArtifactHover += Show;
        ArtifactView.OnArtifactExit += Hide;
    }

    private void OnDisable()
    {
        ArtifactView.OnArtifactHover -= Show;
        ArtifactView.OnArtifactExit -= Hide;
    }
}