using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using Viking.Scripts.Game.MonsterManager;
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

            _monsterModel = (MonsterModel)_getAccessToPrivate.GetPrivateFieldValue(typeof(MonsterView), _monsterView,
                "_monsterModel");
            

            // _getAccessToPrivate.SetPrivateFieldValue(typeof(GameManagerView), view,
            //     "sliderLifeCharacter", slider);
            //
            // _getAccessToPrivate.SetPrivateFieldValue(typeof(GameManagerView), view,
            //     "monstersKilledText", textMeshPro);




        }

        [UnityTest]
        public IEnumerator MonsterDeath_IncreasesLivesAndSpawnsSphereOfLife()
        {
            // Arrange
            int initialLives = 1;

            // Act
            _monsterView.OnMonsterDeath();
            yield return new WaitForSeconds(0.1f);

            
            // Assert
            Assert.AreEqual(initialLives + 1, _monsterModel.Lives,"monsterModel.Lives + 1");
           
            Assert.IsTrue(GameObject.Find("SphereLife"),"find SphereLife");
            
        }
    }
}