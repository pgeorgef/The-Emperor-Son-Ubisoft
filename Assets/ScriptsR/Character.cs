using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;


public class Character : MonoBehaviour
{
    public float health;
    public float maxhealth;
    public int speed;
    public int damage;
    public int jumpheight;
    public LayerMask groundLayer;
    [SerializeField]
    private GameObject graphic;
    [SerializeField]
    public Animator animator;
    private Vector3 origLocalScale;
    private float scale;
    public Transform tr;
    public Rigidbody2D rb;
    bool landed;
    public Transform attackPos;
    public float attackRange;
    public LayerMask toHit;
    Vector3 characterScale;
    float characterScaleX;
    public ScriptableObject script;
    public float distanceHit; // de retinut, Bogdan mi-a zis sa-l fac public
    private int direction = 1;
    private int teleporting = 0;
    public float orientation;
    public AudioSource soundManager;
    public AudioClip hitSound;
    public AudioClip hurtSound;
    public AudioClip jump;
    public ParticleSystem deathParticles;

    Image[] healthBar;
    // Start is called before the first frame update
    public virtual void Start()
    {
        scale = transform.localScale.y;
        soundManager = GetComponent<AudioSource>();
        healthBar = this.GetComponentsInChildren<Image>();
        rb = GetComponent<Rigidbody2D>();
        characterScale = this.transform.localScale;
        characterScaleX = characterScale.x;
    }

    public virtual void Update()
    {
        healthBar[1].fillAmount = health / maxhealth;
      //  Debug.Log(healthBar[1].fillAmount);
       // Debug.Log(health + " " + maxhealth);
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        /*  if (rb.velocity.y < 0 && !CheckRaycast(1f,Vector2.down, groundLayer))

              if (CheckRaycast(2f,Vector2.down, groundLayer))
                  animator.SetTrigger("Land");*/
        /*   if (teleporting == 1)
           {
               SlashAttack();

           }*/
    }

    public RaycastHit2D CheckRaycast(float distance, Vector2 orientation, LayerMask layer)
    {
        //bool grounded;
        Debug.DrawRay(this.transform.position, Vector2.down, Color.red);

        RaycastHit2D ground = Physics2D.Raycast(this.transform.position, orientation, distance, layer);

        /* if (ground.collider != null)
         {
             return true;
            // Debug.Log("atinge");
         }
         else
         {
             //Debug.Log("atingen't");
             return false;
         }*/
        return ground;
    }

    public void Jump()
    {
        // Debug.Log("in jump");
        if (CheckRaycast(2f, Vector2.down, groundLayer).collider != null)
        {
            soundManager.clip = jump;
            soundManager.Play();
            landed = true;
            //  Debug.Log("Suntem pe pamant");
            animator.SetBool("Jump", true);

        }
    }
    public void JumpComplete()
    {
        rb.AddForce(new Vector2(0, jumpheight), ForceMode2D.Impulse);
        animator.SetBool("Jump", false);
        landed = false;

    }

