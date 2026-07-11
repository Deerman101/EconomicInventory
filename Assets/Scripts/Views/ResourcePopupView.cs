using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResourcePopupView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text amountText;

    [Header("Анимация")]
    [SerializeField] private float moveHeight = 80f;
    [SerializeField] private float moveDuration = 0.8f;
    [SerializeField] private float lifeTime = 0f; // Сколько будет жить после анимки. Для тестов!!!

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Setup(ResourceDefinition resource, int amount)
    {
        icon.sprite = resource.Icon;
        amountText.text = "+" + amount;

        PlayAnimation();
    }

    private void PlayAnimation()
    {
        rect.DOAnchorPosY(rect.anchoredPosition.y + moveHeight, moveDuration)
            .SetEase(Ease.OutQuad);

        CanvasGroup group = GetComponent<CanvasGroup>();

        if (group != null)
        {
            group.DOFade(0, lifeTime)
                .SetDelay(moveDuration * 0.5f);
        }

        Destroy(gameObject, lifeTime + moveDuration);
    }

    private void OnDestroy()
    {
        rect?.DOKill();
    }
}