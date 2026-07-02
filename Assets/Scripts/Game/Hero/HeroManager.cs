using System;
using System.Collections.Generic;
using Game.Equipment;
using Game.Item;
using Managers;
using Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Hero
{
    public class HeroManager : MonoBehaviour
    {
        public static HeroManager Instance { get; private set; }
        //private Hero _hero = new Hero();
        private DataManager _data;
        private HeroSlot _selectedHeroSlot;
        [Header("Hero UI")]
        [SerializeField] private Transform heroContent;
        [SerializeField] private HeroUI heroUI;
        private List<HeroUI> _heroes = new();
        [Header("Hero Stats")]
        [SerializeField] private TMP_Text heroNameText;
        [SerializeField] private Image heroImage;
        [SerializeField] private TMP_Text heroLevelText;
        [SerializeField] private TMP_Text heroStatsText;

        private void Awake()
        {
            Instance = this;
            _data = DataManager.Instance;
        }

        private void OnEnable()
        {
            EventManager.OnOpenEquipment += OpenHero;
            EventManager.OnCloseEquipment += CloseHero;
        }

        private void OnDisable()
        {
            EventManager.OnOpenEquipment -= OpenHero;
            EventManager.OnCloseEquipment -= CloseHero;
        }

        private void OpenHero()
        {
            _selectedHeroSlot  = GetHero(0);
            UpdateHeroStats();
            
            if (_heroes.Count != 0) return;
            for (int i = 0; i < HeroCount(); i++)
            {
                var data = GetHero(i);
                var ui = Instantiate(heroUI, heroContent);
                ui.SetHero(data);
                _heroes.Add(ui);
            }
        }
        
        private void CloseHero()
        {
            SaveSystem.Save();
        }

        private HeroSlot GetHero(int index)
        {
            return _data.GetHero(index);
        }

        public void SetEquipment(EquipmentData data)
        {
            _selectedHeroSlot.SetEquipment(data);
            UpdateHeroStats();
        }
        
        public void UnsetEquipment(EquipmentData.EquipmentType type)
        {
            _selectedHeroSlot.UnsetEquipment(type);
            UpdateHeroStats();
        }

        private void UpdateHeroStats()
        {
            heroNameText.text = _selectedHeroSlot.heroData.heroName;
            heroImage.sprite = _selectedHeroSlot.heroData.heroPortrait;
            heroLevelText.text = $"Lv.{_selectedHeroSlot.heroData.heroLevel}";
            heroStatsText.text = $"Attack:\t{_selectedHeroSlot.GetAttack()}" +
                                 $"\nHealth:\t{_selectedHeroSlot.GetHealth()}" +
                                 $"\nDefense:\t{_selectedHeroSlot.GetDefense()}" +
                                 $"\nMana:\t\t{_selectedHeroSlot.GetMana()}";
        }

        public void SelectHeroSlot(HeroUI ui, HeroSlot slot)
        {
            _selectedHeroSlot = slot;
            UpdateHeroStats();
            EquipmentManager.Instance.RefreshEquipmentHero();
        }

        private int HeroCount()
        {
            return _data.HeroCount();
        }

        public HeroSlot GetSelectedHero()
        {
            return _selectedHeroSlot;
        }

        public Sprite GetHeroIcon(string id)
        {
            return _data.GetHero(id).heroData.heroIcon;
        }
    }
}