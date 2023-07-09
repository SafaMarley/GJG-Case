using System.Collections.Generic;
using Enums;
using Gameplay;
using Managers.Base;

namespace Managers
{
    public class MatchManager : MonoSingleton<MatchManager>
    {
        public List<BoardCell> CheckBoardForMatchingClusters(BoardCell tappedCell)
        {
            List<BoardCell> matchingCells = new List<BoardCell>();

            CheckBoardForMatchingClusters(tappedCell, tappedCell.ItemInside.GetItemType, matchingCells);
            
            return matchingCells;
        }

        private void CheckBoardForMatchingClusters(BoardCell neighbourCell, ItemType itemType, List<BoardCell> matchingCells)
        {
            if (matchingCells.Contains(neighbourCell))
            {
                return;
            }
            matchingCells.Add(neighbourCell);
            
            foreach (BoardCell boardCell in neighbourCell.Neighbours.Values)
            {
                if (boardCell.ItemInside && itemType == boardCell.ItemInside.GetItemType)
                {
                    CheckBoardForMatchingClusters(boardCell, itemType, matchingCells);
                }
            }
        }
    }
}
