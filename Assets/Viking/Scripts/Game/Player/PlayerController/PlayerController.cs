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
            _playerControls.Player.Look.performed += OnLuk;
            _playerControls.Player.Look.canceled += OnLuk;
            _playerControls.Player.Attack.started += OnAttack;
            _playerControls.Player.Attack.canceled += OnAttack;
            _playerControls.Enable();

            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private void OnDisable()
        {
            _playerControls.Player.Move.performed -= OnMovement;
            _playerControls.Player.Move.canceled -= OnMovement;
            _playerControls.Player.Look.performed -= OnLuk;
            _playerControls.Player.Look.canceled -= OnLuk;
            _playerControls.Player.Attack.started -= OnAttack;
            _playerControls.Player.Attack.canceled -= OnAttack;
            _playerControls.Disable();
        }

        private void Update()
        {

            OnLuk();


            if (_isAttacking)
            {
                //TODO is atakink moctor? , anim
            }

          
        }
        private void FixedUpdate()
        {
            Movement();
        }

        private void OnLuk()
        {
            Debug.Log("PlayerController OnLuk  before rotation rotation = "+ transform.rotation);
            float lukInputX = _lukInput.x;
           
           
            transform.Rotate(Vector3.up * lukInputX * turnSpeed * Time.deltaTime);
           
            // _playerCameraController.LukInput = _lukInput;
            Debug.Log("PlayerController OnLuk   rotation = "+ transform.rotation);

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

        public void OnLuk(InputAction.CallbackContext context)
        {
            _lukInput = context.ReadValue<Vector2>().normalized;

            Debug.Log("PlayerController OnLuk _lukInput = " + _lukInput);
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
