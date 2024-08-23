using UnityEngine;

namespace Assets.Scripts.Enemy.Boss.SkillControllers
{
    public class LaserSkillController : MonoBehaviour
    {
        private bool isLoad;
        private bool isBeam;
        private Player player;
        private Rigidbody2D rb;

        private void Start()
        {
            player = PlayerManager.instance.player;
            rb = GetComponent<Rigidbody2D>();
        }

        public void LaserLoad()
        {
            isLoad = true;
        }

        public void LaserBeam()
        {
            isBeam = true;
            rb.velocity = new Vector2(0, -10);
        }

        private void Update()
        {
            if (isLoad)
            {
                if (IsMaxSize(6))
                    Destroy(gameObject, .5f);

                transform.localScale =
                    Vector2.Lerp(transform.localScale, new Vector2(6, 6), 3 * Time.deltaTime);
            }
            else if (isBeam && !IsMaxSize(8))
            {
                transform.localScale =
                    Vector2.Lerp(transform.localScale, new Vector2(8, 8), 4 * Time.deltaTime);
            }
        }

        private bool IsMaxSize(float maxSize)
        {
            return Mathf.Abs(maxSize - transform.localScale.x) <= 1f
                && Mathf.Abs(maxSize - transform.localScale.y) <= 1f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((collision.gameObject.layer == LayerMask.NameToLayer("Player")
               || collision.gameObject.layer == LayerMask.NameToLayer("Ground")))
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    player.stats.TakeDamage(100);
                    Destroy(gameObject, .5f);
                }
            }
        }
    }
}
