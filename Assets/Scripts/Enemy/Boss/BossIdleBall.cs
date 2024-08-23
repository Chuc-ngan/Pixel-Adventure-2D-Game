namespace Assets.Scripts.Enemy.Boss
{
    public class BossIdleBall : EnemyState
    {
        private BossCharacter boss;
        public BossIdleBall(EnemyCharacter _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            boss = (BossCharacter)_enemyBase;
        }

        public override void Update()
        {
            base.Update();

            if (triggerCalled)
                stateMachine.ChangeState(boss.fireBallState);
        }
    }
}
