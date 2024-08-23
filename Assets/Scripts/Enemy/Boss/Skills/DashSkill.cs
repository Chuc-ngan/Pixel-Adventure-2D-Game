using Assets.Scripts.Enemy.Boss.SkillControllers;
using UnityEngine;

namespace Assets.Scripts.Enemy.Boss.Skills
{
    public class DashSkill : Skill
    {
        public GameObject dashPrefab;

        public override void UseSkill()
        {
            base.UseSkill();

            GameObject newDash = Instantiate(dashPrefab, hitBox.transform.position, Quaternion.identity);
            BallController controller = dashPrefab.GetComponent<BallController>();
        }
    }
}
