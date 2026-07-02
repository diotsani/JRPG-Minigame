using Game.Item;
using Interfaces;
using Managers;
using UnityEngine;

namespace Game.Collectible
{
    public class Collectible : MonoBehaviour, ICollectible
    {
        [SerializeField] private ItemData itemData;
        
        public void Collected()
        {
            gameObject.SetActive(false);
            DataManager.Instance.AddItem(itemData);
        }
    }
}