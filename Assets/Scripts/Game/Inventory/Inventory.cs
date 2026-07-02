using System.Collections.Generic;
using System.Linq;
using Game.Equipment;
using Game.Item;

namespace Game.Inventory
{
    public class Inventory 
    {
        private List<InventorySlot> _slots = new();
        private List<EquipmentSlot> _equipmentSlots = new();

        public void Load(List<InventorySlot> inventorySlots, List<EquipmentSlot> equipmentSlots)
        {
            _slots = new List<InventorySlot>(inventorySlots);
            _equipmentSlots = new List<EquipmentSlot>(equipmentSlots);
        }
        public void AddItem(ItemData itemData, int quantity = 1)
        {
            if(ItemStackable(itemData))
            {
                var slot = GetSlot(itemData);
                slot.AddQuantity(quantity);
                return;
            }
            if(itemData.itemType == ItemData.ItemsType.Equipment)
            {
                _equipmentSlots.Add(new EquipmentSlot(itemData, quantity, false, ""));
                return;
            }
            _slots.Add(new InventorySlot(itemData, quantity));
        }
        
        public void RemoveItem(ItemData itemData)
        {
            _slots.Remove(_slots.Find(x => x.itemData.id == itemData.id));
        }

        public void ReduceItem(ItemData itemData)
        {
            var slot = GetSlot(itemData);
            slot.ReduceQuantity();
            if(slot.quantity <= 0)
            {
                RemoveItem(itemData);
            }
        }
        
        public InventorySlot GetItem(int index)
        {
            return _slots[index];
        }

        public InventorySlot[] GetConsumableSlots()
        {
            return _slots.Where(x => x.itemData.itemType == ItemData.ItemsType.Consumable).ToArray();
        }
        
        public EquipmentSlot GetEquipmentItem(int index)
        {
            return _equipmentSlots[index];
        }
        
        public bool ItemStackable(ItemData itemData)
        {
            //return _slots.Exists(x => x.itemData.id == itemData.id);
            return itemData.itemStackable && _slots.Exists(x => x.itemData.id == itemData.id);
        }
        
        public int ItemCount => _slots.Count;
        public int EquipmentCount => _equipmentSlots.Count;

        private InventorySlot GetSlot(ItemData itemData)
        {
            return _slots.Find(x => x.itemData.id == itemData.id);
        }
        public List<InventorySlot> GetSlots(ItemData.ItemsType type)
        {
            return _slots.Where(x => x.itemData.itemType == type).ToList();
        }

        public List<InventorySlot> GetInventorySlots()
        {
            return _slots;
        }

        public List<EquipmentSlot> GetEquipmentSlots()
        {
            return _equipmentSlots;
        }

        public EquipmentSlot GetHeroEquipmentSlot(EquipmentData.EquipmentType type, string id)
        {
            return _equipmentSlots.Find(x => x.equippedId == id && ((EquipmentData)x.itemData).equipmentType == type);
        }
        
        public List<ItemData> GetItems(ItemData.ItemsType type)
        {
            return _slots.Where(x => x.itemData.itemType == type).Select(x => x.itemData).ToList();
        }
    }
}