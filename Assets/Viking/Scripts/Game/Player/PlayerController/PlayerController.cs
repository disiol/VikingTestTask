using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Viking.Scripts.Game.Player.Anim;

namespace Viking.Scripts.Game.Player.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player")] [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float turnSpeed = 200f;

        [SerializeField] private GameObject follow;


        private Rigidbody _rb;

        private Vector2 _movementInput;
        private Vector2 _lukInput;

        private bool _isAttacking;


        private DefaultInputActions _defaultInputActions;
        private Rigidbody _rigidbody;
        private PlayerAnimController _playerAnimController;

        private void Start()
        {
            _playerAnimController = gameObject.GetComponent<PlayerAnimController>();

            _defaultInputActions = new DefaultInputActions();
            _defaultInputActions.Player.Move.performed += OnMovement;
            _defaultInputActions.Player.Move.canceled += OnMovement;
            _defaultInputActions.Player.Look.performed += OnLook;
            _defaultInputActions.Player.Look.canceled += OnLook;
            _defaultInputActions.Player.Fire.started += OnAttack;
            _defaultInputActions.Player.Fire.canceled += OnAttack;
            _defaultInputActions.Enable();

            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private void OnDisable()
        {
            _defaultInputActions.Player.Move.performed -= OnMovement;
            _defaultInputActions.Player.Move.canceled -= OnMovement;
            _defaultInputActions.Player.Look.performed -= OnLook;
            _defaultInputActions.Player.Look.canceled -= OnLook;
            _defaultInputActions.Player.Fire.started -= OnAttack;
            _defaultInputActions.Player.Fire.canceled -= OnAttack;
            _defaultInputActions.Disable();
        }

        private void Update()
        {
            OnLook();

            _playerAnimController.Attack(_isAttacking);
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void OnLook()
        {
            // Debug.Log("PlayerController OnLook  before rotation rotation = "+ transform.rotation);
            float lukInputX = _lukInput.x;
            float lukInputY = _lukInput.y;


            Vector3 playerRotation = Vector3.up * lukInputX * turnSpeed * Time.deltaTime;
            transform.Rotate(playerRotation);

            Quaternion followTransformRotation = follow.transform.rotation;
            follow.transform.Rotate(new Vector3(followTransformRotation.x, lukInputY * turnSpeed * Time.deltaTime,
                followTransformRotation.z));

            // _playerCameraController.LukInput = _lukInput;
            // Debug.Log("PlayerController OnLook   rotation = "+ transform.rotation);
        }


        private void Movement()
        {
            Vector3 direction = new Vector3(_movementInput.x, 0, _movementInput.y).normalized;

            _rigidbody.MovePosition(transform.position +
                                    transform.TransformDirection(direction) * movementSpeed * Time.fixedDeltaTime);
            AnimRun();
        }

        private void AnimRun()
        {
            if (_movementInput.magnitude > 0)
            {
                _playerAnimController.Run();
            }
            else
            {
                _playerAnimController.Stop();
            }
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>().normalized;

            // Debug.Log("PlayerController OnMovement _movementInput = " + _movementInput);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _lukInput = context.ReadValue<Vector2>().normalized;

            // Debug.Log("PlayerController OnLook _lukInput = " + _lukInput);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _isAttacking = true;
            }
            else if (context.canceled)
            {
                _isAttacking = false;
            }
        }
    }
}