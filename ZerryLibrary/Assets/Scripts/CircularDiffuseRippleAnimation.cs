using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Numerics;

public class CircularDiffuseRippleAnimation : MonoBehaviour
{
    [SerializeField] CanvasGroup smallGlowCG;
    [SerializeField] CanvasGroup bigGlowCG;
    void Start() {
        DOTween.Sequence().SetLoops(-1, LoopType.Restart)
            .Insert(0, smallGlowCG.transform.DOScale(1, 0.9f).From(0, true).SetEase(Ease.Linear))
            .Insert(0, smallGlowCG.DOFade(0, 0.9f).From(0, true).SetEase(Ease.Linear))
            .Insert(0.3f, bigGlowCG.transform.DOScale(1, 0.9f).From(0, true).SetEase(Ease.Linear))
            .Insert(0.3f, bigGlowCG.DOFade(0, 0.9f).From(1,true).SetEase(Ease.Linear));
    }
}
