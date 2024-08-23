using UnityEngine;

namespace Assets.Scripts.Enemy.Boss
{
    public class BossAttackState : EnemyState
    {
        private BossCharacter boss;
        private Transform player;

        public BossAttackState(EnemyCharacter _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            boss = (BossCharacter)_enemyBase;
        }

        public override void Enter()
        {
            base.Enter();
            player = PlayerManager.instance.player.transform;
            boss.chanceToTeleport += 5;

            if ((player.position.x < boss.transform.position.x && boss.facingDir == 1)
            || (player.position.x > boss.transform.position.x && boss.facingDir == -1))
            {
                boss.Flip();
            }

            boss.SetVelocity(2 * boss.facingDir, 4);
        }

        public override void Exit()
        {
            base.Exit();

            boss.lastTimeAttacked = Time.time;
        }

        public override void Update()
        {
            base.Update();

            boss.SetZeroVelocity();

            if (triggerCalled)
            {
                if (boss.CanTeleport())
                    stateMachine.ChangeState(boss.teleportState);
                else
                    stateMachine.ChangeState(boss.battleState);
            }
        }
    }
}
