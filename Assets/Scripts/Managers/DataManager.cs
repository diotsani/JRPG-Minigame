using System;
using System.Collections.Generic;
using Cutscene;
using Game.Enemy;
using Game.Equipment;
using Game.Hero;
using Game.Inventory;
using Game.Item;
using Save;
using UnityEngine;

namespace Managers
{
    public class DataManager : MonoBehaviour, ISaveable
    {
        public static DataManager Instance;

        [Header("Hero")]
        [SerializeField] private HeroData mainHero;
        [SerializeField] private HeroData hero1;
        private readonly Hero _hero = new Hero();
        private readonly Inventory _inventory  = new Inventory();
        private readonly Equipment _equipment  = new Equipment();
        private readonly Cutscene.Cutscene _cutscene = new Cutscene.Cutscene();
        
        [SerializeField] private EncounterData _currentEncounterData;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            SaveSystem.Register(this);
        }

        private void OnDisable()
        {
            SaveSystem.Unregister(this);
        }

        private void Start()
        {
            // add main hero to hero list if not already added
            if (_hero.Count == 0)
            {
                AddHero(mainHero);
                AddHero(hero1);
            }
            SaveSystem.Load();
        }

        [ContextMenu("Save Data")]
        private void Save()
        {
            SaveSystem.Save();
        }
        
        [ContextMenu("Delete Save Data")]
        private void DeleteSave()
        {
            SaveSystem.DeleteSave();
        }

        #region Hero Data

        public HeroSlot GetHero(int index)
        {
            return _hero.GetHero(index);
        }
        
        public HeroSlot GetHero(string id)
        {
            return _hero.GetHero(id);
        }

        public void AddHero(HeroData data)
        {
            _hero.AddHero(data);
        }

        public int HeroCount()
        {
            return _hero.Count;
        }
        
        public List<HeroSlot> GetHeroSlots()
        {
            return _hero.GetHeroSlots();
        }

        public List<HeroSlot> GetHeroParties()
        {
            return _hero.GetHeroParties();
        }
        #endregion

        #region Inventory Data
        public InventorySlot GetInventorySlot(int index)
        {
            return _inventory.GetItem(index);
        }

        public InventorySlot[] GetConsumableSlots()
        {
            return _inventory.GetConsumableSlots();
        }
        
        public EquipmentSlot GetEquipmentSlot(int index)
        {
            return _inventory.GetEquipmentItem(index);
        }
        
        public List<InventorySlot> GetInventorySlots()
        {
            return _inventory.GetInventorySlots();
        }
        
        public List<EquipmentSlot> GetEquipmentSlots()
        {
            return _inventory.GetEquipmentSlots();
        }

        public EquipmentSlot GetHeroEquipmentSlot(EquipmentData.EquipmentType type,string id)
        {
            return _inventory.GetHeroEquipmentSlot(type, id);
        }

        public int InventoryCount()
        {
            return _inventory.ItemCount;
        }

        public int EquipmentCount()
        {
            return _inventory.EquipmentCount;
        }
        
        public void AddItem(ItemData itemData, int quantity = 1)
        {
            _inventory.AddItem(itemData, quantity);
        }

        public void RemoveItem(ItemData itemData)
        {
            _inventory.RemoveItem(itemData);
        }
        
        public void ReduceItem(ItemData itemData)
        {
            _inventory.ReduceItem(itemData);
        }
        #endregion
        
        #region Encounter Data
        public void SetEncounterData(EncounterData data)
        {
            _currentEncounterData = data;
        }

        public EncounterData GetEncounterData()
        {
            return _currentEncounterData;
        }
        #endregion

        #region Cutscene Data

        public void AddCutscene(string id)
        {
            _cutscene.Add(id);
        }
        
        public bool CheckCutscene(string id)
        {
            return _cutscene.Check(id);
        }
        
        #endregion
        
        
        #region SaveData
        public void Save(ref SaveSystem.SaveData data)
        {
            Debug.Log("Saving Hero Data");
            data.HeroSaveData.heroSlots = new List<HeroSlot>(_hero.GetHeroSlots());
            data.HeroSaveData.heroParties = new List<string>(_hero.GetHeroPartiesId());
            
            Debug.Log("Saving Inventory Data");
            data.InventorySaveData.inventorySlots = new List<InventorySlot>(_inventory.GetInventorySlots());
            data.InventorySaveData.equipmentSlots = new List<EquipmentSlot>(_inventory.GetEquipmentSlots());
            
            data.CutsceneSaveData.cutscenes = new List<string>(_cutscene.GetCutscenes());
        }

        public void Load(in SaveSystem.SaveData data)
        {
            var saveData = data.HeroSaveData;
            
            Debug.Log("Hero Loaded");
            _hero.Load(saveData.heroSlots, saveData.heroParties);
            
            Debug.Log("Inventory Loaded");
            _inventory.Load(data.InventorySaveData.inventorySlots, data.InventorySaveData.equipmentSlots);
            
            _cutscene.Load(data.CutsceneSaveData.cutscenes);
        }
        #endregion
    }
}