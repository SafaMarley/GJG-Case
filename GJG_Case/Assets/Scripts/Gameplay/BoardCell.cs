using System.Collections.Generic;
using Enums;
using Managers;
using UnityEngine;

namespace Gameplay
{
    public enum NeighbourCellDirection
    {
        Right,
        Left,
        Up,
        Down
    }

    public class BoardCell : MonoBehaviour
    {
        public bool isAvailable;
        public int coordinateX;
        public int coordinateY;

        private Dictionary<NeighbourCellDirection, BoardCell> _neighbours = new Dictionary<NeighbourCellDirection, BoardCell>();
        public Dictionary<NeighbourCellDirection, BoardCell> Neighbours => _neighbours;

        private Item _itemInside;
        public Item ItemInside { get => _itemInside; }
    
        [SerializeField] private Item itemPrefab;

        public void Initialize(int x, int y, ItemType itemType)
        {
            coordinateX = x;
            coordinateY = y;
            gameObject.name = "boardCell: " + x + " : " + y;
            _itemInside = Instantiate(itemPrefab, transform);
            _itemInside.Initialize(itemType);
            isAvailable = false;
        }

        public void SetItemInside(Item itemInside)
        {
            _itemInside = itemInside;
            Transform itemInsideTransform = _itemInside.transform;
            itemInsideTransform.SetParent(transform);
        }

        public void AssignNeighbourCells(NeighbourCellDirection neighbourCellDirection, BoardCell boardCell)
        {
            _neighbours.Add(neighbourCellDirection, boardCell);
        }

        public Item DeactivateItemInside()
        {
            Item tempItemHolder = _itemInside;
            _itemInside.transform.parent = null;
            _itemInside.gameObject.SetActive(false);
            _itemInside = null;
            isAvailable = true;
            return tempItemHolder;
        }

        private void OnMouseDown()
        {
            MatchManager.Instance.CheckBoardForMatchingCluster(this);
        }
    }
}
