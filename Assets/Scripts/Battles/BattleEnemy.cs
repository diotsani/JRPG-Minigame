using System;
using Cysharp.Threading.Tasks;
using Game.Enemy;
using Game.Skills;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battles
{
    public class BattleEnemy : BattleUnit
    {
        [SerializeField] private Animator animator;
        private EnemyData _enemyData;
        private bool _attack;
        public void Initialize(EnemyData data)
        {
            _enemyData = data;
            stats = new BattleStats(
                data.enemyAttack,
                data.enemyDefense, 
                data.enemyHealth,
                data.enemyMana);
        }

        private void OnEnable()
        {
            EventManager.OnAnimationEvent += OnAnimationEvent;
        }

        private void OnDisable()
        {
            EventManager.OnAnimationEvent -= OnAnimationEvent;
        }

        private void OnAnimationEvent(string obj)
        {
            if (obj == "enemy_attack")
            {
                _attack = true;
                Debug.Log("_attack Hit!");
            }
        }

        protected override async UniTask AsyncPlayAnimation(AnimationType type)
        {
            PlayAnimation(type);
            if (type == AnimationType.Attack)
            {
                await UniTask.WaitUntil(() => _attack);
                _attack = false;
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                await UniTask.Delay(TimeSpan.FromSeconds(animator.GetCurrentAnimatorStateInfo(0).length));
            }
        }

        protected override void PlayAnimation(AnimationType type)
        {
            switch (type)
            {
                case AnimationType.Idle:
                    animator.Play("Idle");
                    break;
                case AnimationType.Attack:
                    animator.Play("Attack");
                    break;
                case AnimationType.Hit:
                    animator.Play("Hit");
                    break;
                case AnimationType.Death:
                    animator.Play("Death");
                    break;
                case AnimationType.Ability:
                    break;
                case AnimationType.Defence:
                    break;
            }
        }

        public SkillData[] GetAttackSkills()
        {
            return _enemyData.attackSkills;
        }

        public SkillData GetRandomAttackSkill()
        {
            return _enemyData.attackSkills[Random.Range(0, _enemyData.attackSkills.Length)];
        }
        
        public SkillData[] GetAbilitySkills()
        {
            return _enemyData.abilitySkills;
        }

        public override string GetUnitName()
        {
            return _enemyData.enemyName;
        }

        public override Sprite GetUnitSprite()
        {
            return _enemyData.enemyIcon;
        }
    }
}