using System.Collections.Generic;
using Enums;
using Gameplay;
using Managers.Base;
using UnityEngine;

namespace Managers
{
    public class BoardManager : MonoSingleton<BoardManager>
    {
        [SerializeField] private BoardCell boardCellPrefab;
        [SerializeField] private Transform boardTransform;
        [SerializeField] private int numberOfColumns;
        [SerializeField] private int numberOfRows;
        [SerializeField] private List<ItemType> validItems;
        
        
        private BoardCell[,] _boardCells;

        private void Start()
        {
            BuildBoard();
            AssignCellNeighbours();
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
    }
}
