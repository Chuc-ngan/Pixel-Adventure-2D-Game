namespace Assets.Scripts.Enemy.Boss
{
    public class BossLaserBeamState : EnemyState
    {
        private BossCharacter boss;
        private float cooldown = 2.5f;
        private int count;
        private bool created;

        public BossLaserBeamState(EnemyCharacter _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            boss = (BossCharacter)_enemyBase;
        }

        public override void Exit()
        {
            base.Exit();

            count++;

            if (count >= 4)
            {
                count = 0;
            }
            created = false;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            if (triggerCalled && !created)
            {
                created = true;
                boss.laserSkill.CreateLaserBeam();
                stateTimer = cooldown;
            }

            if (triggerCalled && stateTimer < 0)
            {
                if (count == 3)
                {
                    rb.gravityScale = 6;
                    boss.laserSkill.TriggerExitSkill();
                    stateMachine.ChangeState(boss.idleState);
                    return;
                }
                stateMachine.ChangeState(boss.laserLoadState);
            }
        }
    }
}
