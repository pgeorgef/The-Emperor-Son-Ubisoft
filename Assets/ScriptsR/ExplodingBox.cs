using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExplodingBox : Enemy
{
    public AudioSource explodeSound;
    public override void Start()
    {
        base.Start();
        GameObject SoundExplode = GameObject.FindGameObjectWithTag("SoundExplode");
        explodeSound = SoundExplode.GetComponent<AudioSource>();
        
    }
     void Update()
    {
        Die();
    }
    
    public override void Die()
    {
        //base.Die();
        if (health <= 0)
        {
            Destroy(gameObject);
            explodeSound.Play();
            AttackDetectHit();

        }

    }

}