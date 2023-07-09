using System.Collections.Generic;
using Enums;
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
        }

        public void SetItemInside(Item itemInside)
        {
            _itemInside = itemInside;
            Transform itemInsideTransform = _itemInside.transform;
            itemInsideTransform.SetParent(transform);
            itemInsideTransform.localPosition = Vector2.zero;
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
            return tempItemHolder;
        }

        public void OnItemFall()
        {
            _itemInside = null;
        }

        private void OnMouseDown()
        {
            if (_itemInside)
            {
                _itemInside.OnInteract(this);
            }
        }
    }
}
