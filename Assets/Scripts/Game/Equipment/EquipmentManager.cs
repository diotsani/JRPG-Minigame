using System;
using System.Collections.Generic;
using Game.Hero;
using Game.Inventory;
using Game.Item;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Equipment
{
    public class EquipmentManager : MonoBehaviour
    {
        public static EquipmentManager Instance;
        private DataManager _data;
        private EquipmentSlot _selectedEquipmentSlot;
        private EquipmentUI _selectedEquipmentUI;
        private HeroSlot _selectedHeroSlot;
        
        [Header("Equipment UI")] 
        [SerializeField] private Transform equipmentContent;
        [SerializeField] private EquipmentUI equipmentUI;
        private readonly List<EquipmentUI> _equipments = new();
        
        [Header("Equipment Hero")]
        [SerializeField] private EquipmentUI weaponEquipmentUI;
        [SerializeField] private EquipmentUI headEquipmentUI;
        [SerializeField] private EquipmentUI bodyEquipmentUI;
        [SerializeField] private EquipmentUI accessoryEquipmentUI;
        
        [Header("Equipment Confirmation")]
        [SerializeField] private GameObject equipmentConfirmation;
        [SerializeField] private Button equipButton;
        [SerializeField] private Button unequipButton;
        [SerializeField] private Button cancelButton;

        private void Awake()
        {
            Instance = this;
            _data = DataManager.Instance;
        }

        private void OnEnable()
        {
            EventManager.OnOpenEquipment += OpenInventory;
            EventManager.OnCloseEquipment += CloseInventory;
            equipButton.onClick.AddListener(ConfirmEquip);
            unequipButton.onClick.AddListener(ConfirmUnequip);
            cancelButton.onClick.AddListener(CancelEquip);
        }

        private void OnDisable()
        {
            EventManager.OnOpenEquipment -= OpenInventory;
            EventManager.OnCloseEquipment -= CloseInventory;
            equipButton.onClick.RemoveAllListeners();
            unequipButton.onClick.RemoveAllListeners();
            cancelButton.onClick.RemoveAllListeners();
        }
        
        private void OpenInventory()
        {
            if(_equipments.Count == 0)
            {
                CreateEquipment();
            }
            else
            {
                RefreshEquipment();
            }
            RefreshEquipmentHero();
        }

        private void CloseInventory()
        {
            foreach (var ui in _equipments)
            {
                ui.SetActive(false);
            }
        }
        
        private void CreateEquipment()
        {
            for (int i = 0; i < ItemCount(); i++)
            {
                var itemData = _data.GetEquipmentSlots()[i];
                var ui = Instantiate(equipmentUI, equipmentContent);
                ui.SetItem(itemData);
                _equipments.Add(ui);
            }
        }

        private void RefreshEquipment()
        {
            for (int i = 0; i < ItemCount(); i++)
            {
                var itemData = _data.GetEquipmentSlots()[i];
                // inv 20, count 25
                if(i > _equipments.Count - 1)
                {
                    var ui = Instantiate(equipmentUI, equipmentContent);
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

        public void RefreshEquipmentHero()
        {
            weaponEquipmentUI.SetActive(false);
            headEquipmentUI.SetActive(false);
            bodyEquipmentUI.SetActive(false);
            accessoryEquipmentUI.SetActive(false);
            
             var hero = HeroManager.Instance.GetSelectedHero();
             if (hero != null)
             {
                 if(!hero.HasEquipment())return;
                 if(hero.HasWeapon())
                 {
                     var slot  = GetEquipmentSlot(EquipmentData.EquipmentType.Weapon,hero.heroData.id);
                     weaponEquipmentUI.SetItem(slot);
                     weaponEquipmentUI.SetActive(true);
                 }

                 if (hero.HasHead())
                 {
                     var slot  = GetEquipmentSlot(EquipmentData.EquipmentType.Head, hero.heroData.id);
                     headEquipmentUI.SetItem(slot);
                     headEquipmentUI.SetActive(true);
                 }

                 if (hero.HasBody())
                 {
                     var slot  = GetEquipmentSlot(EquipmentData.EquipmentType.Body, hero.heroData.id);
                     bodyEquipmentUI.SetItem(slot);
                     bodyEquipmentUI.SetActive(true);
                 }

                 if (hero.HasAccessory())
                 {
                     var slot = GetEquipmentSlot(EquipmentData.EquipmentType.Accessory, hero.heroData.id);
                     accessoryEquipmentUI.SetItem(slot);
                     accessoryEquipmentUI.SetActive(true);
                 }
             }

             return;

             EquipmentSlot GetEquipmentSlot(EquipmentData.EquipmentType type, string id)
             {
                 return _data.GetHeroEquipmentSlot(type, id);
             }
        }

        public void SelectEquipmentSlot(EquipmentUI ui, EquipmentSlot slot)
        {
            _selectedHeroSlot = HeroManager.Instance.GetSelectedHero();
            equipmentConfirmation.SetActive(true);
            equipButton.gameObject.SetActive(slot.equippedId == "");
            unequipButton.gameObject.SetActive(slot.equippedId != "" && _selectedHeroSlot.heroData.id == slot.equippedId);
            _selectedEquipmentUI = ui;
            _selectedEquipmentSlot = slot;
        }
        
        private void ConfirmEquip()
        {
            if(_selectedEquipmentSlot != null)
            {
                // Equip the item
                _selectedEquipmentSlot.Equip(_selectedHeroSlot);
                _selectedEquipmentUI.Equip();
                HeroManager.Instance.SetEquipment(_selectedEquipmentSlot.itemData as EquipmentData);
                RefreshEquipmentHero();
            }
            CancelEquip();
        }

        private void ConfirmUnequip()
        {
            if(_selectedEquipmentSlot != null)
            {
                // UnEquip the item
                _selectedEquipmentSlot.Unequip();
                _selectedEquipmentUI.Unequip();
                HeroManager.Instance.UnsetEquipment(((EquipmentData)_selectedEquipmentSlot.itemData).equipmentType);
                RefreshEquipmentHero();
            }
            CancelEquip();
        }
        
        private void CancelEquip()
        {
            equipmentConfirmation.SetActive(false);
            _selectedEquipmentSlot = null;
            _selectedEquipmentUI = null;
        }

        private int ItemCount()
        {
            return _data.EquipmentCount();
        }
    }
}