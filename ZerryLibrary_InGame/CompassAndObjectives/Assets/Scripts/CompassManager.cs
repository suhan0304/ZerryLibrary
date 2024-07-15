using UnityEngine;
using UnityEngine.UI;

public class CompassManager : MonoBehaviour
{
    public static CompassManager Instance;
    public RawImage CompassImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate() => UpdateCompassHeading();

    private void UpdateCompassHeading()
    {
        if (PlayerController.Instance == null) {
            return;
        }

        Vector2 compassUvPosition = Vector2.right * (PlayerController.Instance.transform.rotation.eulerAngles.y / 360);

        CompassImage.uvRect = new Rect(compassUvPosition, Vector2.one);
    }
}