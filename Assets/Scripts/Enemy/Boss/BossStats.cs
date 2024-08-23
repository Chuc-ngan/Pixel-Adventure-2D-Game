namespace Assets.Scripts.Enemy.Boss
{
    public class BossStats : EnemyStats
    {
        private BossCharacter boss;
        private int deathCount;

        protected override void Start()
        {
            base.Start();

            boss = GetComponent<BossCharacter>();
        }

        protected override void Die()
        {
            if (deathCount >= 1)
                base.Die();

            deathCount++;
        }
    }
}
