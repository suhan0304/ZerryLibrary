using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
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

        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (_needToRotate) {
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * _rotateSpeed);

            if (Vector3.Dot(_direction, transform.forward) >= .97) {
                _needToRotate = false;
            }
        }
    }

    private void OnEnable() => _inputMapping.Enable();

    private void OnDisable() => _inputMapping.Disable();

    private void Run(CallbackContext context) {
        Debug.Log("Run");
    }

    private void Walk(CallbackContext context) {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 50f)) {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navPos, .25f, 1 << 0)) {
                _moveTarget = navPos.position;
                _direction = (_moveTarget.WithNewY(0) - transform.position).normalized;
                _lookRotation = Quaternion.LookRotation(_direction);
                _needToRotate = true;
            }
        }

    }
}
