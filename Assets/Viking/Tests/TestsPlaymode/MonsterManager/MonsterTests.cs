using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using Viking.Scripts.Game.Monster.MVP;
using Viking.Scripts.Game.MonsterManager;
using Viking.Scripts.Game.MonsterManager.MVP;
using Viking.Scripts.Tests.TestsPlaymode;

namespace Viking.Tests.TestsPlaymode.MonsterManager
{
    public class MonsterTests
    {
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();

        private MonsterModel _monsterModel;
        private MonsterView _monsterView;
        private MonsterPresenter _monsterPresenter;
        private TextMeshPro _textLife;

        [SetUp]
        public void Setup()
        {
            // Instantiate the monster prefab and set up the model, view, and presenter
            GameObject monsterPrefab = Resources.Load<GameObject>("Prefabs/Mutant");
            GameObject monsterInstance = Object.Instantiate(monsterPrefab);

            _monsterView = monsterInstance.GetComponent<MonsterView>();

            _textLife = (TextMeshPro)_getAccessToPrivate.GetPrivateFieldValue(typeof(MonsterView), _monsterView,
                "livesIndicator");

            _monsterModel = _monsterView.MonsterModel;


            // _getAccessToPrivate.SetPrivateFieldValue(typeof(GameManagerView), view,
            //     "sliderLifeCharacter", slider);
            //
            // _getAccessToPrivate.SetPrivateFieldValue(typeof(GameManagerView), view,
            //     "monstersKilledText", textMeshPro);
        }

        [UnityTest]
        public IEnumerator MonsterDeath()
        {
            // Arrange
            _monsterModel = _monsterView.MonsterModel;

            int initialLives = _monsterModel.Lives;

            // Act
            _monsterView.OnMonsterDeath();
            yield return new WaitForSeconds(0.1f);


            // Assert
            var expected = initialLives + 1;
            int actual = _monsterModel.Lives;
            Assert.AreEqual(expected, actual,
                "monsterModel.Lives + 1  expected  " + expected + " byt actual " + actual);
        }
    }
}