using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Item
{
    public abstract class ItemData : ScriptableObject
    {
        public enum ItemsType
        {
            Consumable,
            Equipment,
            QuestItem,
            Miscellaneous,
            Collectible
        }
        public string id;
        public ItemsType itemType;
        public string itemName;
        public string itemDescription;
        public Sprite itemIcon;
        public int itemPrice;
        public bool itemStackable;
        public int itemMaxStack;
    }
}
