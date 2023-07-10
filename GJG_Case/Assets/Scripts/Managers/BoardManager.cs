using System.Collections.Generic;
using Enums;
using Gameplay;
using Managers.Base;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class BoardManager : MonoSingleton<BoardManager>
    {
        [Header("Parameters")]
        [SerializeField] private int numberOfColumns;
        [SerializeField] private int numberOfRows;
        [SerializeField] private List<ItemType> validItems;
        [SerializeField] private int firstThreshold;
        [SerializeField] private int secondThreshold;
        [SerializeField] private int thirdThreshold;

        
        [Header("References")]
        [SerializeField] private BoardCell boardCellPrefab;
        [SerializeField] private Transform boardTransform;

        private BoardCell[,] _boardCells;

        private void Start()
        {
            BuildBoard();
            AssignCellNeighbours();
            HighlightBoard();
        }

        private void BuildBoard()
        {
            _boardCells = new BoardCell[numberOfColumns, numberOfRows];
            Vector2 boardCellSizeOffset = boardCellPrefab.GetComponent<BoxCollider2D>().size;
            float gridOffsetX = numberOfColumns * boardCellSizeOffset.x / 2f - boardCellSizeOffset.x / 2f;
            float gridOffsetY = numberOfRows * boardCellSizeOffset.y / 2f - boardCellSizeOffset.y / 2f;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    _boardCells[j, i] = Instantiate(boardCellPrefab, new Vector3(j * boardCellSizeOffset.x - gridOffsetX, i * boardCellSizeOffset.y - gridOffsetY), Quaternion.identity, boardTransform);
                    _boardCells[j, i].Initialize(j, i, validItems[Random.Range(0, validItems.Count)]);
                }
            }
        }

        private void AssignCellNeighbours()
        {
            foreach (BoardCell boardCell in _boardCells)
            {
                if (boardCell.coordinateX != numberOfColumns - 1)
                {
                    boardCell.AssignNeighbourCells(NeighbourCellDirection.Right, _boardCells[boardCell.coordinateX + 1, boardCell.coordinateY]);
                }
                if (boardCell.coordinateX != 0)
                {
                    boardCell.AssignNeighbourCells(NeighbourCellDirection.Left, _boardCells[boardCell.coordinateX - 1, boardCell.coordinateY]);
                }
                if (boardCell.coordinateY != numberOfColumns - 1)
                {
                    boardCell.AssignNeighbourCells(NeighbourCellDirection.Up, _boardCells[boardCell.coordinateX, boardCell.coordinateY + 1]);
                }
                if (boardCell.coordinateY != 0)
                {
                    boardCell.AssignNeighbourCells(NeighbourCellDirection.Down, _boardCells[boardCell.coordinateX, boardCell.coordinateY - 1]);
                }
            }
        }

        public void SettleBoard(HashSet<int> affectedColumnIndices)
        {
            foreach (int columnIndex in affectedColumnIndices)
            {
                int dropDistance = 0;
                for (int i = 0; i < numberOfRows; i++)
                {
                    BoardCell cell = _boardCells[columnIndex, i];
                    if (!_boardCells[columnIndex, i].ItemInside)
                    {
                        ++dropDistance;
                    }
                    else
                    {
                        if (dropDistance > 0)
                        {
                            _boardCells[columnIndex, i - dropDistance].SetItemInside(cell.ItemInside);
                            cell.OnItemFall();
                        }
                    }
                }

                for (int i = 1; i <= dropDistance; i++)
                {
                    _boardCells[columnIndex, numberOfRows - i].SpawnItemInside(PoolingManager.Instance.GetFromPool(), (ItemType) Random.Range(0, validItems.Count));
                }
            }
            
            HighlightBoard();
        }

        private void HighlightBoard()
        {
            bool isDeadLock = true;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    BoardCell currentCell = _boardCells[j, i];
                    if (!currentCell.isVisited)
                    {
                        List<BoardCell> cellsInCluster = MatchManager.Instance.CheckBoardForMatchingClusters(currentCell);
                        
                        if (cellsInCluster.Count == 1)
                        {
                            cellsInCluster[0].isVisited = false;
                            cellsInCluster[0].ItemInside.Upgrade(0);
                            continue;
                        }
                        
                        isDeadLock = false;
                        if (cellsInCluster.Count > thirdThreshold)
                        {
                            foreach (BoardCell cell in cellsInCluster)
                            {
                                cell.isVisited = true;
                                cell.ItemInside.Upgrade(3);
                            }
                        }
                        else if (cellsInCluster.Count > secondThreshold)
                        {
                            foreach (BoardCell cell in cellsInCluster)
                            {
                                cell.isVisited = true;
                                cell.ItemInside.Upgrade(2);
                            }
                        }
                        else if (cellsInCluster.Count > firstThreshold)
                        {
                            foreach (BoardCell cell in cellsInCluster)
                            {
                                cell.isVisited = true;
                                cell.ItemInside.Upgrade(1);
                            }
                        }
                        else
                        {
                            foreach (BoardCell cell in cellsInCluster)
                            {
                                cell.isVisited = true;
                                cell.ItemInside.Upgrade(0);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    _boardCells[j, i].isVisited = false;
                }
            }

            if (isDeadLock)
            {
                ShuffleBoard();
            }
        }

        private void ShuffleBoard()
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = numberOfColumns - 1; j > 0; j--)
                {
                    Item tempItemHolder = _boardCells[i, j].ItemInside;
                    BoardCell randomCell = _boardCells[Random.Range(0, numberOfRows), Random.Range(0, j)];
                    _boardCells[i, j].SetItemInside(randomCell.ItemInside);
                    randomCell.SetItemInside(tempItemHolder);
                }
            }
            
            BoardCell randomCenterCell = _boardCells[Random.Range(0, numberOfRows), Random.Range(0, numberOfColumns)];

            foreach (BoardCell neighbourCell in randomCenterCell.Neighbours.Values)
            {
                neighbourCell.ItemInside.Initialize(randomCenterCell.ItemInside.GetItemType);
            }
            
            HighlightBoard();
        }
    }
}
