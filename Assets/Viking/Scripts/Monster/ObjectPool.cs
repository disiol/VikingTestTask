using System.Collections;
using System.Collections.Generic;

namespace Viking.Scripts.Monster
{
    public class ObjectPool<T> 
    {
        private readonly List<T> _objectList = new();
        private int _currentIndex = 0;

        public void AddObject(T obj)
        {
            _objectList.Add(obj);
        }

        public T GetObject()
        {
            if (_currentIndex >= _objectList.Count)
                _currentIndex = 0;

            T obj = _objectList[_currentIndex];
            _currentIndex++;

            return obj;
        }


        public T[] ToArray()
        {
            return _objectList.ToArray();
        }
    }

}