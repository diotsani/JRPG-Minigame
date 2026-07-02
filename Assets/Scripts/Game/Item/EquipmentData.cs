using UnityEngine;

namespace Game.Item
{
    [CreateAssetMenu(fileName = "New Equipment Item", menuName = "Game/Item/Equipment")]
    public class EquipmentData : ItemData
    {
        public enum EquipmentType
        {
            Weapon,
            Head,
            Body,
            Accessory
        }
        [Header("Equipment Data")]
        public EquipmentType equipmentType;
        public int attack;
        public int health;
        public int defense;
    }
}