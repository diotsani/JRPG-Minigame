using System;
using Game.Hero;
using Game.Inventory;
using Game.Item;

namespace Game.Equipment
{
    [Serializable]
    public class EquipmentSlot : InventorySlot
    {
        private bool equipped;
        public string equippedId;

        public EquipmentSlot(ItemData itemData, int quantity, bool equipped, string equippedId) : base(itemData, quantity)
        {
            this.itemData = itemData;
            this.equipped = equipped;
            this.equippedId = equippedId;
            Obtain();
        }

        public void Initialize(ItemData data)
        {
            itemData = data;
        }
        
        public void Equip(HeroSlot heroSlot)
        {
            equipped = true;
            equippedId = heroSlot.heroData.id;
        }

        public void Unequip()
        {
            equipped = false;
            equippedId = "";
        }
    }
}