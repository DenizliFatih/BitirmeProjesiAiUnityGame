using DG.Tweening;
using UnityEngine;

namespace CagataySc
{
    public class AIAbility : BaseAbility
    {
        public override void Cast(Entity currentEntity, Entity targetEntity)
        {

            int firstabilitydmg = (int)( abilityDamage - (GetStat("defense") * 0.3f));

            if (abilityDamage <= 0)
            {
                abilityDamage = 1;
            }

            Debug.LogError("CastingAbility");
            currentEntity.PlayAnimation(AnimatorHashes.IsRunning);

            var targetDirection = currentEntity.transform.forward;
            var spawnPointEnemy = currentEntity.transform.position + targetDirection.normalized * travelDistance;
            
            var startPosition = currentEntity.transform.position;
            var startRotation = currentEntity.transform.rotation.eulerAngles;
            
            currentEntity.transform.DOMove(spawnPointEnemy, moveDuration).OnComplete(() =>
            {
                currentEntity.PlayAnimation(AnimatorHashes.IsSkill2);
                
                DOVirtual.DelayedCall(attackAnimDur, () =>
                {
                    targetEntity.DealDamageToEntity(firstabilitydmg);
                    
                    currentEntity.PlayAnimation(AnimatorHashes.IsIdle);
                    currentEntity.transform.DORotate(startRotation + new Vector3(0f,180f,0f), rotationDuration).OnComplete(() =>
                    {
                        currentEntity.PlayAnimation(AnimatorHashes.IsRunning);
                        currentEntity.transform.DOMove(startPosition, moveDuration).OnComplete(() =>
                        {
                            currentEntity.PlayAnimation(AnimatorHashes.IsIdle);
                            currentEntity.transform.DORotate(startRotation, rotationDuration).OnComplete(() =>
                            {
                                base.Cast(currentEntity, targetEntity);
                            });
                        });
                    });
                });
            });
        }
    }
}
