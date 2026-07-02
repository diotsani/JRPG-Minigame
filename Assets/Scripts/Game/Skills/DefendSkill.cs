using Battles;
using UnityEngine;

namespace Game.Skills
{
    [CreateAssetMenu(fileName = "New Defend Skill Data", menuName = "Game/Skills/Defend Skill")]
    public class DefendSkill : SkillData
    {
        public override void Execute(BattleUnit caster, BattleUnit target)
        {
            
        }

        public override void Execute(BattleUnit caster, BattleUnit[] targets)
        {
            
        }
    }
}