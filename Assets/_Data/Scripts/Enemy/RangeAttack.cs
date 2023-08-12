using System.Collections;
using UnityEngine;

public class RangeAttack : AIAttack
{
    [SerializeField] private float attackTiming;

    #region [PrivateVars]

    private bool isAttacking = false;
    private bool isCanAttack = false;

    private Coroutine checkingIsCanAttack;

    private RangeEnemyAnimations rangeAnimations;

    #endregion

    private void Start()
    {
        rangeAnimations = GetComponent<RangeEnemyAnimations>();
    }

    public override void AttackPlayer(CreatureStats player)
    {
        if (isAttacking)
        {
            return;
        }

        isCanAttack = true;
        isAttacking = true;
        StartCoroutine(AnimAttack(player));
        Invoke(nameof(RefreshAttack), attackSpeed);
    }

    private IEnumerator AnimAttack(CreatureStats player)
    {
        if (checkingIsCanAttack != null)
        {
            StopCoroutine(checkingIsCanAttack);
        }

        checkingIsCanAttack = StartCoroutine(CheckIsCanAttack(player));

        //Prepare attack
        rangeAnimations.SetPrepareAttack(true);

        yield return new WaitForSeconds(attackTiming);

        if (!isCanAttack)
        {
            rangeAnimations.SetPrepareAttack(false);
            yield break;
        }

        rangeAnimations.AttackTrigger();
        player.TakeDamage(damage);
        rangeAnimations.SetPrepareAttack(false);
    }

    private IEnumerator CheckIsCanAttack(CreatureStats player)
    {
        while (true)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > attackRange)
            {
                isCanAttack = false;
                break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void RefreshAttack()
    {
        isAttacking = false;
    }

    public override bool IsCanAttack(Transform target)
    {
        return Vector3.Distance(target.position, transform.position) <= attackRange; 
    }
}
