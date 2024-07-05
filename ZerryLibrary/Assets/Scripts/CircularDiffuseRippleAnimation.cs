using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Numerics;

public class CircularDiffuseRippleAnimation : MonoBehaviour
{
    [SerializeField] CanvasGroup SmallGlowCG;
    [SerializeField] CanvasGroup BigGlowCG;
    void Start() {
        DOTween.Sequence().SetLoops(-1, LoopType.Restart)
            .Insert(0, SmallGlowCG.transform.DOScale(1, 0.9f).From(0, true).SetEase(Ease.Linear))
            .Insert(0, SmallGlowCG.DOFade(0, 0.9f).From(1, true).SetEase(Ease.Linear))
            .Insert(0.3f, BigGlowCG.transform.DOScale(1, 0.9f).From(0, true).SetEase(Ease.Linear))
            .Insert(0.3f, BigGlowCG.DOFade(0, 0.9f).From(1, true).SetEase(Ease.Linear));
    }
}
