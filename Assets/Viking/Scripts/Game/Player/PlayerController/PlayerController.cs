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


        private PlayerControls _playerControls;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _playerControls = new PlayerControls();
            _playerControls.Player.Move.performed += OnMovement;
            _playerControls.Player.Move.canceled += OnMovement;
            _playerControls.Player.Look.performed += OnLook;
            _playerControls.Player.Look.canceled += OnLook;
            _playerControls.Player.Attack.started += OnAttack;
            _playerControls.Player.Attack.canceled += OnAttack;
            _playerControls.Enable();

            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private void OnDisable()
        {
            _playerControls.Player.Move.performed -= OnMovement;
            _playerControls.Player.Move.canceled -= OnMovement;
            _playerControls.Player.Look.performed -= OnLook;
            _playerControls.Player.Look.canceled -= OnLook;
            _playerControls.Player.Attack.started -= OnAttack;
            _playerControls.Player.Attack.canceled -= OnAttack;
            _playerControls.Disable();
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
