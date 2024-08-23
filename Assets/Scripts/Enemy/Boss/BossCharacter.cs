using Assets.Scripts.Enemy.Boss.Skills;
using UnityEngine;

namespace Assets.Scripts.Enemy.Boss
{
    public class BossCharacter : EnemyCharacter
    {
        #region States
        public BossBattleState battleState { get; private set; }
        public BossAttackState attackState { get; private set; }
        public BossIdleState idleState { get; private set; }
        public BossDeadState deadState { get; private set; }
        public BossTeleportState teleportState { get; private set; }
        public BossIdleBall idleBall { get; private set; }
        public BossFireBallState fireBallState { get; private set; }
        public BossDashState dashState { get; private set; }
        public BossLaserIdleState laserIdleState { get; private set; }
        public BossLaserLoadState laserLoadState { get; private set; }
        public BossLaserBeamState laserBeamState { get; private set; }
        public bool CanHealth { get => canHealth; set => canHealth = value; }
        #endregion

        [Header("Teleport details")]
        [SerializeField] private BoxCollider2D arena;
        [SerializeField] private Vector2 surroundingCheckSize;
        public float chanceToTeleport;
        public float defaultChanceToTeleport = 25;

        public Player player;
        public Collider2D hitBox;
        public LayerMask playerLayer;
        private bool canHealth;

        public BallSkill ballSkill;
        public DashSkill dashSkill;
        public LaserSkill laserSkill;

        public bool death;

        protected override void Awake()
        {
            base.Awake();

            idleState = new BossIdleState(this, stateMachine, "Idle");

            battleState = new BossBattleState(this, stateMachine, "Move");
            attackState = new BossAttackState(this, stateMachine, "Attack");

            deadState = new BossDeadState(this, stateMachine, "Idle");

            teleportState = new BossTeleportState(this, stateMachine, "Teleport");
            fireBallState = new BossFireBallState(this, stateMachine, "Ball");
            idleBall = new BossIdleBall(this, stateMachine, "IdleBall");
            dashState = new BossDashState(this, stateMachine, "Dash");
            laserIdleState = new BossLaserIdleState(this, stateMachine, "IdleLaser");
            laserLoadState = new BossLaserLoadState(this, stateMachine, "LaserLoad");
            laserBeamState = new BossLaserBeamState(this, stateMachine, "LaserBeam");

            GameObject skillManagerObject = GameObject.Find("BossSkillManager");

            ballSkill = skillManagerObject.GetComponent<BallSkill>();
            dashSkill = skillManagerObject.GetComponent<DashSkill>();
            laserSkill = skillManagerObject.GetComponent<LaserSkill>();
        }

        protected override void Start()
        {
            base.Start();

            stateMachine.Initialize(idleState);
        }

        protected override void Update()
        {
            base.Update();
        }
        public override void Die()
        {
            base.Die();
            stateMachine.ChangeState(deadState);
        }

        public bool CanTeleport()
        {
            if (Random.Range(0, 100) <= chanceToTeleport)
            {
                chanceToTeleport = defaultChanceToTeleport;
                return true;
            }


            return false;
        }

        public bool CanBattle()
        {
            if (Random.value < .3f)
            {
                if (laserSkill.CanUseSkill())
                {
                    stateMachine.ChangeState(laserIdleState);
                }
            }
            else if (Random.value < .3f)
            {
                if (dashSkill.CanUseSkill())
                {
                    stateMachine.ChangeState(dashState);
                }
            }
            else if (Random.value < .3f)
            {
                if (ballSkill.CanUseSkill())
                {
                    stateMachine.ChangeState(idleBall);
                }
            }
            else if (Physics2D.OverlapBox(hitBox.bounds.center, hitBox.bounds.size, 0, playerLayer))
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(attackState);
                }
                else
                    stateMachine.ChangeState(idleState);

                return true;
            }

            return false;
        }

        private bool CanAttack()
        {
            if (Time.time >= lastTimeAttacked + attackCooldown)
            {
                attackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                lastTimeAttacked = Time.time;
                return true;
            }

            return false;
        }

        public void FindPosition()
        {
            float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
            float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

            transform.position = new Vector3(x, y);
            transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (cd.size.y / 2));

            if (!GroundBelow() || SomethingIsAround())
            {
                FindPosition();
            }
        }

        private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, whatIsGround);
        private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, whatIsGround);

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
            Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
        }

        public void Health(bool _health)
        {
            canHealth = _health;
        }
    }
}
