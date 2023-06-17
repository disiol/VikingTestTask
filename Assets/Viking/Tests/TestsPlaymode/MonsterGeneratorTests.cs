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
            // Load the terrainPrefab
            ResourceRequest prefabLoadRequest = Resources.LoadAsync<GameObject>("Terrain/Terrain");
            yield return prefabLoadRequest;

            _terrainPrefab = prefabLoadRequest.asset as GameObject;
        }

        [UnityTest]
        public IEnumerator MonsterGenerator_PositionsMonstersWithinTerrainBounds()
        {
            // Create a test terrain terrainPrefab

          
            // Set a specific scale for the terrain

            // Create a test monster terrainPrefab
            GameObject monsterPrefab = new GameObject("TestMonster");

            // Create a new MonsterGenerator instance
            GameObject monsterGeneratorObject = new GameObject("MonsterGenerator");
            MonsterGenerator monsterGenerator = monsterGeneratorObject.AddComponent<MonsterGenerator>();

            // Assign the test prefabs to the MonsterGenerator
            GameObject.Instantiate(_terrainPrefab);

            var gameObjectTerrainClone = GameObject.Find("Terrain(Clone)");
           
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator,
                "terrainPrefab", gameObjectTerrainClone);
            _getAccessToPrivate.SetPrivateFieldValue(typeof(MonsterGenerator), monsterGenerator,
                "monsterPrefab", monsterPrefab);


            // Call the GenerateMonsterPositions method
            _getAccessToPrivate.GetPrivateMethod(typeof(MonsterGenerator), monsterGenerator,
                "GenerateMonsterPositions");

            yield return new WaitForSeconds(10f);

            ObjectPool<Monster> monsterPool =
                (ObjectPool<Monster>)_getAccessToPrivate.GetPrivateFieldValue(
                    typeof(MonsterGenerator), monsterGenerator,
                    "_monsterPool");
            
            Monster[] monsters = monsterPool.ToArray();

            // Check if all monsters are within the terrain bounds

            foreach ( Monster monster in monsters)
            {
                Vector3 monsterPosition = monster.GetPosition();
                Bounds terrainBounds = _terrainPrefab.GetComponent<TerrainCollider>().bounds;

                Assert.IsTrue(terrainBounds.Contains(monsterPosition));
            }

            // Clean up the test objects
            Object.Destroy(monsterGeneratorObject);
            Object.Destroy(_terrainPrefab);
            Object.Destroy(monsterPrefab);
        }
    }
}