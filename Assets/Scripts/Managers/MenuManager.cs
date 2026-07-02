using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject equipmentPanel;
    [Header("Button")]
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button closeInventoryButton;
    [SerializeField] private Button equipmentButton;
    [SerializeField] private Button closeEquipmentButton;
    [SerializeField] private Button helpButton;

    private void Awake()
    {
        menuPanel.SetActive(true);
        inventoryPanel.SetActive(false);
        equipmentPanel.SetActive(false);
    }

    private void OnEnable()
    {
        inventoryButton.onClick.AddListener(OnInventoryButtonClick);
        closeInventoryButton.onClick.AddListener(OnCloseInventoryButtonClick);
        equipmentButton.onClick.AddListener(OnEquipmentButtonClick);
        closeEquipmentButton.onClick.AddListener(OnCloseEquipmentButtonClick);
        //helpButton.onClick.AddListener(OnHelpButtonClick);
    }

    private void OnDisable()
    {
        inventoryButton.onClick.RemoveAllListeners();
        closeInventoryButton.onClick.RemoveAllListeners();
        equipmentButton.onClick.RemoveAllListeners();
        closeEquipmentButton.onClick.RemoveAllListeners();
        //helpButton.onClick.RemoveAllListeners();
    }

    private void OnInventoryButtonClick()
    {
        inventoryPanel.SetActive(true);
        EventManager.OpenInventory();
    }

    private void OnCloseInventoryButtonClick()
    {
        inventoryPanel.SetActive(false);
        EventManager.CloseInventory();
    }

    private void OnEquipmentButtonClick()
    {
        equipmentPanel.SetActive(true);
        EventManager.OpenEquipment();
    }

    private void OnCloseEquipmentButtonClick()
    {
        equipmentPanel.SetActive(false);
        EventManager.CloseEquipment();
    }

    private void OnHelpButtonClick()
    {
        
    }
}
