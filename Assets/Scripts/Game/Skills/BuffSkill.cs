using Battles;
using UnityEngine;

namespace Game.Skills
{
    [CreateAssetMenu(fileName = "New Buff Skill Data", menuName = "Game/Skills/Buff Skill")]
    public class BuffSkill : SkillData
    {
        public override void Execute(BattleUnit caster, BattleUnit target)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(BattleUnit caster, BattleUnit[] targets)
        {
            throw new System.NotImplementedException();
        }
    }
}