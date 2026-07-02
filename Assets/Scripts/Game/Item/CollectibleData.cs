using UnityEngine;

namespace Game.Item
{
    [CreateAssetMenu(fileName = "New Collectible Item", menuName = "Game/Item/Collectible")]
    public class CollectibleData : ItemData
    {
        public enum CollectibleType
        {
            Wood,
            Stone,
            Iron
        }
        public CollectibleType collectibleType;
    }
}