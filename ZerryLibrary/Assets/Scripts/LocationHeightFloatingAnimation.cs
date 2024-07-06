using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LocationHeightFloatingAnimation : MonoBehaviour
{
    [SerializeField] Image pinImage;
    [SerializeField] float moveY = 100;

    private void Start()
    {
        float initPosY = pinImage.transform.localPosition.y;
        pinImage.transform.DOLocalMoveY(initPosY, 1)
            .From(initPosY + moveY)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
