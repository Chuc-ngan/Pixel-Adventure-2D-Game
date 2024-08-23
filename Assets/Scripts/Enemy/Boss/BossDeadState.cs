using UnityEngine;

namespace Assets.Scripts.Enemy.Boss
{
    public class BossDeadState : EnemyState
    {
        private BossCharacter boss;
        private int count;
        private float healthCooldown = .15f;
        private float healthTimer;

        public BossDeadState(EnemyCharacter _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
        {
            boss = (BossCharacter)_enemyBase;
        }

        public override void Enter()
        {
            base.Enter();

            if (count < 1)
            {
                healthTimer = healthCooldown;
                boss.anim.SetBool("Health", true);
                count++;
                return;
            }

            boss.anim.SetBool(boss.lastAnimBoolName, true);
            boss.anim.speed = 0;
            boss.cd.enabled = false;

            stateTimer = .15f;
        }

        public override void Update()
        {
            base.Update();

            healthTimer -= Time.deltaTime;

            if (boss.CanHealth && healthTimer <= 0)
            {
                healthTimer = healthCooldown;

                boss.stats.IncreaseHealthBy(Mathf.RoundToInt(boss.stats.maxHealth.GetValue() * .3f));
            }
            else if (triggerCalled)
            {
                boss.anim.SetBool("Health", false);
                stateMachine.ChangeState(boss.idleState);
            }

            if (stateTimer > 0)
                rb.velocity = new Vector2(0, 10);
        }

        public override void Exit()
        {
            base.Exit();

            boss.death = true;
        }
    }
}
