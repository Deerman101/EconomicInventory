using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceIconView : MonoBehaviour
{
    [SerializeField] private Image image;

    public void Setup(ResourceDefinition resource) => image.sprite = resource.Icon;
}
