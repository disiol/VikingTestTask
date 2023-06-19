using UnityEngine;

namespace Viking.Scripts.Monster
{
    public class MonsterGenerator : MonoBehaviour
    {
        private ObjectPool _monsterPool;

        [SerializeField] private GameObject monsterPrefab;

        [SerializeField] private Terrain terrain;

        [SerializeField] private int monsterCount;

        [SerializeField] private float spawnRadius; // Update with the desired spawn radius

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

            _monsterPool = gameObject.AddComponent<ObjectPool>();

            _monsterPool.Initialize(monsterPrefab, monsterCount);
            GenerateMonsters();
        }

        private void GenerateMonsters()
        {
            Vector3 centralPoint = terrain.transform.position + terrain.terrainData.bounds.center;
            for (int i = 0; i < monsterCount; i++)
            {
                GameObject monster = _monsterPool.GetObjectFromPool();
                if (monster != null)
                {
                    Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
                    Vector3 spawnPosition = centralPoint + new Vector3(randomPoint.x, 0f, randomPoint.y);
                    float y = terrain.SampleHeight(spawnPosition) + terrain.transform.position.y;
                    spawnPosition.y = y;

                    monster.transform.position = spawnPosition;
                    monster.SetActive(true);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (terrain == null)
                return;

            Bounds terrainBounds = terrain.terrainData.bounds;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(terrainBounds.center, terrainBounds.size);

            Vector3 centralPoint = terrain.transform.position + terrainBounds.center;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(centralPoint, spawnRadius);
        }
    }
}