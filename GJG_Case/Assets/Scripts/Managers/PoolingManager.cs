using System.Collections.Generic;
using Gameplay;
using Managers.Base;

namespace Managers
{
    public class PoolingManager : MonoSingleton<PoolingManager>
    {
        private List<Item> _pooledItems = new List<Item>();

        public void AddToPool(List<BoardCell> matchingBoardCells)
        {
            HashSet<int> affectedColumnIndices = new HashSet<int>();
            
            foreach (BoardCell boardCell in matchingBoardCells)
            {
                affectedColumnIndices.Add(boardCell.coordinateX);
                _pooledItems.Add(boardCell.DeactivateItemInside()); 
            }
            
            BoardManager.Instance.SettleBoard(affectedColumnIndices);
        }

        public Item GetFromPool()
        {
            Item tempItemHolder = _pooledItems[0];
            _pooledItems.RemoveAt(0);
            tempItemHolder.gameObject.SetActive(true);
            return tempItemHolder;
        }
    }
}
