using System;
using Battles;
using Game.Item;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private InventorySlot slot;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemCount;
        [SerializeField] private Button button;

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
        
        private void OnButtonClick()
        {
            Debug.Log($"Clicked on {slot.itemData.name} with quantity {slot.quantity}");
            if (BattleManager.Instance != null)
            {
                BattleManager.Instance.SelectConsumableItem(slot.itemData as ConsumableData);
            }
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        public void SetItem(InventorySlot s)
        {
            slot = s;
            itemIcon.sprite = slot.itemData.itemIcon;
            itemCount.text = slot.quantity > 1 ? slot.quantity.ToString() : "";
        }
    }
}