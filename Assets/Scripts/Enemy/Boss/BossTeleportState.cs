namespace Assets.Scripts.Enemy.Boss
{
    public class BossTeleportState : EnemyState
    {
        private BossCharacter boss;

        public BossTeleportState(EnemyCharacter _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            boss = (BossCharacter)_enemyBase;
        }

        public override void Enter()
        {
            base.Enter();

            boss.stats.MakeInvincible(true);
        }

        public override void Update()
        {
            base.Update();

            if (triggerCalled)
            {
                stateMachine.ChangeState(boss.battleState);
            }
        }

        public override void Exit()
        {
            base.Exit();

            boss.stats.MakeInvincible(false);
        }
    }
}
