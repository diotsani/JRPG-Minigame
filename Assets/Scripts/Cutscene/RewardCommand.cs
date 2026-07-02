using Cysharp.Threading.Tasks;
using Game.Item;
using Save;
using UnityEngine;

namespace Cutscene
{
    [CreateAssetMenu(fileName = "New Reward Command", menuName = "Cutscene/Command/Reward Command")]
    public class RewardCommand : CutsceneCommand
    {
        public RewardCommandType rewardType;
        public RewardData rewards;
        public override UniTask Execute(CutsceneContext context)
        {
            // Implement the logic for the reward command here
            // For example, you can trigger a reward animation, update the player's inventory, etc.
            Debug.Log("Reward command executed.");
            foreach (var reward in rewards.rewardItems)
            {
                context.Data.AddItem(reward.itemData, reward.quantity);
            }
            SaveSystem.Save();
            return UniTask.CompletedTask;
        }
    }
}