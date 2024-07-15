using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

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
    }

}
