using System;
using UnityEngine;

namespace CagataySc
{
    public class BaseHealth
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth{ get; private set; }
    
        public Action OnDeath;
        public Action<int> OnHealthChanged;
    
        public BaseHealth(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }
    
        public void TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
            OnHealthChanged?.Invoke(CurrentHealth);
        
            if (CurrentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    
        public void Heal(int healAmount)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + healAmount, 0, MaxHealth);
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    
        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }

        public void ResetHealth()
        {
            CurrentHealth = MaxHealth;
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }
}
