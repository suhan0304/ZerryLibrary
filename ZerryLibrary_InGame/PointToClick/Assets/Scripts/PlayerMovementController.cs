using Assets.Scripts;
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

    public float _walkSpeed = 2.5f;
    public float _runSpeed = 4f;

    private MovementStates _currentMovement;
    public MovementStates CurrentMovement
    {
        get => _currentMovement;
        set
        {
            switch (value)
            {
                case MovementStates.Walk:
                    _agent.speed = 2.5f;
                    AnimationController.Instance.CurrentState = MovementStates.Walk;
                    break;
                case MovementStates.Run:
                    _agent.speed = 4f;
                    AnimationController.Instance.CurrentState = MovementStates.Run;
                    break;
                case MovementStates.None:
                    AnimationController.Instance.CurrentState = MovementStates.None;
                    break;
            }

            _currentMovement = value;
        }
    }

    private void StopNavigation() {
        _agent.SetDestination(transform.position);
        CurrentMovement = MovementStates.None;
        AnimationController.Instance.CurrentState = CurrentMovement;
    }

    void Start() {
        _inputMapping.Default.Walk.performed += Walk;
        _inputMapping.Default.Run.performed += Run;

        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
    }

    public bool IsNavigating => _agent.pathPending || _agent.remainingDistance > .25f;
    private void Update() {
        if (!_needToRotate && !IsNavigating && _currentMovement != MovementStates.None) {
            StopNavigation();
        }
        else if (_needToRotate) {
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                _lookRotation, Time.deltaTime * _rotateSpeed);

            if (Vector3.Dot(_direction, transform.forward) >= .99f) {
            _agent.SetDestination(_moveTarget);
            AnimationController.Instance.CurrentState = CurrentMovement;
                _needToRotate = false;
            }
        }
    }

    private void OnEnable() => _inputMapping.Enable();

    private void OnDisable() => _inputMapping.Disable();

    private void Run(CallbackContext context)
    {
        CurrentMovement = MovementStates.Run;
        AnimationController.Instance.CurrentState = CurrentMovement;
    }

    public ParticleSystem WalkDecal;

    private void Walk(CallbackContext context) {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 50f)) {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navPos, .25f, 1 << 0)) {
                _moveTarget = navPos.position;

                WalkDecal.transform.position = _moveTarget.WithNewY(0.1f);
                WalkDecal.Play();

                _direction = (_moveTarget.WithNewY(0) - transform.position).normalized;
                _lookRotation = Quaternion.LookRotation(_direction);
                _needToRotate = true;

                StopNavigation();

                CurrentMovement = MovementStates.Walk;

                if (IsNavigating && Vector3.Dot(_direction, transform.forward) >= 0.25f) {
                    _agent.SetDestination(_moveTarget);
                }
            }
        }
    }
}
