using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcePanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text wheatText;
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text ironText;

    private void Start()
    {
        ResourceStorage.Instance.OnResourcesChanged += Refresh;

        Refresh();
    }

    private void OnDestroy()
    {
        if (ResourceStorage.Instance != null)
            ResourceStorage.Instance.OnResourcesChanged -= Refresh;
    }

    private void Refresh()
    {
        wheatText.text = ResourceStorage.Instance.Wheat.ToString();
        woodText.text = ResourceStorage.Instance.Wood.ToString();
        ironText.text = ResourceStorage.Instance.Iron.ToString();
    }
}
