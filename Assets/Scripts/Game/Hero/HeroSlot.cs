using System;
using Game.Item;

namespace Game.Hero
{
    [Serializable]
    public class HeroSlot
    {
        public HeroData heroData;
        public HeroEquipment heroEquipment;
        
        public HeroSlot(HeroData data)
        {
            heroData = data;
            heroEquipment = new HeroEquipment(null, null, null, null);
        }

        public void SetEquipment(EquipmentData data)
        {
            heroEquipment ??= new HeroEquipment(null, null, null, null);
            heroEquipment.SetEquipment(data);
        }

        public void UnsetEquipment(EquipmentData.EquipmentType type)
        {
            heroEquipment.UnsetEquipment(type);
        }

        public int GetAttack()
        {
            return heroData.heroAttack + (heroEquipment?.GetAttack() ?? 0);
        }
        
        public int GetHealth()
        {
            return heroData.heroHealth + (heroEquipment?.GetHealth() ?? 0);
        }
        
        public int GetDefense()
        {
            return heroData.heroDefense + (heroEquipment?.GetDefense() ?? 0);
        }
        
        public int GetMana()
        {
            return heroData.heroMana;
        }

        public bool HasEquipment()
        {
            return heroEquipment != null;
        }

        public bool HasWeapon()
        {
            return heroEquipment.weapon != null;
        }

        public bool HasHead()
        {
            return heroEquipment.head != null;
        }

        public bool HasBody()
        {
            return heroEquipment.body != null;
        }

        public bool HasAccessory()
        {
            return heroEquipment.accessory != null;
        }
    }
}