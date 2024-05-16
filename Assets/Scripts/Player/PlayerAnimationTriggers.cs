using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttactTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<CharacterStats>().TakeDamage(player.stats.damage.GetValue());
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }

    private void ThrowSword()
    {
        SkillManager.manager.sword.CreateSword();
    }
}
