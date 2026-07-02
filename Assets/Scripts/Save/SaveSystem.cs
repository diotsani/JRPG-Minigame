using System;
using System.Collections.Generic;
using System.IO;
using Game.Inventory;
using UnityEngine;

namespace Save
{
    public static class SaveSystem
    {
        [Serializable]
        public struct SaveData
        {
            public int Version;
            public PlayerSaveData PlayerSaveData;
            public InventorySaveData  InventorySaveData;
            public HeroSaveData  HeroSaveData;
            public CutsceneSaveData CutsceneSaveData;
        }

        private static SaveData _saveData = new SaveData();

        private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "save.save");

        public static bool HasSave()
        {
            return File.Exists(SavePath);
        }

        public static void Save()
        {
            Debug.Log("Save Data");
            try
            {
                _saveData.Version = 1;
                HandleSaveData();
                File.WriteAllText(SavePath, JsonUtility.ToJson(_saveData, true));
                Debug.Log("Save Data Success");
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static void Load()
        {
            if (!HasSave())
            {
                Debug.Log("Save Data not found"); 
                return;
            }
            try
            {
                string json = File.ReadAllText(SavePath);
                _saveData = JsonUtility.FromJson<SaveData>(json);
                HandleLoadData();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static void DeleteSave()
        {
            if (HasSave())
            {
                File.Delete(SavePath);
            }

            _saveData = new SaveData();
        }
        
        private static readonly List<ISaveable> saveables = new();

        public static void Register(ISaveable saveable)
        {
            if (!saveables.Contains(saveable))
                saveables.Add(saveable);
        }

        public static void Unregister(ISaveable saveable)
        {
            saveables.Remove(saveable);
        }

        public static void HandleSaveData()
        {
            foreach (var saveable in saveables)
                saveable.Save(ref _saveData);
        }

        public static void HandleLoadData()
        {
            foreach (var saveable in saveables)
                saveable.Load(in _saveData);
        }

        /*private static void HandleSaveData()
        {
            GameManager.Instance.Save(ref _saveData.PlayerSaveData);
            InventoryManager.Instance.Save(ref _saveData.InventorySaveData);
        }
        
        private static void HandleLoadData()
        {
            GameManager.Instance.Load(_saveData.PlayerSaveData);
            InventoryManager.Instance.Load(_saveData.InventorySaveData);
        }*/
    }
}