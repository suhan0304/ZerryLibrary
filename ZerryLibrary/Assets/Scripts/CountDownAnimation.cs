using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Sirenix.OdinInspector;

public class CountDownAnimation : MonoBehaviour
{
    [SerializeField] TextMeshPro countDownText;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip countAudioClip, startAudioClip;
    [SerializeField] Color numberColor;
    [SerializeField] Color startColor;

    int currentCount;

    [Button("Start Count Down")]
    private void StartCountDown() {
        DOTween.Kill("CountDown" + GetInstanceID());
        currentCount = 3;
        UpdateCountDown();
    }

    private void UpdateCountDown() {
        if (currentCount == -1) {
            countDownText.DOFade(0, 5f);
            return;
        }
        else if (currentCount == 0) {
            countDownText.text = "Start";
            countDownText.color = startColor;
            countDownText.DOFade(1, 0);
            audioSource.PlayOneShot(startAudioClip);
        }
        else {
            countDownText.text = currentCount.ToString();
            countDownText.color = numberColor;
            countDownText.DOFade(0,1).From(1).SetEase(Ease.InOutQuad);
            countDownText.transform.DOScale(1, 1).From(1.2f).SetEase(Ease.InOutQuad);
            audioSource.PlayOneShot(countAudioClip);
        }
        currentCount--;
        DOVirtual.DelayedCall(1, UpdateCountDown).SetId("Countdown" + GetInstanceID());
    }   


}
