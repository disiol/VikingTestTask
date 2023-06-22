using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.TestTools;
using Viking.Scripts.Game.Player.PlayerController;
using Viking.Scripts.Tests.TestsPlaymode;

namespace Viking.Tests.TestsPlaymode
{
    public class PlayerControllerTests : InputTestFixture
    {
        private GameObject _playerObject;
        private PlayerController _playerController;
        private DefaultInputActions _defaultInputActions;
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();
        private Rigidbody _rigidbody;
        
        private Keyboard _keyboard;
        private Mouse _mouse;

        [SetUp]
        public override void Setup()
        {
            GameObject loadPerfabVicing = Resources.Load<GameObject>("Prefabs/Vicing");


            _playerObject = GameObject.Instantiate(loadPerfabVicing);
            _rigidbody = _playerObject.GetComponent<Rigidbody>();
            _playerController = _playerObject.GetComponent<PlayerController>();
            
            
            var root = new GameObject();
            // Attach a camera to our root game object.
            root.AddComponent(typeof(Camera));
            // Get a reference to the camera.
            var camera = root.GetComponent<Camera>();
            // Set the camera's background color to white.
            // Add our game object (with the camera included) to the scene by instantiating it.
            root = GameObject.Instantiate(root);

            // _rigidbody = _playerObject.AddComponent<Rigidbody>();
            // _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            // _playerController = _playerObject.AddComponent<PlayerController>();
            _defaultInputActions = new DefaultInputActions();
           
            _keyboard = InputSystem.AddDevice<Keyboard>();
            _mouse = InputSystem.AddDevice<Mouse>();
            
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(_playerObject);
            _defaultInputActions.Disable();
        }
        
        
        

        [UnityTest]
        public IEnumerator Player_Forward()
        {
            // Arrange
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            _defaultInputActions.Player.Move.performed += _playerController.OnMovement;
            _defaultInputActions.Player.Move.canceled += _playerController.OnMovement;
            _defaultInputActions.Player.Move.Enable();
            _defaultInputActions.Enable();

            


            Vector3 initialPosition = _playerObject.transform.position;
            
            // Act
            Press(_keyboard.upArrowKey);
            yield return new WaitForSeconds(1f);


            // Assert

            Vector3 finalPosition = _playerObject.transform.position;

            Assert.Greater(finalPosition.z, initialPosition.z); // Check if the object has moved forward
        }

        [UnityTest]
        public IEnumerator Player_Back()
        {
            // Arrange
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            _defaultInputActions.Player.Move.performed += _playerController.OnMovement;
            _defaultInputActions.Player.Move.canceled += _playerController.OnMovement;
            _defaultInputActions.Player.Move.Enable();
            _defaultInputActions.Enable();


            float playerControllerMovementSpeed =
                (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "movementSpeed");
            var position = _playerObject.transform.position;

            Vector3 direction = new Vector3(0, 0, -1.0f);


            Vector3 expectedPosition = position +
                                       _playerObject.transform.TransformDirection(direction) *
                                       playerControllerMovementSpeed * Time.fixedTime;
            Vector3 expectedPositionNormalized = expectedPosition.normalized;


           

            // Act
            Press(_keyboard.sKey);

            yield return new WaitForSeconds(0.1f);


            var newPosition = _playerObject.transform.position;

            // Assert
            Assert.AreEqual(expectedPositionNormalized, newPosition.normalized,
                "Player_Left. Mover object moved from " + position + " to " + expectedPositionNormalized);
        }

        [UnityTest]
        public IEnumerator Player_Right()
        {
            // Arrange
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            _defaultInputActions.Player.Move.performed += _playerController.OnMovement;
            _defaultInputActions.Player.Move.canceled += _playerController.OnMovement;
            _defaultInputActions.Player.Move.Enable();
            _defaultInputActions.Enable();


            float playerControllerMovementSpeed =
                (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "movementSpeed");
            var position = _playerObject.transform.position;

            Vector3 direction = new Vector3(1.0f, 0, 0.0f);


            Vector3 expectedPosition = position +
                                       _playerObject.transform.TransformDirection(direction) *
                                       playerControllerMovementSpeed * Time.fixedTime;
            Vector3 expectedPositionNormalized = expectedPosition.normalized;


           

            // Act
            Press(_keyboard.dKey);


            yield return new WaitForSeconds(0.1f);


            var newPosition = _playerObject.transform.position;

            // Assert
            Assert.AreEqual(expectedPositionNormalized, newPosition.normalized,
                "Player_Left. Mover object moved from " + position + " to " + expectedPositionNormalized);
        }

