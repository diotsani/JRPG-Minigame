using System;
using System.Collections.Generic;
using Battles;
using Battles.UI;
using Game.Equipment;
using Game.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance;
        private DataManager _data;
        [Header("Inventory UI")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject confirmationPanel;
        [SerializeField] private Transform inventoryContent;
        [SerializeField] private InventoryUI inventoryUI;
        [Header("Button")]
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button useButton;
        [SerializeField] private Button cancelUseButton;
        private readonly List<InventoryUI> _inventories = new();

        private void Awake()
        {
            Instance = this;
            _data = DataManager.Instance;
        }

        private void OnEnable()
        {
            cancelButton.onClick.AddListener(CloseItem);
            useButton.onClick.AddListener(UseItem);
            cancelUseButton.onClick.AddListener(CloseUseItem);
        }

        private void OnDisable()
        {
            cancelButton.onClick.RemoveAllListeners();
            useButton.onClick.RemoveAllListeners();
            cancelUseButton.onClick.RemoveAllListeners();
        }

        private void Start()
        {
            CreateInventory();
        }

        public void OpenInventoryItem()
        {
            inventoryPanel.SetActive(true);
            RefreshInventory();
        }

        private void CreateInventory()
        {
            for (int i = 0; i < _data.GetConsumableSlots().Length; i++)
            {
                var itemData = _data.GetConsumableSlots()[i];
                var ui = Instantiate(inventoryUI, inventoryContent);
                ui.SetItem(itemData);
                _inventories.Add(ui);
            }
        }

        private void RefreshInventory()
        {
            foreach (var ui in _inventories)
            {
                ui.SetActive(false);
            }
            
            for (int i = 0; i < _data.GetConsumableSlots().Length; i++)
            {
                var itemData = _data.GetConsumableSlots()[i];
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
        }

        public void OpenConfirmationPanel()
        {
            confirmationPanel.SetActive(true);
        }
        private void CloseItem()
        {
            inventoryPanel.SetActive(false);
            BattleUI.Instance.ReopenActionMenu();
        }

        private void UseItem()
        {
            confirmationPanel.SetActive(false);
            inventoryPanel.SetActive(false);
            BattleManager.Instance.UseConsumableItem();
        }

        private void CloseUseItem()
        {
            confirmationPanel.SetActive(false);
        }
    }
}