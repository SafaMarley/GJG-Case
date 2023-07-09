using Enums;
using Managers;
using UnityEngine;

namespace Gameplay
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private ItemType _itemType;
        public ItemType ItemType { get=> _itemType; }

        public void Initialize(ItemType itemType)
        {
            _itemType = itemType;
            spriteRenderer.sprite = SpriteManager.Instance.GetDefaultItemImage(_itemType);
        }
    }
}
