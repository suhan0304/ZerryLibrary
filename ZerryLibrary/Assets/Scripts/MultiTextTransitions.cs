using DG.Tweening;
using UnityEngine;

public class MultiTextTransitions : MonoBehaviour
{
    [SerializeField] CanvasGroup[] textCGs;
    [SerializeField] int slideYAmount = 30;
    [SerializeField] float sildeDuration = 1;
    [SerializeField] float waitDelayToHide = 0;

    Sequence textSequence;

    void Awake() {
        textSequence = DOTween.Sequence().SetAutoKill(false).SetLoops(-1, LoopType.Restart);
        for (int i = 0; i < textCGs.Length; i++) {
            Transform textT = textCGs[i].transform;
            float hideDuration = sildeDuration * .5f;
            float startTime = i * (sildeDuration + waitDelayToHide + hideDuration);

            textSequence.Insert(startTime, textCGs[i].DOFade(1, sildeDuration).From(0, true).SetEase(Ease.OutQuart))
                .Join(textT.DOLocalMoveY(0, sildeDuration).From(-slideYAmount, true, true).SetEase(Ease.OutQuart))
                .AppendInterval(waitDelayToHide)
                .Append(textCGs[i].DOFade(0, hideDuration).SetEase(Ease.OutQuart))
                .Join(textT.DOLocalMoveY(slideYAmount, sildeDuration).SetRelative().SetEase(Ease.OutQuart));
        }
    }

    void OnDestroy(){
        textSequence.Kill();    
    }
}
