using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

namespace CagataySc
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager instance;
        [SerializeField] private HealthDisplayer playerHealthDisplayer, enemyHealthDisplayer;
        [SerializeField] private List<Button> playerAbilityButtons;
        [SerializeField] private List<BaseAbility> playerAbilitys;
        [SerializeField] public List<BaseAbility> playerAbilitysCD;
        [SerializeField] private TextMeshProUGUI turnText;

        [HideInInspector] public NewPlayer currentPlayer;
        [HideInInspector] public NewAI currentEnemy;
        
        private bool isPlayerTurn = true;

        private void Awake()
        {
            instance = this;
        }

        private void OnEnable()
        {
            EventsHandler.OnCombatStart.Event += StartCombat;
            EventsHandler.OnTurnEnd.Event += SwitchTurns;
        }

        private void OnDisable()
        {
            EventsHandler.OnCombatStart.Event -= StartCombat;
            EventsHandler.OnTurnEnd.Event -= SwitchTurns;
        }
        
        public void SetupPlayerAbilityButtons(List<BaseAbility> abilities)
        {
            for (var i = 0; i < abilities.Count; i++)
            {
                var ability = abilities[i];
                var button = playerAbilityButtons[i];

                var i1 = i;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    currentPlayer.UseAbility(i1);
                });
            }
        }
        
        public void SetPlayerInputs(bool value)
        {
            foreach (var button in playerAbilityButtons)
            {
                button.interactable = value;
            }
        }

        private void StartCombat(NewPlayer player, NewAI enemy)
        {
            isPlayerTurn = true;
            currentPlayer = player;
            currentEnemy = enemy;
            turnText.text = "Senin Sýran!";
            currentPlayer.SwitchInputMapCombat();
            SetPlayerInputs(true);
            currentPlayer.SetEnemyTarget(this);
            enemy.SetTargetPlayer(this);
            
            playerHealthDisplayer.SetupDisplayer(player.Health, player.EntityData.EntityName);
            enemyHealthDisplayer.SetupDisplayer(enemy.Health, enemy.EntityData.EntityName);
        }


        private void SwitchTurns()
        {
            if (currentPlayer.Health.IsDead())
            {
                currentPlayer.SwitchInputMapOpenWorld();
                EventsHandler.OnCombatEnd.Invoke(false);
                currentPlayer.ResetEntityHealth();
                currentEnemy.ResetEntityHealth();
                currentPlayer.PlayAnimation(AnimatorHashes.IsDeadComplete);
                return;
            } 
            
            if (currentEnemy.Health.IsDead())
            {
                currentPlayer.SwitchInputMapOpenWorld();
                EventsHandler.OnCombatEnd.Invoke(true);
                currentPlayer.ResetEntityHealth();
                currentEnemy.ResetEntityHealth();
                
                return;
            }

            if (isPlayerTurn)
            {
                
                turnText.text = "Düþmanýn Sýrasý!";
                currentEnemy.CastRandomAbility();
                isPlayerTurn = false;
            }
            else
            {
                TurnCDSkills();
                turnText.text = "Senin Sýran!";
                currentPlayer.EnablePlayerInput();
                isPlayerTurn = true;
            }
        }

        private void TurnCDSkills()
        {
            for (int i = 0; i < playerAbilitysCD.Count; i++)
            {
                playerAbilitysCD[i].TurnLimiter();
            }
        }


        private void OnApplicationQuit()
        {
            EventsHandler.ClearAllSubscribers();
        }
    }
}
