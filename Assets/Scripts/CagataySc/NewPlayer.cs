using GameCreator.Runtime.Characters;
using UnityEngine;

namespace CagataySc
{
    public class NewPlayer : Entity
    {
        private CombatManager currentCombat;

        public Animator locomAnim;
        public void UseAbility(int index)
        {
            var abilityToCast = abilities[index];
            abilityToCast.Cast(this, currentCombat.currentEnemy);
            
            DisablePlayerInput();
            
            abilityToCast.OnAbilityEffectComplete.AddListener(() =>
            {
                abilityToCast.OnAbilityEffectComplete.RemoveAllListeners();
                EventsHandler.OnTurnEnd.Invoke();
            });
        }
        
        public void SetEnemyTarget(CombatManager combat)
        {
            currentCombat = combat;
            
            combat.SetupPlayerAbilityButtons(abilities);
        }

        public void SwitchInputMapCombat()
        {
            //GetComponent<Character>().enabled = false;
            locomAnim.enabled = false;
            // animatör kapat
        }
        
        public void SwitchInputMapOpenWorld()
        {
            //GetComponent<Character>().enabled = true;
            locomAnim.enabled = true;
            // animatör aç
        }

        public void EnablePlayerInput()
        {
            currentCombat.SetPlayerInputs(true);
        }

        public void DisablePlayerInput()
        {
            currentCombat.SetPlayerInputs(false);
        }
    }
}
