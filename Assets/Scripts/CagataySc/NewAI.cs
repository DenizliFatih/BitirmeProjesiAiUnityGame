using System;
using System.Collections.Generic;
using UnityEngine;

namespace CagataySc
{
    public class NewAI : Entity
    {
        private CombatManager currentCombat;
        
        public void CastRandomAbility()
        {
            var randomAbility = abilities[UnityEngine.Random.Range(0, abilities.Count)];
            randomAbility.Cast(this, currentCombat.currentPlayer);
            
            randomAbility.OnAbilityEffectComplete.AddListener(() =>
            {
                randomAbility.OnAbilityEffectComplete.RemoveAllListeners();
                EventsHandler.OnTurnEnd.Invoke();
            });
        }
        
        public void SetTargetPlayer(CombatManager combat)
        {
            currentCombat = combat;
        }
    }
}
