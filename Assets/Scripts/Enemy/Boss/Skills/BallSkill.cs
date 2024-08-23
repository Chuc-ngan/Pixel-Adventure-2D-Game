using Assets.Scripts.Enemy.Boss.SkillControllers;
using UnityEngine;

namespace Assets.Scripts.Enemy.Boss.Skills
{
    public class BallSkill : Skill
    {
        public GameObject ballPrefab;

        public override void UseSkill()
        {
            base.UseSkill();

            GameObject newBall = Instantiate(ballPrefab, hitBox.transform.position, Quaternion.identity);
            BallController controller = newBall.GetComponent<BallController>();
            controller.Setup(this);
        }
    }
}
