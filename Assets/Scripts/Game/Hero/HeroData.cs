using Battles;
using Game.Skills;
using UnityEngine;

namespace Game.Hero
{
    [CreateAssetMenu(fileName = "New Hero Data", menuName = "Game/Hero Data")]
    public class HeroData : ScriptableObject
    {
        public string id;
        public string heroName;
        public string heroDescription;
        public Sprite heroIcon;
        public Sprite heroPortrait;
        public BattleHero heroPrefab;
        [Header("Stats")]
        public int heroLevel;
        public int heroExperience;
        public int heroAttack;
        public int heroHealth;
        public int heroDefense;
        public int heroMana;
        [Header("Skills")]
        public SkillData[] attackSkills;
        public SkillData[] abilitySkills;
        public SkillData[] defendSkills;
    }
}