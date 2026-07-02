using Battles;
using UnityEngine;

namespace Game.Skills
{
    public abstract class SkillData : ScriptableObject
    {
        public SkillType skillType;
        public string skillName;
        public string skillDescription;
        public Sprite skillIcon;
        public SkillTargetType targetType;
        public int mpCost;

        public abstract void Execute(BattleUnit caster, BattleUnit target);
        public abstract void Execute(BattleUnit caster, BattleUnit[] targets);

        public enum SkillType
        {
            Attack,
            Ability,
            Defence,
        }
        public enum SkillTargetType
        {
            Self,
            Ally,
            Enemy,
            AllEnemies,
            AllAllies
        }
    }
}