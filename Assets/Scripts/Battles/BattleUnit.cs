using System;
using Cysharp.Threading.Tasks;
using Fungus;
using Game.Item;
using Game.Skills;
using UnityEngine;

namespace Battles
{
    public abstract class BattleUnit : MonoBehaviour
    {
        public enum UnitType
        {
            Hero,
            Enemy
        }
        protected enum AnimationType
        {
            Idle,
            Attack,
            Ability,
            Defence,
            Hit,
            Death,
            Run
        }
        public UnitType unitType;
        public BattleStats stats;
        public bool isAlive = true;
        
        public event Action<float> OnHealthChanged;
        public event Action<float> OnManaChanged;

        public virtual void UseSkillSingle(SkillData skill, BattleUnit target)
        {
            _ = UseSkill(skill, target);
        }

        public virtual async UniTask UseSkill(SkillData skill, BattleUnit target)
        {
            BattleManager.Instance.ResetBattleCamera();
            // wait camera
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            UseMana(-skill.mpCost);
            
            switch (skill.skillType)
            {
                case SkillData.SkillType.Attack:
                    await MoveTo(target);
                    await AsyncPlayAnimation(AnimationType.Attack);
                    PlayAnimation(AnimationType.Idle);
                    var atk = skill as AttackSkill;
                    float dmg = BattleDamageCalculator.CalculateAttackDamage(this, atk, target);
                    skill.Execute(this, target);
                    await target.TakeDamage(dmg);
                    await Return();
                    break;
                case SkillData.SkillType.Ability:
                    await AsyncPlayAnimation(AnimationType.Ability);
                    skill.Execute(this, target);
                    break;
                case SkillData.SkillType.Defence:
                    await AsyncPlayAnimation(AnimationType.Defence);
                    skill.Execute(this, target);
                    break;
            }

            // Waiting before end turn
            BattleManager.Instance.EndTurn();
        }
        
        public virtual void UseSkillMulti(SkillData skill, BattleUnit[] targets)
        {
            UseMana(-skill.mpCost);
            switch (skill.skillType)
            {
                case SkillData.SkillType.Attack:
                    var atk = skill as AttackSkill;
                    break;
                case SkillData.SkillType.Ability:
                    skill.Execute(this, targets);
                    break;
                case SkillData.SkillType.Defence:
                    skill.Execute(this, targets);
                    break;
            }
            
            // Waiting before end turn
            BattleManager.Instance.EndTurn();
        }
        
        private Vector3 _originalPosition;
        protected virtual async UniTask MoveTo(BattleUnit target)
        {
            PlayAnimation(AnimationType.Run);
            _originalPosition = transform.position;
            Vector3 targetPosition = target.transform.position;
            switch (unitType)
            {
                case UnitType.Hero:
                    targetPosition.x -= 1f;
                    break;
                case UnitType.Enemy:
                    targetPosition.x += 1f;
                    break;
            }
            
            float elapsedTime = 0f;
            float duration = 0.5f; // Duration of the movement in seconds

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(_originalPosition, targetPosition, elapsedTime / duration);
                await UniTask.Yield();
            }
            transform.position = targetPosition;
            PlayAnimation(AnimationType.Idle);
        }
        
        protected virtual async UniTask Return()
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            PlayAnimation(AnimationType.Run);
            Vector3 originalPosition = transform.position;
            Vector3 targetPosition = _originalPosition;
            
            float elapsedTime = 0f;
            float duration = 0.5f; // Duration of the movement in seconds

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / duration);
                await UniTask.Yield();
            }
            transform.position = targetPosition;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            PlayAnimation(AnimationType.Idle);
        }

        protected virtual async UniTask AsyncPlayAnimation(AnimationType type)
        {
            
        }

        protected virtual void PlayAnimation(AnimationType type)
        {
            
        }

        public virtual void Attack(BattleUnit target)
        {
            
        }
        
        public virtual void Damage(float damage)
        {
            
        }
        
        public virtual async UniTask TakeDamage(float damage)
        {
            stats.currentHP -= damage;
            OnHealthChanged?.Invoke(stats.currentHP);
            await AsyncPlayAnimation(AnimationType.Hit);
            if (stats.currentHP <= 0)
            {
                Death();
            }
            else
            {
                PlayAnimation(AnimationType.Idle);
            }
        }

        public async UniTask UseConsumableItem(ConsumableData item)
        {
            BattleManager.Instance.ResetBattleCamera();
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            
            if(item.healthRestore > 0)
            {
                Heal(item.healthRestore);
            }
            else if (item.manaRestore > 0)
            {
                UseMana(item.manaRestore);
            }
            
            BattleManager.Instance.EndTurn();
        }

        public virtual void Heal(float heal)
        {
            stats.currentHP += heal;
            OnHealthChanged?.Invoke(stats.currentHP);
        }

        public virtual void UseMana(float mana)
        {
            stats.currentMP += mana;
            OnManaChanged?.Invoke(stats.currentMP);
        }

        public virtual void Death()
        {
            _ = AsyncPlayAnimation(AnimationType.Death);
            isAlive = false;
        }

        public virtual string GetUnitName()
        {
            return unitType.ToString();
        }

        public float GetUnitHealth()
        {
            return stats.currentHP;
        }

        public float GetUnitMana()
        {
            return stats.currentMP;
        }

        public virtual Sprite GetUnitSprite()
        {
            return null;
        }

        public bool IsHero()
        {
            return unitType == UnitType.Hero;
        }

        public bool IsEnemy()
        {
            return unitType == UnitType.Enemy;
        }
        
        public virtual bool IsDead()
        {
            return stats.currentHP <= 0;
        }
    }
}