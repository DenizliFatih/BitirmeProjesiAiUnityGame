using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CagataySc
{
    public class HealthDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText, nameText;
        [SerializeField] private Image fillBar, backFillBar;
        
        private Tween _fillTween, _backFillTween;
        private int maxHealth;
        
        public void SetupDisplayer(BaseHealth health, string entityName)
        {
            health.OnHealthChanged += UpdateText;
            health.OnHealthChanged += DoFillEffect;
            
            maxHealth = health.MaxHealth;
            
            DoFillEffect(health.CurrentHealth);
            UpdateText(health.CurrentHealth);
            
            nameText.text = entityName;
        }

        private void DoFillEffect(int CurrentHealth)
        {
            _fillTween?.Kill();
            _fillTween = fillBar.DOFillAmount((float) CurrentHealth / maxHealth, 0.5f).OnComplete(() =>
            {
                _backFillTween?.Kill();
                _backFillTween = backFillBar.DOFillAmount((float) CurrentHealth / maxHealth, 0.5f).SetDelay(0.2f);
            });
        }
        
        private void UpdateText(int CurrentHealth)
        {
            healthText.text = CurrentHealth + " / " + maxHealth;
        }
    }
}
