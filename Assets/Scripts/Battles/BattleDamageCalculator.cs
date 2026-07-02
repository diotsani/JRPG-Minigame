using Game.Skills;
using UnityEngine;

namespace Battles
{
    public static class BattleDamageCalculator
    {
        public static float CalculateAttackDamage(BattleUnit attacker, AttackSkill skill, BattleUnit defender)
        {
            var atk = attacker.stats.GetAttack() * skill.damageMultiplier / 100;
            return Mathf.Max(0, atk - defender.stats.GetDefense());
        }
    }
}