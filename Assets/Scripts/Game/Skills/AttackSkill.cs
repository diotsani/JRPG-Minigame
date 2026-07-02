using Battles;
using UnityEngine;

namespace Game.Skills
{
    [CreateAssetMenu(fileName = "New Attack Skill Data", menuName = "Game/Skills/Attack Skill")]
    public class AttackSkill : SkillData
    {
        [Header("Attack")]
        [Tooltip("Multiplier Percentage")]public float damageMultiplier;
        public override void Execute(BattleUnit caster, BattleUnit target)
        {
            
        }

        public override void Execute(BattleUnit caster, BattleUnit[] targets)
        {
            
        }
    }
}