namespace Assets.Scripts.Enemy.Boss
{
    public class BossDashState : EnemyState
    {
        private BossCharacter boss;
        private Player player;
        private int count;
        public BossDashState(EnemyCharacter _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            boss = (BossCharacter)_enemyBase;
        }

        public override void Exit()
        {
            base.Exit();
            count = 0;
        }

        public override void Enter()
        {
            base.Enter();
            player = PlayerManager.instance.player;
        }

        public override void Update()
        {
            base.Update();

            if (player.transform.position.x > boss.transform.position.x && boss.facingDir == -1)
                boss.Flip();
            else if (player.transform.position.x < boss.transform.position.x && boss.facingDir == 1)
                boss.Flip();

            if (triggerCalled)
            {
                triggerCalled = false;

                if (count < 3)
                {
                    count++;
                }

                if (count == 3)
                {
                    boss.dashSkill.TriggerExitSkill();
                    stateMachine.ChangeState(boss.idleState);
                }
            }
        }
    }
}
