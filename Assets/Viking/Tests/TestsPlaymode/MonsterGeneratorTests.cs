using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Viking.Scripts.Monster;
using Viking.Scripts.Tests.TestsPlaymode;

namespace Viking.Tests.TestsPlaymode
{
    public class MonsterGeneratorTests
    {
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();


        [UnitySetUp]
        public void SetUp()
        {
            var root = new GameObject();
            // Attach a camera to our root game object.
            root.AddComponent(typeof(Camera));
            // Get a reference to the camera.
            var camera = root.GetComponent<Camera>();
            // Set the camera's background color to white.
            // Add our game object (with the camera included) to the scene by instantiating it.
            root = GameObject.Instantiate(root);
            // Load the terrainPrefab
        }

        [UnityTest]
        public IEnumerator GenerateMonsters_GeneratesExpectedNumberOfMonsters()
        {
            // Arrange
            GameObject monsterGeneratorGo = new GameObject();
            MonsterGenerator monsterGenerator = monsterGeneratorGo.AddComponent<MonsterGenerator>();

            GameObject monster = Resources.Load<GameObject>("Prefabs/Mutant");
            GameObject
                terrainPrefab =
                    Resources.Load<GameObject>("Terrain/Terrain");


            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "monsterPrefab",
                monster);
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "terrain",
                terrainPrefab.GetComponent<Terrain>());

            int monsterCount = 10;
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "monsterCount",
                monsterCount);
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "spawnRadius",
                200);
            // Act
            _getAccessToPrivate.GetPrivateMethod(typeof(MonsterGenerator), monsterGenerator, "GenerateMonsters");

            // Wait for one frame to allow the monsters to be spawned
            yield return null;

            // Assert
            int activeMonsterCount = GetActiveMonsterCount();
            int monsterGeneratorMonsterCount =
                (int)_getAccessToPrivate.GetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator,
                    "monsterCount");
            Assert.AreEqual(monsterGeneratorMonsterCount, activeMonsterCount,
                "Expected " + monsterCount + " find " + activeMonsterCount);
            Object.Destroy(monsterGeneratorGo);

        }


        [UnityTest]
        public IEnumerator GenerateMonsters_MonstersOnTerrain()
        {
            // Arrange

            // Arrange
            GameObject monsterGeneratorGo = new GameObject();
            MonsterGenerator monsterGenerator = monsterGeneratorGo.AddComponent<MonsterGenerator>();

            GameObject monster =new GameObject("Mutant");
            Rigidbody rigidbody = monster.AddComponent<Rigidbody>();
            rigidbody.freezeRotation = true;
            monster.AddComponent<CapsuleCollider>();
            monster.AddComponent<TerrainCollisionDetector>();


            GameObject
                terrainPrefab =
                    Resources.Load<GameObject>("Terrain/Terrain");

            GameObject.Instantiate(terrainPrefab);


            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "monsterPrefab",
                monster);
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "terrain",
                terrainPrefab.GetComponent<Terrain>());

            int monsterCount = 10;
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "monsterCount",
                monsterCount);
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "spawnRadius",
                200);


            // Act
            _getAccessToPrivate.GetPrivateMethod(typeof(MonsterGenerator), monsterGenerator, "GenerateMonsters");

            // Wait for one frame to allow the monsters to be spawned
            yield return new WaitForSeconds(1f);

            // Assert
            Assert.IsTrue(AreAllMonstersWithinTerrainBounds());
            
            Object.Destroy(monsterGeneratorGo);
        }

        private bool AreAllMonstersWithinTerrainBounds()
        {
            GameObject[]
                monsters = GameObject.FindGameObjectsWithTag("Monster"); // Update with the actual tag used for monsters

            foreach (GameObject monster in monsters)
            {
                bool isTerrainCollisionDetected =
                    monster.GetComponent<TerrainCollisionDetector>().isTerrainCollisionDetected;

                if (!isTerrainCollisionDetected)
                {
                    return false;
                }
            }

            return true;
        }

        private int GetActiveMonsterCount()
        {
            GameObject[]
                monsters = GameObject.FindGameObjectsWithTag("Monster"); // Update with the actual tag used for monsters
            return monsters.Length;
        }
        
        
    }
  

    public class TerrainCollisionDetector : MonoBehaviour
    {
        public bool isTerrainCollisionDetected = false;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Terrain"))
            {
                Debug.Log("Collision with terrain detected!");
                isTerrainCollisionDetected = true;
                // Perform actions or logic when collision with terrain occurs.
            }
        }
    }
}