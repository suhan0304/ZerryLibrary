using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapController : MonoBehaviour
{
    private VisualElement _root;
    private bool IsMapOpen => _root.ClassListContains("root-container-full");

    public GameObject Player;
    [Range(1, 15)]
    public float miniMultiplyer = 5.3f;
    [Range(1, 15)]
    public float fullMultiplyer = 7f;
    private VisualElement _playerRepresentation;

    void Start() {
        _root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
        _playerRepresentation = _root.Q<VisualElement>("Player");
    }

    private void LateUpdate()
    {
        var multiplyer = IsMapOpen ? fullMultiplyer : miniMultiplyer;
        _playerRepresentation.style.translate = 
            new Translate(Player.transform.position.x * multiplyer, Player.transform.position.z * -multiplyer, 0);
        _playerRepresentation.style.rotate = 
            new Rotate(new Angle(Player.transform.rotation.eulerAngles.y));
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            ToggleMap(!IsMapOpen);
        }
    }

    private void ToggleMap(bool on) {
        _root.EnableInClassList("root-container-mini", !on);
        _root.EnableInClassList("root-container-full", on);
    }
}
