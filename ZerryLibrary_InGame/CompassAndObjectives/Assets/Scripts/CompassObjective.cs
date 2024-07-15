using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompassObjective : MonoBehaviour
{
    public Image ObjectiveImage;
    public bool IsCompassObjectiveActive {get; private set;}
    private RectTransform _rectTransform;
    public Transform WorldGameObject {get; private set;}

    public const float MinVisiblityRange = 5;
    public const float MaxVisiblityRange = 30;

    public CompassObjective Configure(GameObject worldGameObject, Color color, Sprite sprite = null) {
        WorldGameObject = worldGameObject.transform;
        _rectTransform = GetComponent<RectTransform>();

        ObjectiveImage.color = color;

        if (sprite != null) {
            ObjectiveImage.sprite = sprite;
        }

        ObjectiveImage.transform.localScale = Vector3.zero;

        UpdateCompassPosition();

        return this;
    }

    private void LateUpdate() => UpdateCompassPosition();

    public void UpdateCompassPosition() {
        if (WorldGameObject == null || !IsCompassObjectiveActive || CompassManager.Instance == null) {
            return;
        }

        _rectTransform.localPosition = Vector2.right *  GetObjectiveAngle(WorldGameObject) *  (CompassManager.Instance.CompassImage.rectTransform.sizeDelta.x / 2);
    }

}
