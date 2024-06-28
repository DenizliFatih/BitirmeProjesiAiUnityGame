namespace CagataySc
{
    public static class Extensions
    {
        public static void DealDamageToEntity(this Entity entity, int damage)
        {
            
            entity.Health.TakeDamage(damage);
            if (entity.Health.IsDead())
            {
                entity.PlayAnimation(AnimatorHashes.IsDead);
            }
            else
            {
                entity.PlayAnimation(AnimatorHashes.IsTakeDamage);
            }
            
        }
        
        public static void ResetEntityHealth(this Entity entity)
        {
            entity.Health.ResetHealth();
        }
        public static void HealEntity(this Entity entity, int heal)
        {
            entity.Health.Heal(heal);
        }
    }
}
