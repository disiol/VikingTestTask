using UnityEngine;
using UnityEngine.InputSystem;

namespace Viking.Scripts.Game.Player.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private float movementSpeed = 2f;
        [SerializeField] private float turnSpeed = 200f;
        
        

        private Rigidbody _rb;

        private Vector2 _movementInput;
        private Vector2 _lukInput;

        private bool _isAttacking;


        private DefaultInputActions _defaultInputActions;
        private Rigidbody _rigidbody;

        private void Start()
        {
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


            if (_isAttacking)
            {
                //TODO is atakink moctor? , anim
            }

          
        }
        private void FixedUpdate()
        {
            Movement();
        }

        private void OnLook()
        {
            // Debug.Log("PlayerController OnLook  before rotation rotation = "+ transform.rotation);
            float lukInputX = _lukInput.x;
           
           
            transform.Rotate(Vector3.up * lukInputX * turnSpeed * Time.deltaTime);
           
            // _playerCameraController.LukInput = _lukInput;
            // Debug.Log("PlayerController OnLook   rotation = "+ transform.rotation);

        }

       

        private void Movement()
        {
            //TODO  anim


            Vector3 direction = new Vector3(_movementInput.x, 0, _movementInput.y).normalized;

            _rigidbody.MovePosition(transform.position +
                                    transform.TransformDirection(direction) * movementSpeed * Time.fixedDeltaTime);
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>().normalized;

            Debug.Log("PlayerController OnMovement _movementInput = " + _movementInput);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _lukInput = context.ReadValue<Vector2>().normalized;

            Debug.Log("PlayerController OnLook _lukInput = " + _lukInput);
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
