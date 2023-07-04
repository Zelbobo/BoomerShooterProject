using System.Collections;
using UnityEngine;

public class MeleeAttack : AIAttack
{
    [SerializeField] private float attackTiming;

    private bool isAttacking = false;

    public override void AttackPlayer(PlayerStats player)
    {
        if (isAttacking)
        {
            return;
        }

        isAttacking = true;
        enemyAnimations.AttackTrigger();
        StartCoroutine(AnimAttack(player));
        Invoke(nameof(RefreshAttack), attackSpeed);
    }

    private IEnumerator AnimAttack(PlayerStats player)
    {
        yield return new WaitForSeconds(attackTiming);

        player.TakeDamage(damage);
    }

    private void RefreshAttack()
    {
        isAttacking = false;
    }
}
