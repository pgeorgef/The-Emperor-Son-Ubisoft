using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : Enemy
{
    public GameObject spiderBaby1;
    public GameObject spiderBaby2;
    public GameObject spiderBaby3;
    public GameObject spiderBaby4;
    // Start is called before the first frame update

    public override void Die()
    {
        if (health <= 0)
        {
            animator.SetTrigger("Death");

        }
    }
    public override void Update()
    {
        base.Update();
        detectTop();
    }
    public void DeathComplete()
    {
        deathParticles.gameObject.SetActive(true);
        deathParticles.Emit(10);
        deathParticles.transform.parent = transform.parent;
        GameObject instanceS = Instantiate(spiderBaby1, new Vector3(tr.position.x + 1, tr.position.y, tr.position.z), tr.rotation) as GameObject;
        instanceS.GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 8f), ForceMode2D.Impulse);
        GameObject instanceP = Instantiate(spiderBaby2, new Vector3(tr.position.x + 1, tr.position.y, tr.position.z), tr.rotation) as GameObject;
        instanceP.GetComponent<Rigidbody2D>().AddForce(new Vector2(8f, 8f), ForceMode2D.Impulse);
        GameObject instanceI = Instantiate(spiderBaby3, new Vector3(tr.position.x + 1, tr.position.y, tr.position.z), tr.rotation) as GameObject;
        instanceI.GetComponent<Rigidbody2D>().AddForce(new Vector2(-12f, 8f), ForceMode2D.Impulse);
        GameObject instanceD = Instantiate(spiderBaby4, new Vector3(tr.position.x + 1, tr.position.y, tr.position.z), tr.rotation) as GameObject;
        instanceD.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5f, 8f), ForceMode2D.Impulse);
        Destroy(gameObject);
        Destroy(GameObject.Find("GateFinalBoss"));
    }
    public void detectTop()
    {
                if (CheckRaycast(4f, Vector2.up, toHit).collider != null)
            rb.AddForce(new Vector2(Random.Range(-4, 4), 2), ForceMode2D.Impulse);
    }
}
