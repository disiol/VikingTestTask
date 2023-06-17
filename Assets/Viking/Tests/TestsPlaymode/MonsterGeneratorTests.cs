using System.Collections;
using NUnit.Framework;
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

        [SetUp]

        public void Setup()
        {
            var root = new GameObject();
            // Attach a camera to our root game object.
            root.AddComponent(typeof(Camera));
            // Get a reference to the camera.
            var camera = root.GetComponent<Camera>();
            // Set the camera's background color to white.
            // Add our game object (with the camera included) to the scene by instantiating it.
            root = GameObject.Instantiate(root);
        }

        [UnityTest]
        public IEnumerator MonsterGenerator_PositionsMonstersWithinTerrainBounds()
        {
            // Create a test terrain prefab
            GameObject terrainPrefab = GameObject.CreatePrimitive(PrimitiveType.Plane);
            terrainPrefab.AddComponent<TerrainCollider>();
            
            // TerrainCollider terrainCollider = terrainPrefab.GetComponent<TerrainCollider>();
            // terrainCollider.terrainData.size = new Vector3(500f, 500f, 500f);
           
            terrainPrefab.transform.localScale = new Vector3(10f, 1f, 10f); // Set a specific scale for the terrain

            // Create a test monster prefab
            GameObject monsterPrefab = new GameObject("TestMonster");

            // Create a new MonsterGenerator instance
            GameObject monsterGeneratorObject = new GameObject("MonsterGenerator");
            MonsterGenerator monsterGenerator = monsterGeneratorObject.AddComponent<MonsterGenerator>();

            // Assign the test prefabs to the MonsterGenerator
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator,
                "terrainPrefab", terrainPrefab);
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator,
                "monsterPrefab", monsterPrefab);


            // Call the GenerateMonsterPositions method
            _getAccessToPrivate.GetPrivateMethod(typeof(MonsterGenerator), monsterGenerator,
                "GenerateMonsterPositions");

            yield return new WaitForSeconds(60f);

            ObjectPool<Monster> monsterPool =
                (ObjectPool<Monster>)_getAccessToPrivate.GetPrivateFieldValue(
                    typeof(MonsterGenerator), monsterGenerator,
                    "_monsterPool");
            
            Monster[] monsters = monsterPool.ToArray();

            // Check if all monsters are within the terrain bounds

            foreach ( Monster monster in monsters)
            {
                Vector3 monsterPosition = monster.GetPosition();
                Bounds terrainBounds = terrainPrefab.GetComponent<TerrainCollider>().bounds;

                Assert.IsTrue(terrainBounds.Contains(monsterPosition));
            }

            // Clean up the test objects
            Object.Destroy(monsterGeneratorObject);
            Object.Destroy(terrainPrefab);
            Object.Destroy(monsterPrefab);
        }
    }
}