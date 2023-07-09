using System.Collections.Generic;
using Gameplay;
using Managers.Base;
using UnityEngine;

namespace Managers
{
    public class PoolingManager : MonoSingleton<PoolingManager>
    {
        private List<GameObject> _pooledItems = new List<GameObject>();

        public void AddToPool(List<BoardCell> matchingBoardCells)
        {
            foreach (BoardCell boardCell in matchingBoardCells)
            {
                _pooledItems.Add(boardCell.DeactivateItemInside().gameObject); 
            }
        }

        public GameObject GetFromPool()
        {
            GameObject tempGameObjectHolder = _pooledItems[0];
            _pooledItems.RemoveAt(0);
            return tempGameObjectHolder;
        }
    }
}
