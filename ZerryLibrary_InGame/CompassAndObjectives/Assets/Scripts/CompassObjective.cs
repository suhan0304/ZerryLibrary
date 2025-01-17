using UnityEngine;
using UnityEngine.UI;

public class CompassObjective : MonoBehaviour
{
    public Image ObjectiveImage;
    public bool IsCompassObjectiveActive {get; private set;}
    private RectTransform _rectTransform;
    public Transform WorldGameObject {get; private set;}

    public const float MinVisibilityRange = 5;
    public const float MaxVisibilityRange = 30;

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
        if (WorldGameObject == null) {
            if(ObjectiveImage.transform.localScale.magnitude <= .1f) {
                CompassManager.Instance?.RemoveCompassObjective(this);
            }
            return;
        }
        
        if (!IsCompassObjectiveActive || CompassManager.Instance == null) {
            return;
        }

        _rectTransform.localPosition = Vector2.right *  GetObjectiveAngle(WorldGameObject) *  (CompassManager.Instance.CompassImage.rectTransform.sizeDelta.x / 2);
    }

    private void Update() => ObjectiveImage.transform.localScale = Vector3.Lerp(ObjectiveImage.transform.localScale, IsCompassObjectiveActive && WorldGameObject != null ? Vector3.one : Vector3.zero, Time.deltaTime * 8);

    public static float GetObjectiveAngle(Transform worldObjectiveTransform) => 
        PlayerController.Instance == null ? -1 :             
            Vector3.SignedAngle(PlayerController.Instance.transform.forward, 
            GetObjectiveDirection(worldObjectiveTransform, 
            PlayerController.Instance.transform), Vector3.up) / 180;

    private static Vector3 GetObjectiveDirection(Transform objectiveTransform, 
        Transform sourceTransform) => (new Vector3(objectiveTransform.position.x,     
            sourceTransform.position.y, objectiveTransform.position.z) - 
            sourceTransform.position).normalized;

    public void UpdateUiIndex(int newIndex)
    {
        _rectTransform.SetSiblingIndex(newIndex);
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        if(PlayerController.Instance == null) 
        { return; }

        float currentDistance = Vector3.Distance(WorldGameObject.position, 
            PlayerController.Instance.transform.position);

        IsCompassObjectiveActive = currentDistance < MaxVisibilityRange && 
            currentDistance > MinVisibilityRange;
    }
}
