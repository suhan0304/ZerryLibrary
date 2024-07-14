using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovementController : MonoBehaviour
{
    private InputControls _inputMapping;

    private void Awake() => _inputMapping = new InputControls();

    void Start() {
        _inputMapping.Default.Walk.performed += Walk;
        _inputMapping.Default.Run.performed += Run;
    }

    private void OnEnable() => _inputMapping.Enable();

    private void OnDisable() => _inputMapping.Disable();

    private void Run(CallbackContext context) {
        Debug.Log("Run");
    }

    private void Walk(CallbackContext context) {
        Debug.Log("Walk");

    }
}
