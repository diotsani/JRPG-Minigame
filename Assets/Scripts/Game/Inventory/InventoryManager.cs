using System;
using System.Collections.Generic;
using Game.Equipment;
using Game.Item;
using Managers;
using Save;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }
        //private readonly Inventory _inventory = new Inventory();
        private DataManager _data;
        
        [Header("Inventory UI")]
        [SerializeField] private Transform inventoryContent;
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private EquipmentUI equipmentUI;
        private List<InventoryUI> _inventories = new();
        private List<EquipmentUI> _equipments = new();
        
        
        private void Awake()
        {
            Instance = this;
            _data = DataManager.Instance;
        }

        private void Start()
        {
            CreateInventory();
        }

        private void OnEnable()
        {
            EventManager.OnOpenInventory += OpenInventory;
            EventManager.OnCloseInventory += CloseInventory;
        }

        private void OnDisable()
        {
            EventManager.OnOpenInventory -= OpenInventory;
            EventManager.OnCloseInventory -= CloseInventory;
        }

        private void OpenInventory()
        {
            RefreshInventory();
        }

        private void CloseInventory()
        {
            /*foreach (var ui in _inventories)
            {
                ui.SetActive(false);
            }
            foreach (var ui in _equipments)
            {
                ui.SetActive(false);
            }*/
        }

        private void CreateInventory()
        {
            for (int i = 0; i < ItemCount(); i++)
            {
                var itemData = _data.GetInventorySlot(i);
                var ui = Instantiate(inventoryUI, inventoryContent);
                ui.SetItem(itemData);
                _inventories.Add(ui);
            }
            for (int i = 0; i < EquipmentCount(); i++)
            {
                var itemData = _data.GetEquipmentSlot(i);
                var ui = Instantiate(equipmentUI, inventoryContent);
                ui.SetItem(itemData);
                _equipments.Add(ui);
            }
        }

        private void RefreshInventory()
        {
            foreach (var ui in _inventories)
            {
                ui.SetActive(false);
            }
            
            for (int i = 0; i < ItemCount(); i++)
            {
                var itemData = _data.GetInventorySlot(i);
                // inv 20, count 25
                if(i > _inventories.Count - 1)
                {
                    var ui = Instantiate(inventoryUI, inventoryContent);
                    ui.SetItem(itemData);
                    _inventories.Add(ui);
                }
                else
                {
                    _inventories[i].SetItem(itemData);
                    _inventories[i].SetActive(true);
                }
            }
            
            foreach (var ui in _equipments)
            {
                ui.SetActive(false);
            }

            for (int i = 0; i < EquipmentCount(); i++)
            {
                var itemData = _data.GetEquipmentSlot(i);
                if(i > _equipments.Count - 1)
                {
                    var ui = Instantiate(equipmentUI, inventoryContent);
                    ui.SetItem(itemData);
                    _equipments.Add(ui);
                }
                else
                {
                    _equipments[i].SetItem(itemData);
                    _equipments[i].SetActive(true);
                }
            }
        }

        private int ItemCount()
        {
            return _data.InventoryCount();
        }

        private int EquipmentCount()
        {
            return _data.EquipmentCount();
        }
    }
}