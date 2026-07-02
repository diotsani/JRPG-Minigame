using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Battles
{
    public class BattleActionUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text actionNameText;
        [SerializeField] private TMP_Text actionDescriptionText;
        [SerializeField] private Image actionImg;
        [SerializeField] private Button actionButton;

        public void Initialize(string actionName, string actionDesc, Sprite actionIcon, UnityAction action)
        {
            actionNameText.text = actionName;
            actionDescriptionText.text = actionDesc;
            actionImg.sprite = actionIcon;
            actionButton.onClick.AddListener(action);
            SetActive(true);
        }

        public void Disable()
        {
            actionButton.onClick.RemoveAllListeners();
            SetActive(false);
        }

        private void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}