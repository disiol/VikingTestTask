using UnityEngine;
using UnityEngine.Serialization;

namespace Viking.Scripts.Monster
{
    public class MonsterGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject terrainPrefab; // Assign the terrain prefab in the Unity Inspector

        [SerializeField] private GameObject monsterPrefab; // Assign the monster prefab in the Unity Inspector

        [SerializeField] private float monsterYPosition;
        [SerializeField] private float minXPosition;
        [SerializeField] private float minZPosition;


        private readonly ObjectPool<Monster> _monsterPool = new();

        private void Start()
        {
            GenerateMonsterPositions();
        }

        private void GenerateMonsterPositions()
        {
            GameObject instantiatedTerrain = Instantiate(terrainPrefab, transform.position, transform.rotation);
            TerrainCollider terrainCollider = instantiatedTerrain.GetComponent<TerrainCollider>();

            if (terrainCollider == null)
            {
                Debug.LogError("TerrainCollider component not found!");
                Destroy(instantiatedTerrain);
                return;
            }

            Vector3 terrainSize = terrainCollider.bounds.size;
            float length = terrainSize.x;
            float width = terrainSize.z;

            Destroy(instantiatedTerrain);

            int numberOfMonsters = 10;
            float perimeter = 2 * (length + width);
            float segmentLength = perimeter / numberOfMonsters;

            // Create monster objects and add them to the object pool
            for (int i = 0; i < numberOfMonsters; i++)
            {
                Monster monster = new Monster(monsterPrefab);
                _monsterPool.AddObject(monster);
            }

            // Activate and position monsters from the object pool
            for (int i = 0; i < numberOfMonsters; i++)
            {
                Monster monster = _monsterPool.GetObject();
                monster.SetActive(true);

                // Generate random positions for x and z coordinates within the terrain's bounds
                float x = Random.Range(minXPosition, length);
                float z = Random.Range(minZPosition, width);
                float y = monsterYPosition; // Set a fixed value for the Y position

                monster.SetPosition(new Vector3(x, y, z));
            }
        }
    }
}