using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{
    // Start is called before the first frame update
    public LayerMask playerMask;
    //public GameObject playerObject;
    float oldTime;
    float ai = 1;
    private float distance;
    private bool player;
    private int moveInvert = 1;
    private float moveInvertTime = 3f;
    private float moveTime = 0.0f;
    private float idle;
    public float distanceAttackRangeDetect;
    public override void Start()
     {
        base.Start();
     }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!animator.GetBool("Attack"))
        {
            //VerticalAttack();
            DetectPlayerTop();
            Die();
            // Patrol();
            if (DetectPlayer(7f))
            {
                distance = tr.position.x - GameObject.Find("Player 1").transform.position.x;
                MoveToPlayer(distance);
                Debug.DrawRay(tr.position, Vector2.right, Color.green);
                DetectWalls();
                if (DetectPlayer(distanceAttackRangeDetect))
                    Attack();
            }
            else
                Patrol();


        }
    }
    bool DetectPlayer(float dim)
    {
        player = Physics2D.OverlapCircle(tr.position,dim,playerMask);

        return player;

    }
    void MoveToPlayer(float distance)
    {
        if (distance > 0)
            Move(-1);
        else
            Move(1);
            
    }
    void DetectWalls()
    {
        Debug.DrawRay(transform.position, Vector2.right, Color.yellow);
        if ( CheckRaycast(2f,Vector2.right,groundLayer).collider != null)
            Jump();

        Debug.DrawRay(transform.position, Vector2.left, Color.green);
        if ( CheckRaycast(2f, Vector2.left, groundLayer).collider != null)
            Jump();


    }
    void Patrol()
    {
        DetectWalls();
        if (Time.time > moveTime )
        {
            moveTime = Time.time + moveInvertTime;
            moveInvert = moveInvert * -1;

            idle = Random.Range(-50f, 100f);
        }
        
        if (idle < 100f)
        {
            Move(0);
        }
        if (idle > 0f)
        {
            Move(moveInvert);
        }
    }
}
