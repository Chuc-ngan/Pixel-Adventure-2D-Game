using UnityEngine;

namespace Assets.Scripts.Enemy.Boss.Skills
{
    public enum AbilityState
    {
        Ready, Active, Cooldown
    }

    public class Skill : MonoBehaviour
    {
        [SerializeField] protected float cooldown;
        protected float cooldownTimer;
        protected BossCharacter boss;
        protected Player player;
        protected Collider2D hitBox;

        public AbilityState state;

        protected virtual void Start()
        {
            GameObject skillManagerObject = GameObject.Find("BossSkillManager");

            boss = skillManagerObject.GetComponentInParent<BossCharacter>();
            player = PlayerManager.instance.player;
            hitBox = boss.hitBox;
        }

        protected virtual void Update()
        {
            if (cooldownTimer >= 0 && !IsActive())
            {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer < 0)
                {
                    state = AbilityState.Ready;
                }
            }
        }

        public virtual bool CanUseSkill()
        {
            return state == AbilityState.Ready && cooldownTimer <= 0;
        }

        public virtual void UseSkill()
        {
            state = AbilityState.Active;
        }

        public bool IsCooldown() => state == AbilityState.Cooldown;

        public bool IsActive() => state == AbilityState.Active;
        public virtual void TriggerExitSkill()
        {
            state = AbilityState.Cooldown;
            cooldownTimer = cooldown;
        }
    }
}
