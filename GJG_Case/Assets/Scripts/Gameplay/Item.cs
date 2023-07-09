using Enums;
using Managers;
using UnityEngine;

namespace Gameplay
{
    public abstract class Item : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private ItemType _itemType;
        public ItemType ItemType { get=> _itemType; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(ItemType itemType)
        {
            _itemType = itemType;
            _spriteRenderer.sprite = SpriteManager.Instance.GetDefaultItemImage(_itemType);
        }

        public abstract void OnInteract(BoardCell boardCell);
    }
}
