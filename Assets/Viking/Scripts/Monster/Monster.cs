using UnityEngine;

namespace Viking.Scripts.Monster
{
    public class Monster:MonoBehaviour
    {
        private readonly GameObject _monsterGameObject;

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


        public Vector3 GetPosition()
        {
            return _monsterGameObject.transform.position;
        }
    }

}