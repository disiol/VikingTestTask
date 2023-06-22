using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Viking.Scripts.Game.Player.Anim;
using Viking.Scripts.Tests.TestsPlaymode;

namespace Viking.Tests.TestsPlaymode.Player
{
    public class PlayerAnimControllerTests
    {
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();

        private PlayerAnimController _animController;
        private Animator _animator;

        [SetUp]
        public void SetUp()
        {
            GameObject loadPerfabVicing = Resources.Load<GameObject>("Prefabs/Vicing");

            GameObject playerObject = GameObject.Instantiate(loadPerfabVicing);
            _animController = playerObject.GetComponent<PlayerAnimController>();
            _animator = playerObject.GetComponent<Animator>();
        }

        [UnityTest]
        public IEnumerator Run_ShouldSetAnimatorParametersCorrectly()
        {
            // Act
            _animController.Run();

            yield return new WaitForSeconds(0.1f);


            // Assert
            int run1 = (int)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerAnimController), _animController,
                "Run1");
            int idle = (int)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerAnimController), _animController,
                "Idle");

            Assert.IsTrue(_animator.GetBool(run1));
            Assert.IsFalse(_animator.GetBool(idle));
        }

        [UnityTest]
        public IEnumerator Stop_ShouldSetAnimatorParametersCorrectly()
        {
            // Act
            _animController.Stop();

            yield return new WaitForSeconds(0.1f);

            // Assert
            int run1 = (int)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerAnimController), _animController,
                "Run1");
            int idle = (int)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerAnimController), _animController,
                "Idle");


            Assert.IsFalse(_animator.GetBool(run1));
            Assert.IsTrue(_animator.GetBool(idle));
        }

        [UnityTest]
        public IEnumerator Attack_ShouldSetAnimatorParameterCorrectly()
        {
            // Act
            _animController.Attack();
            yield return new WaitForSeconds(0.1f);

            // Assert
            int attack1 = (int)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerAnimController), _animController,
                "Attack1");
            Assert.IsTrue(_animator.GetBool(attack1));
        }

        [UnityTest]
        public IEnumerator Damage_ShouldSetAnimatorParameterCorrectly()
        {
            // Act
            _animController.Damage();
            yield return new WaitForSeconds(0.1f);

            // Assert
            int damage1 = (int)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerAnimController), _animController,
                "Damage1");
            Animator animator = _animController.GetComponent<Animator>();
            Assert.IsTrue(animator.GetBool(damage1));
        }

        [UnityTest]
        public IEnumerator Die_ShouldSetAnimatorParameterCorrectly()
        {
            // Act
            _animController.Die();
            yield return new WaitForSeconds(0.1f);

            // Assert
            int die1 = (int)_getAccessToPrivate.GetPrivateFieldValue(typeof(PlayerAnimController), _animController,
                "Die1");
            Assert.IsTrue(_animator.GetBool(die1));
        }
    }
}