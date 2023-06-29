using UnityEngine;

public class AIStats : CreatureStats
{
    protected override void Death()
    {
        base.Death();

        Destroy(gameObject);
    }
}
