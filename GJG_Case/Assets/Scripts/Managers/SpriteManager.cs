using Enums;
using Managers.Base;
using UnityEngine;

namespace Managers
{
    public class SpriteManager : MonoSingleton<SpriteManager>
    {
        [Header("Red")]
        [SerializeField] private Sprite itemRedDefault;
        [SerializeField] private Sprite itemRedRocket;
        [SerializeField] private Sprite itemRedBomb;
        [SerializeField] private Sprite itemRedVacuum;

        [Header("Pink")]
        [SerializeField] private Sprite itemPinkDefault;
        [SerializeField] private Sprite itemPinkRocket;
        [SerializeField] private Sprite itemPinkBomb;
        [SerializeField] private Sprite itemPinkVacuum;
        
        [Header("Yellow")]
        [SerializeField] private Sprite itemYellowDefault;
        [SerializeField] private Sprite itemYellowRocket;
        [SerializeField] private Sprite itemYellowBomb;
        [SerializeField] private Sprite itemYellowVacuum;

        [Header("Green")]
        [SerializeField] private Sprite itemGreenDefault;
        [SerializeField] private Sprite itemGreenRocket;
        [SerializeField] private Sprite itemGreenBomb;
        [SerializeField] private Sprite itemGreenVacuum;

        [Header("Blue")]
        [SerializeField] private Sprite itemBlueDefault;
        [SerializeField] private Sprite itemBlueRocket;
        [SerializeField] private Sprite itemBlueBomb;
        [SerializeField] private Sprite itemBlueVacuum;

        [Header("Purple")]
        [SerializeField] private Sprite itemPurpleDefault;
        [SerializeField] private Sprite itemPurpleRocket;
        [SerializeField] private Sprite itemPurpleBomb;
        [SerializeField] private Sprite itemPurpleVacuum;

        public Sprite GetDefaultItemImage(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Red:
                    return itemRedDefault;
                case ItemType.Pink:
                    return itemPinkDefault;
                case ItemType.Yellow:
                    return itemYellowDefault;
                case ItemType.Green:
                    return itemGreenDefault;
                case ItemType.Blue:
                    return itemBlueDefault;
                case ItemType.Purple:
                    return itemPurpleDefault;
            }
            return null;
        }
    
        public Sprite GetItemRocketImage(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Red:
                    return itemRedRocket;
                case ItemType.Pink:
                    return itemPinkRocket;
                case ItemType.Yellow:
                    return itemYellowRocket;
                case ItemType.Green:
                    return itemGreenRocket;
                case ItemType.Blue:
                    return itemBlueRocket;
                case ItemType.Purple:
                    return itemPurpleRocket;
            }
            return null;
        }
        
        public Sprite GetItemBombImage(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Red:
                    return itemRedBomb;
                case ItemType.Pink:
                    return itemPinkBomb;
                case ItemType.Yellow:
                    return itemYellowBomb;
                case ItemType.Green:
                    return itemGreenBomb;
                case ItemType.Blue:
                    return itemBlueBomb;
                case ItemType.Purple:
                    return itemPurpleBomb;
            }
            return null;
        }
        
        public Sprite GetItemVacuumImage(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Red:
                    return itemRedVacuum;
                case ItemType.Pink:
                    return itemPinkVacuum;
                case ItemType.Yellow:
                    return itemYellowVacuum;
                case ItemType.Green:
                    return itemGreenVacuum;
                case ItemType.Blue:
                    return itemBlueVacuum;
                case ItemType.Purple:
                    return itemPurpleVacuum;
            }
            return null;
        }
    }
}
