using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Stats;


namespace CagataySc
{
    public abstract class BaseAbility : MonoBehaviour
    {
        //public Actions IsStartRunning;
        //public Actions IsEndRunning;

        //public Actions IsAttacking;
        //public Actions IsIdle;
        public Traits Stats;
        public int turnCount;
        private int tempTurn;
        public GameObject myButton;
        [SerializeField] protected int abilityDamage;
        [SerializeField] protected float attackAnimDur, travelDistance, moveDuration, rotationDuration;
        
        public UnityEvent OnAbilityEffectComplete = new();

        public virtual void Cast(Entity currentEntity, Entity targetEntity)
        {
            
            Debug.Log("BaseCast");
            AddCD();
            OnAbilityEffectComplete.Invoke();
        }

        public int GetStat(string str)
        {
            return (int) Stats.RuntimeStats.Get(str).Value;
        }

        public void TurnLimiter()
        {
            if (myButton != null)
            {
                if (tempTurn != turnCount)
                {
                    myButton.transform.GetChild(1).gameObject.SetActive(true);
                    myButton.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + (turnCount - tempTurn);
                    tempTurn++;
                    //myButton.GetComponent<Button>().interactable = false;
                }
                else
                {
                    myButton.transform.GetChild(1).gameObject.SetActive(false);
                   // myButton.GetComponent<Button>().interactable = true;

                    tempTurn = 0;
                    if (CombatManager.instance.playerAbilitysCD.Contains(this))
                    {
                        CombatManager.instance.playerAbilitysCD.Remove(this);
                    }
                }
            }
          
        }

        private void AddCD()
        {
            if (!CombatManager.instance.playerAbilitysCD.Contains(this))
            {
                CombatManager.instance.playerAbilitysCD.Add(this);
            }
            
        }

    }
}
