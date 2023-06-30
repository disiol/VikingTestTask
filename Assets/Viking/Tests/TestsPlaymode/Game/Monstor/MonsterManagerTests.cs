using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using Viking.Scripts.Game.GameManager.View;
using Viking.Scripts.Game.Monster.MVP;
using Viking.Scripts.Tests.TestsPlaymode;

namespace Viking.Tests.TestsPlaymode.Game.Monstor
{
    public class MonsterManagerTests
    {
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();
        private TextMeshPro _textLife;
        private MonsterModel _monsterModel;
        private MonsterView _monsterView;

        [UnityTest]
        public IEnumerator OnTriggerEnter_DamagesMonster_WhenCollidingWithPlerWepen()
        {
            // Arrange
            GameObject monsterPrefab = Resources.Load<GameObject>("Prefabs/Mutant");
            GameObject monsterInstance = Object.Instantiate(monsterPrefab);
            Rigidbody rigidbodyMutant = monsterInstance.GetComponent<Rigidbody>();
            rigidbodyMutant.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            monsterInstance.transform.position = new Vector3(0, 0, 0);

            _monsterView = monsterInstance.GetComponent<MonsterView>();

            _monsterModel = _monsterView.MonsterModel;
            Scripts.Game.Monster.MonsterManager monsterManager =
                monsterInstance.GetComponent<Scripts.Game.Monster.MonsterManager>();

            GameObject gamePrefab = Resources.Load<GameObject>("Prefabs/Game");

            GameObject plerWepen = new GameObject();
            plerWepen.AddComponent<BoxCollider>().isTrigger = true;
            plerWepen.transform.position = new Vector3(10000, 10000, 10000);
            plerWepen.tag = "PlerWepen";

            GameObject gameInstance = Object.Instantiate(gamePrefab);
            ;

            GameManagerView gameManagerView = gameInstance.GetComponent<GameManagerView>();

            MonsterView monsterView = monsterInstance.GetComponent<MonsterView>();

            MonsterModel monsterModel = monsterView.MonsterModel;
            // Act
            plerWepen.transform.position = new Vector3(0, 0, 0);
            monsterInstance.transform.position = new Vector3(0, 0, 0);

            yield return new WaitForSeconds(0.1f);

            // Assert
            int expected = 1;


            TextMeshProUGUI monstersKilledText = (TextMeshProUGUI)_getAccessToPrivate.GetPrivateFieldValue(
                typeof(GameManagerView), gameManagerView,
                "monstersKilledText");

            Assert.AreEqual(expected, monstersKilledText.text);

            expected = 0;

            int actual = monsterModel.Lives;
            Assert.AreEqual(expected, actual);

            // Clean up
            Object.DestroyImmediate(plerWepen);
            Object.DestroyImmediate(gameInstance);
            Object.DestroyImmediate(monsterInstance);
        }
    }
}