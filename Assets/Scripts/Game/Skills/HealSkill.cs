using Battles;
using UnityEngine;

namespace Game.Skills
{
    [CreateAssetMenu(fileName = "New Heal Skill Data", menuName = "Game/Skills/Heal Skill")]
    public class HealSkill : SkillData
    {
        public float heal;
        public override void Execute(BattleUnit caster, BattleUnit target)
        {
            target.Heal(heal);
        }

        public override void Execute(BattleUnit caster, BattleUnit[] targets)
        {
            foreach (var target in targets)
            {
                target.Heal(heal);
            }
        }
    }
}