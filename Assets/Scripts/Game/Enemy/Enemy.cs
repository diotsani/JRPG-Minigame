using Interfaces;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour, IInteractable
    {
        [SerializeField] private EncounterData encounterData;
        public void Interact()
        {
            DataManager.Instance.SetEncounterData(encounterData);
            SceneManager.LoadScene("BattleScene");
        }
    }
}