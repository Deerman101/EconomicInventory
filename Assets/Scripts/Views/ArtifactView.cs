//using UnityEngine;
//using UnityEngine.UI;


//public class ArtifactView : MonoBehaviour
//{
//    [SerializeField] private Image icon;

//    public ArtifactDefinition Artifact { get; private set; }


//    public void Bind(ArtifactDefinition definition)
//    {
//        Artifact = definition;

//        icon.sprite = definition.Icon;
//    }


//    public void Activate()
//    {
//        gameObject.SetActive(true);
//    }
//}


using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ArtifactView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<ArtifactDefinition, Vector2> OnArtifactHover;
    public static event Action OnArtifactExit;

    [SerializeField] private Image icon;

    private ArtifactDefinition artifact;

    public void Bind(ArtifactDefinition definition)
    {
        artifact = definition;

        icon.sprite = definition.Icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (artifact == null)
            return;


        OnArtifactHover?.Invoke(
            artifact,
            Input.mousePosition
        );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnArtifactExit?.Invoke();
    }
}