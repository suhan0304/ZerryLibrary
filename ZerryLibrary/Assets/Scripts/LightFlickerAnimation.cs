using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Random = UnityEngine.Random;
using Sirenix.OdinInspector;

public class LightFlickerAnimation : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private AudioSource flickerSound;
    
    [Button("Light Off Flicker")]
    // Light off flicker
    void LightOff() {
        image.DOFade(0.3f, Random.Range(0.4f, 0.45f))
            .From(1)
            .SetEase(Ease.OutFlash, 6)
            .OnStart(() => flickerSound.Play())
            .OnComplete(() =>  image.DOFade(0, 0));
    }

    [Button("Light On Flicker")]
    // Light on flicker
    void LightOn() {
        image.DOFade(1f, Random.Range(0.2f, 0.4255f))
            .From(0)
            .SetEase(Ease.InFlash, 5)
            .OnStart(() => flickerSound.Play());
    }
}
