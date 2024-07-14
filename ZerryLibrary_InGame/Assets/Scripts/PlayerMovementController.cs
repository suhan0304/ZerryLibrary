using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovementController : MonoBehaviour
{
    private InputControls _inputMapping;

    private void Awake() => _inputMapping = new InputControls();

    private Camera _camera;
    private NavMeshAgent _agent;
    private float _rotateSpeed = 5f;
    private bool _needToRotate = false;

    private Vector3 _moveTarget = Vector3.zero;
    private Vector3 _direction = Vector3.zero;
    private Quaternion _lookRotation = Quaternion.identity;

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
