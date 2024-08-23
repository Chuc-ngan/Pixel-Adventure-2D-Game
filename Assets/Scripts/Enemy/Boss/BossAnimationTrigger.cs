using UnityEngine;

namespace Assets.Scripts.Enemy.Boss
{
    internal class BossAnimationTrigger : Enemy_AnimationTriggers
    {
        public LayerMask playerLayer;
        private BossCharacter boss => GetComponentInParent<BossCharacter>();

        private void Relocate() => boss.FindPosition();

        private void MakeInvisible() => boss.fx.MakeTransprent(true);
        private void MakeVisible() => boss.fx.MakeTransprent(false);
        private void StartHealth() => boss.Health(true);
        private void EndHealth() => boss.Health(false);
        private void Dash()
        {
            boss.dashSkill.UseSkill();
            boss.SetVelocity(8 * boss.facingDir, 6);
        }

        protected override void AttackTrigger()
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(playerLayer);
            filter.useLayerMask = true;

            Collider2D[] hitColliders = new Collider2D[10];
            int hitCount = Physics2D.OverlapCollider(boss.hitBox, filter, hitColliders);

            for (int i = 0; i < hitCount; i++)
            {
                Collider2D hit = hitColliders[i];
                if (hit != null && hit.CompareTag("Player"))
                {
                    Player player = PlayerManager.instance.player;
                    if (player != null)
                    {
                        PlayerStats target = hit.GetComponent<PlayerStats>();
                        boss.stats.DoDamage(target);
                    }
                }
            }
        }
    }
}
