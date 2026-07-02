using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battles.UI
{
    public class HeroBattleUI : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text hpBackText;
        [SerializeField] private TMP_Text mpText;
        [SerializeField] private TMP_Text mpBackText;
        [SerializeField] private Slider hpBar;
        [SerializeField] private Slider mpBar;
        private BattleUnit _unit;
        private float _maxHp;
        private float _maxMp;

        public void Initialize(BattleUnit unit)
        {
            _unit = unit;
            iconImage.sprite = unit.GetUnitSprite();
            //nameText.text = unit.GetUnitName();
            
            _maxHp = unit.GetUnitHealth();
            _maxMp = unit.GetUnitMana();
            
            UpdateHp(_maxHp);
            UpdateMp(_maxMp);
            
            _unit.OnHealthChanged += UpdateHp;
            _unit.OnManaChanged += UpdateMp;
            SetActive(true);
        }

        private void OnEnable()
        {
            if(_unit==null)return;
            _unit.OnHealthChanged += UpdateHp;
            _unit.OnManaChanged += UpdateMp;
        }

        private void OnDisable()
        {
            if(_unit==null)return;
            _unit.OnHealthChanged -= UpdateHp;
            _unit.OnManaChanged -= UpdateMp;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        private void UpdateHp(float value)
        {
            if (value <= 0) value = 0;
            hpBar.value = value / _maxHp;
            value = Mathf.RoundToInt(value);
            hpBackText.text = value.ToString("0000");
            hpText.text = $"{value}";
        }

        private void UpdateMp(float value)
        {
            if (value <= 0) value = 0;
            mpBar.value = value / _maxMp;
            value = Mathf.RoundToInt(value);
            mpBackText.text = value.ToString("0000");
            mpText.text = $"{value}";
        }
    }
}