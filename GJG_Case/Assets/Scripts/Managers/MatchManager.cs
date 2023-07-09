using System.Collections.Generic;
using Enums;
using Gameplay;
using Managers.Base;

namespace Managers
{
    public class MatchManager : MonoSingleton<MatchManager>
    {
        public void CheckBoardForMatchingCluster(BoardCell tappedCell)
        {
            List<BoardCell> matchingCells = new List<BoardCell>();

            CheckBoardForMatchingCluster(tappedCell, tappedCell.ItemInside.ItemType, matchingCells);
            
            PoolingManager.Instance.AddToPool(matchingCells);
        }

        private void CheckBoardForMatchingCluster(BoardCell neighbourCell, ItemType itemType, List<BoardCell> matchingCells)
        {
            if (matchingCells.Contains(neighbourCell))
            {
                return;
            }
            matchingCells.Add(neighbourCell);
            
            foreach (BoardCell boardCell in neighbourCell.Neighbours.Values)
            {
                if (boardCell.ItemInside && itemType == boardCell.ItemInside.ItemType)
                {
                    CheckBoardForMatchingCluster(boardCell, itemType, matchingCells);
                }
            }
        }
    }
}
