using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class LoadingUIAnimation_ScaleFade : MonoBehaviour
{
[SerializeField] float duration = 1f;
    [SerializeField] float radius = 100;
    [SerializeField] GameObject loadingElement;

    private int count;
    private float delayUnit;

    private void Start() {
        Play();
    }

    [Button("Plus Loading Element")]
    private void plusLoadingElement() {
        Instantiate(loadingElement, transform);
        Init();
    }

    [Button("Init")]
    [ContextMenu("Init")]
    private void Init() {
        count = transform.childCount == 0 ? 1 : transform.childCount;
        delayUnit = duration / count;

        int i = 0;
        foreach (Transform child in transform) {
            child.localPosition = new Vector2(Mathf.Sin(Mathf.PI * 2 * i / count) * radius, Mathf.Cos(Mathf.PI * 2 * i / count) * radius);
            child.localRotation = Quaternion.LookRotation(Vector3.forward, new Vector3(child.localPosition.x, child.localPosition.y, 0));
            i++;
        }
    }

    [Button("Play")]
    private void Play() {
        Init();
            float delay = 0;
            foreach (Transform child in transform)
            {
                child.DOScale(0, 0);
                child.DOScale(0, duration).From(1, true)
                .SetDelay(delay)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear)
                .SetLink(gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable);

                delay += delayUnit;
            }
    }
}
