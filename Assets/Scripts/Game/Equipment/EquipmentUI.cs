using Game.Hero;
using Game.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Equipment
{
    public class EquipmentUI : MonoBehaviour
    {
        [SerializeField] private EquipmentSlot slot;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Image heroEquipIcon;
        [SerializeField] private TMP_Text itemName;
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
            Debug.Log($"Clicked on {slot.itemData.itemName}");
            EquipmentManager.Instance.SelectEquipmentSlot(this, slot);
        }

        public void Equip()
        {
            heroEquipIcon.gameObject.SetActive(true);
            heroEquipIcon.sprite = HeroManager.Instance.GetHeroIcon(slot.equippedId);
        }

        public void Unequip()
        {
            heroEquipIcon.gameObject.SetActive(false);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        public void SetItem(EquipmentSlot s)
        {
            slot = s;
            itemIcon.sprite = slot.itemData.itemIcon;
            itemName.text = slot.itemData.itemName;
            if (slot.equippedId != "")
            {
                heroEquipIcon.gameObject.SetActive(true);
                heroEquipIcon.sprite = HeroManager.Instance.GetHeroIcon(slot.equippedId);
            }
            else
            {
                heroEquipIcon.gameObject.SetActive(false);
            }
        }

        public void SetItem(EquipmentData data)
        {
            
        }
    }
}