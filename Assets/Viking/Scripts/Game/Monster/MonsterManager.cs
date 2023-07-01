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
        private MonsterModel _monsterMonsterModel;
        private GameManagerView _gameManagerView;
        private Collider _collider;

        private void Start()
        {
            
            _monsterView = gameObject.GetComponent<MonsterView>();

            GameObject game = GameObject.FindWithTag("Game");
            _gameManagerView = game.GetComponent<GameManagerView>();

            _monsterMonsterModel = _monsterView.MonsterModel;

            _monsterMonsterModel.MonsterDeathEvent += MonsterIsDie;
            _monsterMonsterModel.MonsterDeathEvent += _monsterView.Presenter.OnMonsterDeath;
        }

        // Spawns the sphere of life at the specified position
        private void SpawnSphereOfLife()
        {
            Instantiate(prefabSphereOfLife, monsterPrefab.transform.position, Quaternion.identity);
        }

        private void OnDestroy()
        {
            if (_monsterMonsterModel != null)
            {
                _monsterMonsterModel.MonsterDeathEvent -= MonsterIsDie;
                _monsterMonsterModel.MonsterDeathEvent -= _monsterView.Presenter.OnMonsterDeath;
            }
        }

        // Removes a monster from the manager
        private void RemoveMonster()
        {
            //TODO poll
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            _collider = other;
   
            if (_collider.gameObject.CompareTag("PlerWepen"))
            {
           
                
                Debug.Log("MonsterCollision enter to PlerWepen ");

                _monsterView.Presenter.MonsterHasDamage();
            }
        }

        private void MonsterIsDie()
        {
            Debug.Log("MonsterCollision enter to MonsterIsDie ");

            if (_monsterMonsterModel.Lives <= 0)
            {
               

                _gameManagerView.OnMonsterKilled();
                SpawnSphereOfLife();
                RemoveMonster();
            }
        }
    }
}