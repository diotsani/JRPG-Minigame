using System;
using Save;
using UnityEngine;

namespace Players
{
    public class PlayerManager : MonoBehaviour, ISaveable
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerFollower playerFollower1;

        private void Awake()
        {
            SaveSystem.Register(this);
        }

        private void OnDestroy()
        {
            SaveSystem.Unregister(this);
        }

        #region SaveData
        public void Save(ref SaveSystem.SaveData data)
        {
            Debug.Log("Save Player Data");
            data.PlayerSaveData = new PlayerSaveData()
            {
                playerName = GameManager.Instance.PlayerName,
                playerPosition = playerController.transform.position,
                playerMove = playerController.PlayerMove,
                playerLook = playerController.Look
            };
        }

        public void Load(in SaveSystem.SaveData data)
        {
            var saveData = data.PlayerSaveData;
            //playerName = saveData.playerName;
            //mainCharacter.NameText = playerName;
            GameManager.Instance.LoadPlayer(saveData.playerName);
            playerController.ControllerMove(saveData.playerPosition, saveData.playerMove, saveData.playerLook);
            Debug.Log("Load Player Data");
        }
        #endregion
    }
}