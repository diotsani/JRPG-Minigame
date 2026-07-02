using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Hero
{
    public class HeroUI : MonoBehaviour
    {
        [SerializeField] private HeroSlot slot;
        [SerializeField] private Image heroIcon;
        [SerializeField] private TMP_Text heroText;
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
            Debug.Log($"Clicked on {slot.heroData.heroName}");
            HeroManager.Instance.SelectHeroSlot(this, slot);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        public void SetHero(HeroSlot s)
        {
            slot = s;
            heroIcon.sprite = slot.heroData.heroIcon;
            heroText.text = slot.heroData.heroName;
        }
    }
}