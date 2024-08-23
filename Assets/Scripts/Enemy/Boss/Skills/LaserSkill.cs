using Assets.Scripts.Enemy.Boss.SkillControllers;
using UnityEngine;

namespace Assets.Scripts.Enemy.Boss.Skills
{
    public class LaserSkill : Skill
    {
        public GameObject laserLoadPrefab;
        public GameObject laserBeamPrefab;

        public void CreateLaserLoad()
        {
            GameObject newLaserLoad = Instantiate(laserLoadPrefab, hitBox.transform.position, Quaternion.identity);
            LaserSkillController controller = newLaserLoad.GetComponent<LaserSkillController>();
            controller.LaserLoad();
        }

        public void CreateLaserBeam()
        {
            GameObject newLaserBeam = Instantiate(laserBeamPrefab, hitBox.transform.position, Quaternion.identity);
            LaserSkillController controller = newLaserBeam.GetComponent<LaserSkillController>();
            controller.LaserBeam();
        }
    }
}
