namespace Assets.Scripts.Enemy.Boss
{
    public class BossLaserIdleState : EnemyState
    {
        private BossCharacter boss;

        public BossLaserIdleState(EnemyCharacter _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            boss = (BossCharacter)_enemyBase;
        }

        public override void Enter()
        {
            base.Enter();

            rb.gravityScale = 0;
            boss.SetVelocity(0, 14);
        }

        public override void Update()
        {
            base.Update();

            if (triggerCalled)
            {
                stateMachine.ChangeState(boss.laserLoadState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            boss.SetVelocity(0, 0);
        }
    }
}
