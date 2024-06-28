using UnityEngine;
using DG.Tweening;


namespace CagataySc
{
    public class Ability3 : BaseAbility
    {
        public override void Cast(Entity currentEntity, Entity targetEntity)
        {
            EntityData enemyData = targetEntity.EntityData;

            int firstabilitydmg = (int)((abilityDamage + (GetStat("attack") * 0.1f) + (GetStat("strength") * 0.05f) + (GetStat("dexterity") * 0.05f)) - (enemyData.Defense * 0.2f));

            if (abilityDamage <= 0)
            {
                abilityDamage = 1;
            }

            currentEntity.PlayAnimation(AnimatorHashes.IsRunning);

            var targetDirection = currentEntity.transform.forward;
            var spawnPointEnemy = currentEntity.transform.position + targetDirection.normalized * travelDistance;

            var startPosition = currentEntity.transform.position;
            var startRotation = currentEntity.transform.rotation.eulerAngles;

            currentEntity.transform.DOMove(spawnPointEnemy, moveDuration).OnComplete(() =>
            {
                currentEntity.PlayAnimation(AnimatorHashes.IsAutoAttack);

                DOVirtual.DelayedCall(attackAnimDur, () =>
                {
                    targetEntity.DealDamageToEntity(firstabilitydmg);

                    currentEntity.PlayAnimation(AnimatorHashes.IsIdle);
                    currentEntity.transform.DORotate(startRotation + new Vector3(0f, 180f, 0f), rotationDuration).OnComplete(() =>
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
