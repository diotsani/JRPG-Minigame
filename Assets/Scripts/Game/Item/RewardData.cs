using System;
using UnityEngine;

namespace Game.Item
{
    [CreateAssetMenu(fileName = "New Reward Item", menuName = "Game/Reward")]
    public class RewardData : ScriptableObject
    {
        public string rewardId;
        public RewardItemData[] rewardItems;
    }
    [Serializable]
    public class RewardItemData
    {
        public ItemData itemData;
        public int quantity = 1;
    }
}