        [UnityTest]
        public IEnumerator Player_Left()
        {
            // Arrange
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            _defaultInputActions.Player.Move.performed += _playerController.OnMovement;
            _defaultInputActions.Player.Move.canceled += _playerController.OnMovement;
            _defaultInputActions.Player.Move.Enable();
            _defaultInputActions.Enable();


            float playerControllerMovementSpeed =
                (float)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "movementSpeed");
            var position = _playerObject.transform.position;

            Vector3 direction = new Vector3(-1.0f, 0, 0.0f);


            Vector3 expectedPosition = position +
                                       _playerObject.transform.TransformDirection(direction) *
                                       playerControllerMovementSpeed * Time.fixedTime;
            Vector3 expectedPositionNormalized = expectedPosition.normalized;


           

            // Act
            Press(_keyboard.aKey);

            yield return new WaitForSeconds(0.5f);


            var newPosition = _playerObject.transform.position;

            // Assert
            Assert.AreEqual(expectedPositionNormalized, newPosition.normalized,
                "Player_Left. Mover object moved from " + position + " to " + expectedPositionNormalized);
        }

        [UnityTest]
        public IEnumerator RotatesPlayerRight()
        {
            // Arrange
            _rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;


            _defaultInputActions.Player.Look.performed += _playerController.OnLook;
            _defaultInputActions.Player.Look.canceled += _playerController.OnLook;
            _defaultInputActions.Player.Look.Enable();
            _defaultInputActions.Enable();

           

            Quaternion initialRotation = _playerObject.transform.rotation;

            var LookInputX = 1.0f;


            // Act
            Press(_keyboard.dKey);


            yield return new WaitForSeconds(0.5f);

            // Assert


            Quaternion finalRotation = _playerObject.transform.rotation;

            Assert.Greater(finalRotation.normalized.y, initialRotation.normalized.y, "RotatesPlayerRight");
        }

        [UnityTest]
        public IEnumerator RotatesPlayerLeft()
        {
            // Arrange
            _rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;


            _defaultInputActions.Player.Look.performed += _playerController.OnLook;
            _defaultInputActions.Player.Look.canceled += _playerController.OnLook;
            _defaultInputActions.Player.Look.Enable();
            _defaultInputActions.Enable();

           

            Quaternion initialRotation = _playerObject.transform.rotation;

            var LookInputX = -1.0f;


            // Act
            Set(_mouse.position, new Vector2(LookInputX, 0.0f));

            yield return new WaitForSeconds(0.1f);

            // Assert


            Quaternion finalRotation = _playerObject.transform.rotation;

            Assert.Less(finalRotation.normalized.y, initialRotation.normalized.y, "RotatesPlayerLeft");
        }

        [UnityTest]
        public IEnumerator Fire_Started_SetsIsAttackingToTrue()
        {
            _defaultInputActions.Enable();

            _defaultInputActions.Player.Fire.started += _playerController.OnAttack;
            _defaultInputActions.Player.Fire.Enable();
            _defaultInputActions.Player.Fire.ReadValue<float>();

            yield return null;

            bool isAttacking =
                (bool)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "_isAttacking");
            Assert.IsFalse(isAttacking);
        }

        [UnityTest]
        public IEnumerator Attack_Canceled_SetsIsAttackingToFalse()
        {
            _defaultInputActions.Enable();

            _defaultInputActions.Player.Fire.canceled += _playerController.OnAttack;
            _defaultInputActions.Player.Fire.Enable();
            _defaultInputActions.Player.Fire.ReadValue<float>();

            yield return null;

            bool isAttacking =
                (bool)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "_isAttacking");
            Assert.IsFalse(isAttacking);
        }

        [UnityTest]
        public IEnumerator IsAttacking_DefaultValue_IsFalse()
        {
            yield return null;

            bool isAttacking =
                (bool)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerController), _playerController,
                    "_isAttacking");
            Assert.IsFalse(isAttacking);
        }
    }
    
}