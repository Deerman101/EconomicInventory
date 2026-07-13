using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ArtifactView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<ArtifactDefinition, Vector2> OnArtifactHover;
    public static event Action OnArtifactExit;

    [SerializeField] private Image icon;
    [SerializeField] private ArtifactDefinition artifact;

    private void Awake()
    {
        if (artifact != null && icon != null)
            icon.sprite = artifact.Icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (artifact == null)
            return;

        OnArtifactHover?.Invoke(artifact, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnArtifactExit?.Invoke();
    }
}