using Battles;
using Game.Skills;
using UnityEngine;

namespace Game.Enemy
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "Game/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public string id;
        public string enemyName;
        public string enemyDescription;
        public Sprite enemyIcon;
        public Sprite enemyPortrait;
        public BattleEnemy enemyPrefab;
        [Header("Stats")]
        public int enemyLevel;
        public float enemyAttack;
        public float enemyHealth;
        public float enemyDefense;
        public float enemyMana;
        [Header("Skills")]
        public SkillData[] attackSkills;
        public SkillData[] abilitySkills;
    }
}