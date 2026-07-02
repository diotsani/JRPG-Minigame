using Game.Skills;
using Managers;
using Save;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Battles.UI
{
    public class BattleUI : MonoBehaviour
    {
        public static BattleUI Instance;
        [Header("Action Menu")]
        [SerializeField] private GameObject actionMenu;
        [SerializeField] private Vector2 actionMenuOffset;
        [Header("Actions")] 
        [SerializeField] private Button attackAction;
        [SerializeField] private Button abilityAction;
        [SerializeField] private Button itemAction;
        [SerializeField] private Button defendAction;
        [SerializeField] private BattleActionUI[] subActions;
        [Header("Selector")]
        [SerializeField] private GameObject selectHero;
        [SerializeField] private Vector3 selectHeroOffset;
        [SerializeField] private GameObject selectEnemy;
        [SerializeField] private Vector3 selectEnemyOffset;
        [Header("End Game")]
        [SerializeField] private GameObject endGamePanel;
        [SerializeField] private Button homeButton;
        [SerializeField] private Button repeatButton;
        private int _selectorIndex;
        private bool _isSelecting;
        
        private BattleHero _battleHero;
        private SkillData _skillData;
        private Camera  _camera;


        private void Awake()
        {
            Instance = this;
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            attackAction.onClick.AddListener(AttackAction);
            abilityAction.onClick.AddListener(AbilityAction);
            itemAction.onClick.AddListener(ItemAction);
            defendAction.onClick.AddListener(DefendAction);
            homeButton.onClick.AddListener(HomeButton);
            repeatButton.onClick.AddListener(RepeatButton);
        }

        private void OnDisable()
        {
            attackAction.onClick.RemoveAllListeners();
            abilityAction.onClick.RemoveAllListeners();
            itemAction.onClick.RemoveAllListeners();
            defendAction.onClick.RemoveAllListeners();
            homeButton.onClick.RemoveAllListeners();
            repeatButton.onClick.RemoveAllListeners();
        }

        private void Update()
        {
            MoveUnitSelector();
        }

        private void HomeButton()
        {
            SceneManager.LoadScene("HomeScene");
            SaveSystem.Save();
        }

        private void RepeatButton()
        {
            SceneManager.LoadScene("BattleScene");
        }

        public void OpenActionMenu(BattleUnit unit)
        {
            _battleHero = unit as BattleHero;
            actionMenu.SetActive(true);
            Vector3 menuPosition = unit.transform.position;
            actionMenu.transform.position = _camera.WorldToScreenPoint(menuPosition) + (Vector3)actionMenuOffset;
        }

        public void ReopenActionMenu()
        {
            OpenActionMenu(_battleHero);
        }
        
        private void OpenItemActionMenu()
        {
            CloseActionMenu();
            ItemManager.Instance.OpenInventoryItem();
        }

        private void CloseActionMenu()
        {
            actionMenu.SetActive(false);
        }

        private void AttackAction()
        {
            OpenSubActionMenu();
            SubActionMenu(_battleHero.GetAttackSkills());
        }

        private void AbilityAction()
        {
            OpenSubActionMenu();
            SubActionMenu(_battleHero.GetAbilitySkills());
        }

        private void ItemAction()
        {
            OpenItemActionMenu();
        }

        private void DefendAction()
        {
            OpenSubActionMenu();
            SubActionMenu(_battleHero.GetDefendSkills());
        }

        private void OpenSubActionMenu()
        {
            CloseActionMenu();
            foreach (BattleActionUI subAction in subActions)
            {
                subAction.Disable();
            }
        }

        private void SubActionMenu(SkillData[] data)
        {
            var skills = data;
            if (skills.Length == 1)
            {
                SelectUnit(skills[0]);
            }
            for (int i = 0; i < skills.Length; i++)
            {
                var d = skills[i];
                subActions[i].Initialize(d.skillName,d.skillDescription, d.skillIcon,
                    () =>
                    {
                        SelectUnit(d);
                    });
            }
        }
        
        private void SelectUnit(SkillData skillData)
        {
            _skillData = skillData;
            var type = skillData.targetType;
            BattleManager.Instance.SetSelectedSkill(skillData);
            if (GetUnitCount() == 1 || NextStep())
            {
                OnSelectUnit();
                return;
            }

            _isSelecting = true;
            _selectorIndex = 0;
            SetUnitSelectorPosition();
            SetActiveUnitSelector(true);
            return;

            bool NextStep()
            {
                return type is SkillData.SkillTargetType.Self;
            }
        }
        
        private void MoveUnitSelector()
        {
            if(!_isSelecting)return;
            if (Keyboard.current != null)
            {
                if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
                {
                    NextUnitSelector(1);
                    SetUnitSelectorPosition();
                }
                else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
                {
                    NextUnitSelector(-1);
                    SetUnitSelectorPosition();
                }
                else if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    _isSelecting = false;
                    SetActiveUnitSelector(false);
                    OnSelectUnit();
                }
            }
        }

        private void SetActiveUnitSelector(bool active)
        {
            var type = _skillData.targetType;
            switch (type)
            {
                case SkillData.SkillTargetType.Ally:
                    selectHero.SetActive(active);
                    break;
                case SkillData.SkillTargetType.Enemy:
                    selectEnemy.SetActive(active);
                    break;
            }
        }
        
        private void SetUnitSelectorPosition()
        {
            var type = _skillData.targetType;
            switch (type)
            {
                case SkillData.SkillTargetType.Ally:
                    var hero = BattleManager.Instance.GetHero(_selectorIndex);
                    selectHero.transform.position = hero.transform.position + selectHeroOffset;
                    break;
                case SkillData.SkillTargetType.Enemy:
                    var enemy = BattleManager.Instance.GetEnemy(_selectorIndex);
                    selectEnemy.transform.position = enemy.transform.position + selectEnemyOffset;
                    break;
            }
        }
        
        private void NextUnitSelector(int n)
        {
            _selectorIndex += n;
            var count = GetUnitCount();
           
            if (_selectorIndex >= count)
            {
                _selectorIndex = 0;
            }
            else if(_selectorIndex < 0)
            {
                _selectorIndex = count - 1;
            }
        }

        private void OnSelectUnit()
        {
            BattleManager.Instance.SetSelectedUnit(_selectorIndex);
        }

        public void EndGameBattle()
        {
            endGamePanel.SetActive(true);
        }

        private int GetUnitCount()
        {
            var type = _skillData.targetType;
            return type switch
            {
                SkillData.SkillTargetType.Self => 0,
                SkillData.SkillTargetType.Ally => BattleManager.Instance
                    .GetHeroCount(),
                SkillData.SkillTargetType.Enemy => BattleManager.Instance
                    .GetEnemyCount(),
                _ => 0
            };
        }
    }
}