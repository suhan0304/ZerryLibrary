using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class MenuSlideExpansionAnimation : MonoBehaviour
{
    [SerializeField] CanvasGroup[] menuCGs;
    [SerializeField] float slideDuration = 1;
    [SerializeField] float disactiveScale = .7f;
    [SerializeField] float disactiveAlpha = .5f;

    Vector2 initMenuSize;
    int currentMenuIndex = 0;

    private void Awake() {
        initMenuSize = menuCGs[0].GetComponent<RectTransform>().sizeDelta;
        SetCurrentMenu(currentMenuIndex, 0);
    }

    [Button("Prev Menu")]
    public void PrevMenu() {
        currentMenuIndex = Mathf.Max(0, currentMenuIndex - 1);
        SetCurrentMenu(currentMenuIndex, slideDuration);
    }

    [Button("Next Menu")]
    public void NextMenu() {
        currentMenuIndex = Mathf.Min(currentMenuIndex + 1, menuCGs.Length - 1);
        SetCurrentMenu(currentMenuIndex, slideDuration);
    }

    private void SetCurrentMenu(int index, float duration) {
        for (int i = 0; i < menuCGs.Length; i++) {
            bool isActive = i.Equals(index);
            menuCGs[i].DOKill();
            menuCGs[i].GetComponent<RectTransform>().DOKill();
            menuCGs[i].GetComponent<RectTransform>().DOSizeDelta(new Vector2(initMenuSize.x, isActive? initMenuSize.y : initMenuSize.y * disactiveScale), isActive ? duration : duration * .9f).SetEase(Ease.OutBack, .5f); menuCGs[i].DOFade(isActive ? 1 : disactiveAlpha, isActive ? duration : duration * .5f).SetEase(Ease.OutQuart);
        }
    }
}
