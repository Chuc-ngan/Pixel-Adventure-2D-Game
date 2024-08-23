using UnityEngine;

namespace Assets.Scripts.Enemy.Boss.SkillControllers
{
    public class DashSkillController : MonoBehaviour
    {
        private void FinishDash() => Destroy(gameObject);

        private void Update()
        {
            transform.localScale
                = Vector2.Lerp(transform.localScale, new Vector2(9, 9), 3 * Time.deltaTime);
        }
    }
}
