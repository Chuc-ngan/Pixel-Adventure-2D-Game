using UnityEngine;

namespace Assets.Scripts.Enemy.Boss
{
    public class BossIdleState : EnemyState
    {
        private BossCharacter boss;
        private Transform player;

        public BossIdleState(EnemyCharacter _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            boss = (BossCharacter)_enemyBase;
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = boss.idleTime;
            player = PlayerManager.instance.player.transform;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer < 0)
                stateMachine.ChangeState(boss.battleState);
        }
    }
}
