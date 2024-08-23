using UnityEngine;

namespace Assets.Scripts.Enemy.Boss
{
    public class BossBattleState : EnemyState
    {
        private BossCharacter boss;
        private Transform player;
        private int moveDir;

        public BossBattleState(EnemyCharacter _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            boss = (BossCharacter)_enemyBase;
        }

        public override void Enter()
        {
            base.Enter();

            player = PlayerManager.instance.player.transform;
        }

        public override void Update()
        {
            base.Update();

            stateTimer = boss.battleTime;

            if (boss.CanBattle())
            {
                return;
            }

            if (player.position.x > boss.transform.position.x)
                moveDir = 1;
            else if (player.position.x < boss.transform.position.x)
                moveDir = -1;

            boss.SetVelocity(boss.moveSpeed * moveDir, rb.velocity.y);
        }

        public override void Exit()
        {
            base.Exit();
        }

        private bool CanAttack()
        {
            if (Time.time >= boss.lastTimeAttacked + boss.attackCooldown)
            {
                boss.attackCooldown = Random.Range(boss.minAttackCooldown, boss.maxAttackCooldown);
                boss.lastTimeAttacked = Time.time;
                return true;
            }

            return false;
        }
    }
}
