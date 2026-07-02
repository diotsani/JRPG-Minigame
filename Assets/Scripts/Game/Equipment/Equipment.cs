using System.Collections.Generic;
using Game.Inventory;
using Game.Item;

namespace Game.Equipment
{
    public class Equipment
    {
        private readonly List<EquipmentSlot> _slots = new();
        
        public EquipmentSlot GetItem(int index)
        {
            return _slots[index];
        }
        
        public bool HasItem(ItemData itemData)
        {
            return _slots.Exists(x => x.itemData.id == itemData.id);
        }
        
        public int Count => _slots.Count;

        private EquipmentSlot GetSlot(EquipmentData itemData)
        {
            return _slots.Find(x => x.itemData.id == itemData.id);
        }
    }
}