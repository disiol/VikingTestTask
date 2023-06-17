using System.Collections.Generic;
using UnityEngine;

namespace Viking.Scripts.Tolls
{
    public class MonsterGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject terrainPrefab; // Assign the terrain prefab in the Unity Inspector

        [SerializeField] private GameObject monsterPrefab; // Assign the monster prefab in the Unity Inspector
        [SerializeField] private float monsterYPosition;

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
                float x = Random.Range(0f, length);
                float z = Random.Range(0f, width);
                float y = monsterYPosition; // Set a fixed value for the Y position

                monster.SetPosition(new Vector3(x, y, z));
            }
        }

        // Inner class representing a monster object
        private class Monster
        {
            private GameObject _monsterGameObject;

            public Monster(GameObject monsterPrefab)
            {
                _monsterGameObject = Instantiate(monsterPrefab);
                _monsterGameObject.SetActive(false);
            }

            public void SetActive(bool active)
            {
                _monsterGameObject.SetActive(active);
            }

            public void SetPosition(Vector3 position)
            {
                _monsterGameObject.transform.position = position;
            }
        }

        // Generic ObjectPool class
        private class ObjectPool<T>
        {
            private List<T> objectList = new List<T>();
            private int currentIndex = 0;

            public void AddObject(T obj)
            {
                objectList.Add(obj);
            }

            public T GetObject()
            {
                if (currentIndex >= objectList.Count)
                    currentIndex = 0;

                T obj = objectList[currentIndex];
                currentIndex++;

                return obj;
            }
        }
    }
}