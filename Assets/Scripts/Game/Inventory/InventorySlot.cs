using System;
using Game.Item;

namespace Game.Inventory
{
    [Serializable]
    public class InventorySlot
    {
        public ItemData itemData;
        public int quantity;
        public string uniqueId;

        public InventorySlot(ItemData itemData, int quantity)
        {
            this.itemData = itemData;
            this.quantity = quantity;
            Obtain();
        }

        public void AddQuantity(int q)
        {
            quantity += q;
        }
        
        public void ReduceQuantity()
        {
            quantity--;
        }
        
        public void Obtain()
        {
            uniqueId = Guid.NewGuid().ToString();
        }
    }
}