using UnityEngine;

namespace Game.Item
{
    [CreateAssetMenu(fileName = "New Consumable Item", menuName = "Game/Item/Consumable")]
    public class ConsumableData : ItemData
    {
        [Header("Consumable Data")]
        public int healthRestore;
        public int manaRestore;
        public int staminaRestore;
    }
}