using System;
using Game.Item;

namespace Game.Hero
{
    [Serializable]
    public class HeroEquipment
    {
        public EquipmentData weapon;
        public EquipmentData head;
        public EquipmentData body;
        public EquipmentData accessory;
        
        public HeroEquipment(EquipmentData weapon, EquipmentData head, EquipmentData body, EquipmentData accessory)
        {
            this.weapon = weapon;
            this.head = head;
            this.body = body;
            this.accessory = accessory;
        }

        public void SetEquipment(EquipmentData equipment)
        {
            switch (equipment.equipmentType)
            {
                case EquipmentData.EquipmentType.Weapon:
                    weapon = equipment;
                    break;
                case EquipmentData.EquipmentType.Head:
                    head = equipment;
                    break;
                case EquipmentData.EquipmentType.Body:
                    body = equipment;
                    break;
                case EquipmentData.EquipmentType.Accessory:
                    accessory = equipment;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UnsetEquipment(EquipmentData.EquipmentType type)
        {
            switch (type)
            {
                case EquipmentData.EquipmentType.Weapon:
                    weapon = null;
                    break;
                case EquipmentData.EquipmentType.Head:
                    head = null;
                    break;
                case EquipmentData.EquipmentType.Body:
                    body = null;
                    break;
                case EquipmentData.EquipmentType.Accessory:
                    accessory = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int GetAttack()
        {
            var totalAttack = weapon != null ? weapon.attack : 0;
            if (accessory != null)
            {
                totalAttack += accessory.attack;
            }
            return totalAttack;;
        }
        
        public int GetHealth()
        {
            var totalHealth = 0;
            if (head != null)
            {
                totalHealth += head.health;
            }
            if (body != null)
            {
                totalHealth += body.health;
            }
            if (accessory != null)
            {
                totalHealth += accessory.health;
            }
            return totalHealth;
        }

        public int GetDefense()
        {
            var totalDefense = 0;
            if (head != null)
            {
                totalDefense += head.defense;
            }
            if (body != null)
            {
                totalDefense += body.defense;
            }
            if (accessory != null)
            {
                totalDefense += accessory.defense;
            }
            return totalDefense;
        }
    }
}