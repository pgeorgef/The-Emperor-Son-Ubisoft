using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBabyEnemy : Enemy
{
    // Start is called before the first frame update
    public override void Update()
    {
        base.Update();
        VerticalAttack();
    }
    public override void Die()
    {
        if (health <= 0)
        {
            animator.SetTrigger("Death");

        }
    }
    public void DeathComplete()
    {
        deathParticles.gameObject.SetActive(true);
        deathParticles.Emit(10);
        deathParticles.transform.parent = transform.parent;
        Destroy(gameObject);
    }
}
