using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResourceIconView : MonoBehaviour
{
    [SerializeField] private Image image;

    private Tween rotationTween;

    public void Setup(ResourceDefinition resource) => image.sprite = resource.Icon;

    public void PlayProductionAnimation(float productionTime)
    {
        StopAnimation();

        float speed = productionTime;

        //if (GameSpeedController.Instance != null)
        //    speed /= GameSpeedController.Instance.CurrentSpeed;

        rotationTween = transform.DORotate(
                new Vector3(0, 0, -360),
                speed,
                RotateMode.FastBeyond360
            )
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

    public void StopAnimation()
    {
        if (rotationTween != null)
        {
            rotationTween.Kill(true);
            rotationTween = null;
        }

        transform.DOKill();

        transform.rotation = Quaternion.identity;
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
