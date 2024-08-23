using UnityEngine;

namespace Assets.Scripts.Enemy.Boss
{
    public class BossFireBallState : EnemyState
    {
        private BossCharacter boss;
        private float fireBallCoolDown = 1f;
        private float fireBallTimer;
        private int count;

        public BossFireBallState(EnemyCharacter _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            boss = (BossCharacter)_enemyBase;
        }

        public override void Update()
        {
            base.Update();

            fireBallTimer -= Time.deltaTime;

            if (count < 4 && fireBallTimer < 0)
            {
                fireBallTimer = fireBallCoolDown;
                count++;

                boss.ballSkill.UseSkill();
            }

            if (count == 4)
            {
                boss.ballSkill.TriggerExitSkill();
                stateMachine.ChangeState(boss.idleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            count = 0;
        }
    }
}
