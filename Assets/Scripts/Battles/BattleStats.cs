using System;

namespace Battles
{
    [Serializable]
    public class BattleStats
    {
        public float maxHP;
        public float currentHP;
        public float maxMP;
        public float currentMP;
        public float attack;
        public float defense;

        public BattleStats(float atk, float def, float hp, float mp)
        {
            attack = atk;
            defense = def;
            maxHP = hp;
            maxMP = mp;
            currentHP = maxHP;
            currentMP = maxMP;
        }

        public float GetAttack()
        {
            return attack;
        }

        public float GetDefense()
        {
            return defense;
        }
    }
}