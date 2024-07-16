using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapController : MonoBehaviour
{
    private VisualElement _root;
    private bool IsMapOpen => _root.ClassListContains("root-container-full");

    void Start() {
        _root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
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
