using System;
using UnityEngine;
using Viking.Scripts.Game.GameManager.View;
using Viking.Scripts.Game.Monster.MVP;

namespace Viking.Scripts.Game.Monster
{
    using System.Collections.Generic;
    using UnityEngine;

    public class MonsterManager : MonoBehaviour
    {
        public GameObject monsterPrefab; // Reference to the monster prefab
        public GameObject prefabSphereOfLife; // Reference to the PrefabSphereOfLife prefab
        public Vector3 spawnPosition; // Position to spawn the monsters

        private List<GameObject> monsters = new List<GameObject>();
        private MonsterView _monsterView;
        private GameObject _vicing;

        private void Start()
        {
            _monsterView = gameObject.GetComponent<MonsterView>();
            _vicing = GameObject.Find("Vicing");
        }

        // Spawns the sphere of life at the specified position
        private void SpawnSphereOfLife()
        {
            Instantiate(prefabSphereOfLife, monsterPrefab.transform.position, Quaternion.identity);
        }


        // Removes a monster from the manager
        private void RemoveMonster()
        {
            //TODO poll
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlerWepen"))
            {
                Debug.Log("MonsterCollision enter to PlerWepen ");

                _monsterView.Presenter.MonsterHasDamage();

                if (_monsterView.MonsterModel.Lives <= 0)
                {
                    _vicing.GetComponent<GameManagerView>().OnMonsterKilled();
                    SpawnSphereOfLife();
                    RemoveMonster();
                }
            }
        }
    }
}