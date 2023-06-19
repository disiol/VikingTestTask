using UnityEngine;

namespace Viking.Scripts.Monster
{
    using UnityEngine;

    public class MonsterGenerator : MonoBehaviour
    {
        private ObjectPool _monsterPool;
    
        [SerializeField]
        private GameObject monsterPrefab;

        [SerializeField]
        private Terrain terrain;

        [SerializeField]
        private int monsterCount = 10;

        private void Start()
        {
            if (monsterPrefab == null)
            {
                Debug.LogError("Monster prefab not assigned!");
                return;
            }

            if (terrain == null)
            {
                Debug.LogError("Terrain reference not set!");
                return;
            }

            // Create and initialize the monster pool
            _monsterPool = gameObject.AddComponent<ObjectPool>();
            _monsterPool.Initialize(monsterPrefab, monsterCount);

            GenerateMonsters();
        }

        private void GenerateMonsters()
        {
            for (int i = 0; i < monsterCount; i++)
            {
                GameObject monster = _monsterPool.GetObjectFromPool();
                if (monster != null)
                {
                    Vector3 spawnPosition = GetRandomSpawnPosition();
                    monster.transform.position = spawnPosition;
                    monster.SetActive(true);
                }
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Vector3 terrainSize = terrain.terrainData.size;
            float x = Random.Range(1f, terrainSize.x);
            float z = Random.Range(1f, terrainSize.z);
            float y = terrain.SampleHeight(new Vector3(x, 0f, z)) + terrain.GetPosition().y;

            return new Vector3(x, y, z);
        }

        private void OnDrawGizmosSelected()
        {
            if (terrain == null)
                return;

            Bounds terrainBounds = terrain.terrainData.bounds;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(terrainBounds.center, terrainBounds.size);
        }
    }
}