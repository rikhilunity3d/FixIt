using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// This animation is for wood piece of bridge
public class UpThenScaleAnim : MonoBehaviour
{
    [SerializeField]
    Vector3 addValueForMove;
    [SerializeField]
    float durationForMove;
    

    [SerializeField]
    Vector3 addValueForScale;
    [SerializeField]
    float durationforScale;
    private void OnEnable()
    {

        Vector3 originalPosition = transform.position;
        Vector3 originalScale = transform.localScale;

        Sequence sequence = DOTween.Sequence();

        addValueForMove.x = originalPosition.x + addValueForMove.x;
        addValueForMove.y = originalPosition.y + addValueForMove.y;
        addValueForMove.z = originalPosition.z + addValueForMove.z;

        addValueForScale.x = originalScale.x+addValueForScale.x;
        addValueForScale.y = originalScale.y + addValueForScale.y;
        addValueForScale.z = originalScale.z + addValueForScale.z;

        sequence
            .Join(this.transform.DOMove(addValueForMove, durationForMove).SetEase(Ease.OutQuad))
            .Join(this.transform.DOScale(addValueForScale, durationforScale).SetEase(Ease.OutQuad))
            .Append(this.transform.DOMove(originalPosition, 1).SetEase(Ease.OutQuad))
            .Join(this.transform.DOScale(originalScale, 1).SetEase(Ease.OutQuad));
    }
}