    public void Move(float inputx)
    {
        if (inputx != 0)
            animator.SetBool("Run", true);
        else
            animator.SetBool("Run", false);
        Vector2 move = new Vector2(inputx, 0);
        if (inputx > 0)
        {
            characterScale.x = characterScaleX;
            direction = 1;
        }
        else
        {
            characterScale.x = -characterScaleX;
            direction = -1;
        }
        transform.localScale = characterScale;

        rb.transform.Translate(new Vector3(inputx * Time.fixedDeltaTime * speed, 0, 0));



    }
    public void Attack()
    {
        animator.SetBool("Attack", true);
    }
    public void AttackComplete()
    {
        animator.SetBool("Attack", false);
    }
    public void AttackDetectHit()
    {
        soundManager.clip = hitSound;
        soundManager.Play();
        Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, toHit);
        for (int i = 0; i < enemiesToHit.Length; i++)
        {



            distanceHit = gameObject.transform.position.x - enemiesToHit[i].gameObject.transform.position.x;

            // Debug.Log(direction + "orientare");
            if (enemiesToHit[i].gameObject.CompareTag("Inamic"))
            {

                //distanceHit = enemiesToHit[i].gameObject.GetComponent<Enemy>().transform.position.x;
                //  distanceHit = tr.position.x - distanceHit;
                // Debug.Log(distanceHit + " distanta");
                enemiesToHit[i].gameObject.GetComponent<Enemy>().Hit(distanceHit, damage);
                //  HealthBarScript.health -= damage;
                // Debug.Log("Ia damage");
            }
            if (enemiesToHit[i].gameObject.CompareTag("Playerg"))
            {
                //distanceHit = enemiesToHit[i].gameObject.GetComponent<Player>().transform.position.x;
                //distanceHit = tr.position.x - distanceHit;
                enemiesToHit[i].gameObject.GetComponent<Player>().Hit(distanceHit, damage);
                //   HealthBarScript.health -= damage;
                // Debug.Log("Player ia damage");
            }
            if (enemiesToHit[i].gameObject.CompareTag("Box"))
                enemiesToHit[i].gameObject.GetComponent<ExplodingBox>().Hit(distanceHit, damage);
            // To do, enemy damage cutie 
        }
    }
    /* public void SlashAttack()
     {
         Debug.Log("in detectie");
         Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, toHit);
         for (int i = 0; i < enemiesToHit.Length; i++)
         {

             Debug.Log("Ia damage");
             if (enemiesToHit[i].gameObject.CompareTag("Inamic"))
             {

                 distanceHit = enemiesToHit[i].gameObject.GetComponent<Enemy>().transform.position.x;
                 distanceHit = tr.position.x - distanceHit;
                 enemiesToHit[i].gameObject.GetComponent<Enemy>().Hit(distanceHit, damage);
                 break;
                 // Debug.Log("Ia damage");
             }

         }
     }*/
    public void VerticalAttack()
    {
        if (CheckRaycast(1f, Vector2.up, toHit).collider != null)
        {
            Hit(Random.Range(-1, 1), damage);
            GameObject.Find("Player 1").GetComponent<Player>().rb.AddForce(new Vector2(1f, 1f), ForceMode2D.Impulse);
            // Debug.Log("intra");
        }
    }
    public void DetectPlayerTop()
    {
        if (CheckRaycast(3f, Vector2.down, toHit).collider != null)
            rb.AddForce(new Vector2(Random.Range(-4, 4), 0), ForceMode2D.Impulse);
        if (CheckRaycast(3f, Vector2.up, toHit).collider != null)
            rb.AddForce(new Vector2(Random.Range(-4, 4), 0), ForceMode2D.Impulse);
    }
    public virtual void Die()
    {
        if (health <= 0)
        {
            deathParticles.gameObject.SetActive(true);
            deathParticles.Emit(10);
            deathParticles.transform.parent = transform.parent;
            Destroy(gameObject);
        }
    }
    public void Hit(float distancehit, int damageAttack)
    {
        soundManager.clip = hurtSound;
        soundManager.Play();
        teleporting = 0;
        health = health - damageAttack;

        if (distancehit < 0)
        {
            rb.AddForce(new Vector2(6, 3), ForceMode2D.Impulse);
        }
        if (distancehit > 0)
        {
            rb.AddForce(new Vector2(-6, 3), ForceMode2D.Impulse);


        }
        animator.SetBool("Hurt", true);

        //  if (distancehit < 0)
        //   rb.AddForce(new Vector2(13, 5), ForceMode2D.Impulse);
        //tr.position.x = transform.position
        //rb.velocity = new Vector2(13, 5);
        //else
        //   rb.AddForce(new Vector2(-13,5), ForceMode2D.Impulse);
        //rb.velocity = new Vector2(-13, 5);


    }
    public void HitComplete()
    {
        animator.SetBool("Hurt", false);
    }
    public void Teleport()
    {
        if (direction == 1)
        {
            RaycastHit2D obj = CheckRaycast(5f, Vector2.right, groundLayer);
            if (obj.collider == null)
                this.transform.position += new Vector3(5, 0, 0);
            else
                this.transform.position += new Vector3(obj.distance, 0, 0);
        }
        if (direction == -1)
        {
            RaycastHit2D obj = CheckRaycast(5f, Vector2.left, groundLayer);
            if (obj.collider == null)
                this.transform.position -= new Vector3(5, 0, 0);
            else
                this.transform.position -= new Vector3(obj.distance, 0, 0);
        }
        // rb.velocity = new Vector2(13000, 5);
        /*
                rb.AddForce(new Vector2(500 * direction, 2), ForceMode2D.Force);
                teleporting = 1;
                if (!CheckRaycast(5f, Vector2.left, groundLayer))
                  this.transform.position -= new Vector3(5, 0, 0);*/


    }
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}