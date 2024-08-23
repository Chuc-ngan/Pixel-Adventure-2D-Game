using UnityEngine;

namespace Assets.Scripts.Enemy.Boss
{
    public class BossLaserLoadState : EnemyState
    {
        private BossCharacter boss;
        private float cooldown = 1f;
        private Transform player;

        public BossLaserLoadState(EnemyCharacter _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            boss = (BossCharacter)_enemyBase;
        }

        public override void Enter()
        {
            base.Enter();

            player = PlayerManager.instance.player.transform;

            stateTimer = cooldown;
            boss.laserSkill.CreateLaserLoad();
            boss.laserSkill.state = Skills.AbilityState.Active;
        }

        public override void Update()
        {
            base.Update();

            float moveDir = 0;

            if (player.position.x > boss.transform.position.x)
                moveDir = 1;
            else if (player.position.x < boss.transform.position.x)
                moveDir = -1;

            boss.SetVelocity(boss.moveSpeed * moveDir, 0);

            if (stateTimer < 0 && triggerCalled)
            {
                stateMachine.ChangeState(boss.laserBeamState);
            }
        }
    }
}
