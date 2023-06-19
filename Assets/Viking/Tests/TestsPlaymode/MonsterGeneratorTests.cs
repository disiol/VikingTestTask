using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.TestTools;
using Viking.Scripts.Monster;
using Viking.Scripts.Tests.TestsPlaymode;

namespace Viking.Tests.TestsPlaymode
{
    public class MonsterGeneratorTests
    {
        private readonly GetAccessToPrivate _getAccessToPrivate = new GetAccessToPrivate();

        GameObject _terrainPrefab;

        [UnitySetUp]
        public IEnumerator SetUp()
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
            ResourceRequest prefabLoadRequest = Resources.LoadAsync<GameObject>("Terrain/Terrain");
            yield return prefabLoadRequest;

            _terrainPrefab = prefabLoadRequest.asset as GameObject;
        }

        [UnityTest]
        public IEnumerator GenerateMonsters_GeneratesExpectedNumberOfMonsters()
        {
            // Arrange
            GameObject monsterGeneratorGo = new GameObject();
            MonsterGenerator monsterGenerator = monsterGeneratorGo.AddComponent<MonsterGenerator>();

            GameObject
                monsterPrefab =
                    Resources.Load<GameObject>("Prefabs/Mutant");
            GameObject
                terrainPrefab =
                    Resources.Load<GameObject>("Terrain/Terrain");


            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "monsterPrefab",
                monsterPrefab);
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "terrain",
                terrainPrefab.GetComponent<Terrain>());

            int monsterCount = 10;
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "monsterCount",
                monsterCount);
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
        }


        [UnityTest]
        public IEnumerator GenerateMonsters_MonstersOnTerrain()
        {
            // Arrange

            // Arrange
            GameObject monsterGeneratorGo = new GameObject();
            MonsterGenerator monsterGenerator = monsterGeneratorGo.AddComponent<MonsterGenerator>();

            GameObject
                monsterPrefab =
                    Resources.Load<GameObject>("Prefabs/Mutant");
            GameObject
                terrainPrefab =
                    Resources.Load<GameObject>("Terrain/Terrain");

            GameObject.Instantiate(terrainPrefab);


            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "monsterPrefab",
                monsterPrefab);
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "terrain",
                terrainPrefab.GetComponent<Terrain>());

            int monsterCount = 10;
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator, "monsterCount",
                monsterCount);


            Terrain terrain = Object.FindObjectOfType<Terrain>(); // Assuming there is a Terrain component in the scene

            // Act
            _getAccessToPrivate.GetPrivateMethod(typeof(MonsterGenerator), monsterGenerator, "GenerateMonsters");

            // Wait for one frame to allow the monsters to be spawned
            yield return null;

            // Assert
            Assert.IsTrue(AreAllMonstersWithinTerrainBounds());
        }

        private bool AreAllMonstersWithinTerrainBounds()
        {
            Terrain terrain = GameObject.FindObjectOfType<Terrain>();
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // Update with the actual tag used for monsters

            foreach (GameObject monster in monsters)
            {
                Vector3 monsterPosition = monster.transform.position;
                if (!terrain.terrainData.bounds.Contains(monsterPosition))
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
}