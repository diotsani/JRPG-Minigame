using Game.Skills;
using UnityEngine;

namespace Game.Enemy
{
    [CreateAssetMenu(fileName = "New Encounter Data", menuName = "Game/Encounter Data")]
    public class EncounterData : ScriptableObject
    {
        public string id;
        public EnemyData[] enemies;
    }
}