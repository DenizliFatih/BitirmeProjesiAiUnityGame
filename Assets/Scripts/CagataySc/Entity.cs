using System.Collections.Generic;
using UnityEngine;

namespace CagataySc
{
    public class Entity : MonoBehaviour
    {
        [field : SerializeField] public EntityData EntityData { get; private set; }
        [SerializeField] protected List<BaseAbility> abilities;
        [SerializeField] protected Animator animator;
        
        public BaseHealth Health { get; private set; }
        
        protected virtual void Awake()
        {
            Health = new BaseHealth(EntityData.MaxHealth);
        }

        public void PlayAnimation(int triggerName)
        {
            animator.SetTrigger(triggerName);
        }
    }
}
