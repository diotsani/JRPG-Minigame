using System;
using System.Collections.Generic;
using System.Linq;
using Battles.UI;
using Cysharp.Threading.Tasks;
using Game.Hero;
using Game.Item;
using Game.Skills;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battles
{
    public class BattleManager : MonoBehaviour
    {
        public enum BattleState
        {
            Start,
            PlayerTurn,
            EnemyTurn,
            Win,
            Lose
        }
        public static BattleManager Instance;
        [SerializeField] private BattleState battleState = BattleState.Start;
        [SerializeField] private BattleCamera battleCamera;
        [Header("Units")]
        [SerializeField] private Transform unitTransform;
        [SerializeField] private Transform[] heroTransforms;
        [SerializeField] private Transform[] enemyTransforms;
        private readonly List<BattleHero> _heroUnits = new List<BattleHero>();
        private readonly List<BattleEnemy> _enemyUnits = new List<BattleEnemy>();
        private List<BattleUnit> _allUnits = new List<BattleUnit>();

        [Header("Unit UI")]
        [SerializeField] private HeroBattleUI[] heroBattleUI;
        [SerializeField] private EnemyBattleUI[] enemyBattleUI;
        
        private BattleUnit _selectedUnit;
        private BattleUnit _targetedUnit;
        private BattleUnit[] _targetedUnits;
        private SkillData _selectedSkill;
        private ConsumableData _consumableData;
        private int _turnIndex = 0;

        private void Awake()
        {
            Instance = this;
            LoadBattle();
            InitializeUnits();
        }

        private void Start()
        {
            BattleStart();
        }

        private void LoadBattle()
        {
            var parties = DataManager.Instance.GetHeroParties();
            foreach (var t in heroBattleUI)
            {
                t.SetActive(false);
            }
            for (int i = 0; i < parties.Count; i++)
            {
                var hero = Instantiate(parties[i].heroData.heroPrefab, heroTransforms[i].position, Quaternion.identity, unitTransform);
                hero.Initialize(parties[i]);
                heroBattleUI[i].Initialize(hero);
                _heroUnits.Add(hero);
                _allUnits.Add(hero);
            }

            var enemies = DataManager.Instance.GetEncounterData().enemies;
            foreach (var t in enemyBattleUI)
            {
                t.SetActive(false);
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                var enemy = Instantiate(enemies[i].enemyPrefab, enemyTransforms[i].position,Quaternion.identity, unitTransform);
                enemy.Initialize(enemies[i]);
                //enemyBattleUI[i].Initialize(enemy);
                _enemyUnits.Add(enemy);
                _allUnits.Add(enemy);
            }
        }

        private void InitializeUnits()
        {

        }
    
        private void BattleStart()
        {
            battleState = BattleState.Start;
            _ = NextTurn();
        }
    
        private void BattleEnd()
        {
            Debug.Log("End Battle");
            BattleUI.Instance.EndGameBattle();
        }

        public void ResetBattleCamera()
        {
            battleCamera.ResetVCamera();
        }
        
        private async UniTask NextTurn()
        {
            _selectedUnit = GetActiveUnit();
            if (_selectedUnit.unitType == BattleUnit.UnitType.Hero)
            {
                battleCamera.SetHeroVCam(_selectedUnit.transform);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                HeroAction(_selectedUnit as BattleHero);
            }
            else
            {
                battleCamera.SetEnemyVCam(_selectedUnit.transform);
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                EnemyAction(_selectedUnit as BattleEnemy);
            }
        }

        public void EndTurn()
        {
            if (CheckEndBattle())
            {
                BattleEnd();
                return;
            }
            AddTurnIndex();
            _ = NextTurn();
        }

        public void SetSelectedSkill(SkillData skillData)
        {
            _selectedSkill = skillData;
        }

        public void SetSelectedUnit(int index)
        {
            var type = _selectedSkill.targetType;
            switch (type)
            {
                case SkillData.SkillTargetType.Self:
                    _targetedUnit = _selectedUnit;
                    _selectedUnit.UseSkillSingle(_selectedSkill, _targetedUnit);
                    break;
                case SkillData.SkillTargetType.Ally:
                    _targetedUnit = GetHero(index);
                    _selectedUnit.UseSkillSingle(_selectedSkill, _targetedUnit);
                    break;
                case SkillData.SkillTargetType.Enemy:
                    _targetedUnit = GetEnemy(index);
                    _selectedUnit.UseSkillSingle(_selectedSkill, _targetedUnit);
                    break;
                case SkillData.SkillTargetType.AllEnemies:
                    _targetedUnits = GetActiveEnemy();
                    _selectedUnit.UseSkillMulti(_selectedSkill, _targetedUnits);
                    break;
                case SkillData.SkillTargetType.AllAllies:
                    _targetedUnits = GetActiveHero();
                    _selectedUnit.UseSkillMulti(_selectedSkill, _targetedUnits);
                    break;
            }
        }

        private void HeroAction(BattleHero battleHero)
        {
            battleState = BattleState.PlayerTurn;
            BattleUI.Instance.OpenActionMenu(_selectedUnit);
        }

        private void EnemyAction(BattleEnemy battleEnemy)
        {
            battleState = BattleState.EnemyTurn;
            SetSelectedSkill(battleEnemy.GetRandomAttackSkill());
            SetSelectedUnit(Random.Range(0, GetHeroCount()));
        }
        
        public void SelectConsumableItem(ConsumableData itemData)
        {
            _consumableData = itemData;
            ItemManager.Instance.OpenConfirmationPanel();
        }

        public void UseConsumableItem()
        {
            DataManager.Instance.ReduceItem(_consumableData);
            _ = _selectedUnit.UseConsumableItem(_consumableData);
        }
        
        private BattleUnit GetActiveUnit()
        {
            BattleUnit unit = _allUnits[_turnIndex];
            while (!unit.isAlive)
            {
                AddTurnIndex();
                unit = _allUnits[_turnIndex];
            }
            
            return unit;
        }

        private void AddTurnIndex()
        {
            _turnIndex++;
            if (_turnIndex < _allUnits.Count) return;
            // Rebuild
            _allUnits = _allUnits.Where(u => u.isAlive).ToList();
            _turnIndex = 0;
        }

        private BattleUnit[] GetActiveHero()
        {
            return _heroUnits.Where(u => u.isAlive).ToArray<BattleUnit>();
        }

        private BattleUnit[] GetActiveEnemy()
        {
            return _enemyUnits.Where(u => u.isAlive).ToArray<BattleUnit>();
        }
        public BattleHero GetHero(int index)
        {
            var h = _heroUnits[index];
            if (h.IsDead())
            {
                h = GetActiveHero()[0] as BattleHero;
            }

            return h;
        }
        
        public BattleEnemy GetEnemy(int index)
        {
            var e = _enemyUnits[index];
            if (e.IsDead())
            {
                e = GetActiveEnemy()[0] as BattleEnemy;
            }

            return e;
        }

        public int GetHeroCount()
        {
            return _heroUnits.Count(e => e.isAlive);
        }
        
        public int GetEnemyCount()
        {
            return _enemyUnits.Count(e => e.isAlive);
        }

        private bool CheckEndBattle()
        {
            return CheckHeroWinBattle() || CheckEnemyWinBattle();
        }

        private bool CheckHeroWinBattle()
        {
            return GetEnemyCount() == 0;
        }

        private bool CheckEnemyWinBattle()
        {
            return GetHeroCount() == 0;
        }
    }
}
