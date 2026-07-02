using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battles.UI
{
    public class EnemyBattleUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text hpBackText;
        [SerializeField] private Slider hpBar;
        [SerializeField] private Vector3 uiOffset;
        private BattleUnit _unit;
        private float _maxHp;

        public void Initialize(BattleUnit unit)
        {
            _unit = unit;
            
            _maxHp = unit.GetUnitHealth();
            UpdateHp(_maxHp);
            
            _unit.OnHealthChanged += UpdateHp;

            Vector3 position = unit.transform.position;
            if (Camera.main != null) transform.position = Camera.main.WorldToScreenPoint(position) + uiOffset;

            SetActive(true);
        }

        private void OnEnable()
        {
            if(_unit==null)return;
            _unit.OnHealthChanged += UpdateHp;
        }

        private void OnDisable()
        {
            if(_unit==null)return;
            _unit.OnHealthChanged -= UpdateHp;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        private void UpdateHp(float value)
        {
            if (value <= 0) value = 0;
            hpBar.value = value / _maxHp;
            hpBackText.text = value.ToString("0000");
            hpText.text = $"{value}";
        }
    }
}