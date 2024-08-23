using Assets.Scripts.Enemy.Boss.Skills;
using UnityEngine;

namespace Assets.Scripts.Enemy.Boss.SkillControllers
{
    public class BallController : MonoBehaviour
    {
        private CircleCollider2D cd;
        private Player player;
        private BallSkill ballSkill;

        private bool canMove;
        private float moveSpeed = 28;

        private bool canGrow;
        private float growSpeed = 3;
        private float maxSize = 6;

        private float ballTimer;
        private float ballTimeCooldown = 2;

        private Vector2 moveDirection;

        private void Start()
        {
            cd = GetComponent<CircleCollider2D>();
            player = PlayerManager.instance.player;
        }

        private void FireBall()
        {
            canMove = true;
            canGrow = false;
            moveDirection = (player.transform.position - transform.position).normalized;
            ballTimer = ballTimeCooldown;
        }

        private void Update()
        {
            if (canMove)
            {
                transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
                ballTimer -= Time.deltaTime;

                if (ballTimer < 0)
                    FinishFireBallSkill();
            }
            else if (IsMaxSize())
            {
                FireBall();
            }
            else if (canGrow)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
            }
        }

        private bool IsMaxSize()
        {
            return Mathf.Abs(maxSize - transform.localScale.x) <= 1f
                && Mathf.Abs(maxSize - transform.localScale.y) <= 1f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (canMove && (collision.gameObject.layer == LayerMask.NameToLayer("Player")
               || collision.gameObject.layer == LayerMask.NameToLayer("Ground")))
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    player.stats.TakeDamage(60);
                }
                FinishFireBallSkill();
            }
        }

        private void FinishFireBallSkill()
        {
            canMove = false;
            Destroy(gameObject);
        }

        public void Setup(BallSkill _fireBallSkill)
        {
            ballSkill = _fireBallSkill;
            canGrow = true;
        }
    }
}
