using Enums;
using Managers;
using UnityEngine;

namespace Gameplay
{
    public abstract class Item : MonoBehaviour
    {
        protected SpriteRenderer SpriteRenderer;
        protected ItemType ItemType;
        public ItemType GetItemType { get=> ItemType; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(ItemType itemType)
        {
            ItemType = itemType;
            SpriteRenderer.sprite = SpriteManager.Instance.GetDefaultItemImage(ItemType);
        }

        public abstract void OnInteract(BoardCell boardCell);
        
        public abstract void Upgrade(int level);
    }
}
