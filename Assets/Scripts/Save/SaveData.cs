using System;
using System.Collections.Generic;
using Game.Equipment;
using Game.Hero;
using Game.Inventory;
using UnityEngine;

namespace Save
{
    [Serializable]
    public struct PlayerSaveData
    {
        public string playerName;
        public Vector3 playerPosition;
        public Vector3 playerMove;
        public Quaternion playerLook;
    }

    [Serializable]
    public struct InventorySaveData
    {
        public List<InventorySlot> inventorySlots;
        public List<EquipmentSlot> equipmentSlots;
    }
    
    [Serializable]
    public struct HeroSaveData
    {
        public List<HeroSlot> heroSlots;
        public List<string> heroParties;
    }
    
    [Serializable]
    public struct CutsceneSaveData
    {
        public List<string> cutscenes;
    }
}