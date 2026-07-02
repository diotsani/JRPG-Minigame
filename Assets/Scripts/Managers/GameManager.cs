using System;
using Cutscene;
using Fungus;
using Players;
using Save;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Naming Character")] 
    [SerializeField] private GameObject namingPanel;
    [SerializeField] private InputField namingInput;
    
    [Header("Fungus Character")]
    [SerializeField] private string playerName = "Hero";
    public string PlayerName => playerName;
    [SerializeField] Character mainCharacter;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SaveSystem.Load();
    }

    private Action _renameAction;
    public void OpenNamingPanel()
    {
        namingPanel.SetActive(true);
    }

    public void RenamePlayer()
    {
        namingPanel.SetActive(false);
        playerName = namingInput.text;
        mainCharacter.NameText = playerName;
        CutsceneManager.Instance.EventComplete(EventCommandHandler.Intro0);
    }

    public void LoadPlayer(string n)
    {
        playerName = n;
        mainCharacter.NameText = playerName;
    }
}
