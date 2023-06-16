using System.Collections;
using NUnit.Framework;
using Viking.Scripts.Tolls;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Viking.Tests.TestsPlaymode
{
 

    public class MonsterGeneratorTests
    {
        // [UnityTest]
        // public IEnumerator MonsterPositionsAreGeneratedWithinTerrainBounds()
        // {
        //     GameObject terrainPrefab = Resources.Load<GameObject>("Assets/Viking/Prefabs/Mutant.prefab");
        //     GameObject monsterPrefab = Resources.Load<GameObject>("Assets/Viking/Terrain/Terrain.prefab");
        //
        //     GameObject monsterGeneratorObj = new GameObject("MonsterGenerator");
        //     MonsterGenerator monsterGenerator = monsterGeneratorObj.AddComponent<MonsterGenerator>();
        //     monsterGenerator.terrainPrefab = terrainPrefab;
        //     monsterGenerator.monsterPrefab = monsterPrefab;
        //
        //     monsterGenerator.GenerateMonsterPositions();
        //
        //     yield return null; // Wait for one frame to allow monsters to be positioned
        //
        //     foreach (MonsterGenerator.Monster monster in monsterGenerator.GetMonsters())
        //     {
        //         Vector3 monsterPosition = monster.transform.position;
        //         Terrain terrain = Object.FindObjectOfType<Terrain>();
        //
        //         Assert.IsTrue(monsterPosition.x >= 0f && monsterPosition.x <= terrain.terrainData.size.x);
        //         Assert.IsTrue(monsterPosition.z >= 0f && monsterPosition.z <= terrain.terrainData.size.z);
        //     }
        // }
    }

}