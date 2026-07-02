using System;
using Cysharp.Threading.Tasks;
using Game.Hero;
using Game.Skills;
using Spine;
using UnityEngine;
using Event = UnityEngine.Event;

namespace Battles
{
    public class BattleHero : BattleUnit
    {
        public HeroSlot heroSlot;
        [SerializeField] private KnightControl knightControl;
        private bool _attack;
        
        public void Initialize(HeroSlot slot)
        {
            heroSlot = slot;
            stats = new BattleStats(
                heroSlot.GetAttack(),
                heroSlot.GetDefense(), 
                heroSlot.GetHealth(),
                heroSlot.GetMana());
        }

        private void Start()
        {
            knightControl.spineAnimationState.Event += OnSpineEvent;
        }
        
        private void OnDisable()
        {
            knightControl.spineAnimationState.Event -= OnSpineEvent;
        }
        
        private void OnSpineEvent(TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == "sword_attack")
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
                await UniTask.Delay(TimeSpan.FromSeconds(knightControl.GetAnimTime()));
            }
        }

        protected override void PlayAnimation(AnimationType type)
        {
            switch (type)
            {
                case AnimationType.Idle:
                    knightControl.idle();
                    break;
                case AnimationType.Attack:
                    knightControl.attack_1();
                    break;
                case AnimationType.Ability:
                    knightControl.skill_1();
                    break;
                case AnimationType.Defence:
                    knightControl.skill_3();
                    break;
                case AnimationType.Hit:
                    knightControl.getHit();
                    break;
                case AnimationType.Death:
                    knightControl.death();
                    break;
                case AnimationType.Run:
                    knightControl.running();
                    break;
            }
        }

        public SkillData[] GetAttackSkills()
        {
            return heroSlot.heroData.attackSkills;
        }
        
        public SkillData[] GetAbilitySkills()
        {
            return heroSlot.heroData.abilitySkills;
        }

        public SkillData[] GetDefendSkills()
        {
            return heroSlot.heroData.defendSkills;
        }

        public override string GetUnitName()
        {
            return heroSlot.heroData.heroName;
        }

        public override Sprite GetUnitSprite()
        {
            return heroSlot.heroData.heroIcon;
        }
    }
}