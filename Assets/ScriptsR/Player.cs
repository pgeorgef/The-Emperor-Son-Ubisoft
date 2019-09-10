using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XInputDotNetPure;


public class Player : Character
{
    public CameraEffects cameraEffect;
    bool canDoubleJump;
    public float teleportCooldown;
    private float teleportTime = 0.0f;
    private float nextFire = 0.0f;
    private float specialStart;
    public float specialCooldown;
    public float timeBetweenAttack;
    private float teleportBarTime;
    public GameObject rangedBullet;
    Image[] TeleportBar;
    int cnt = 0;
    // Start is called before the first frame update


    // Update is called once per frame
    public override void Start()
    {
        base.Start();
        ScoreScript.scoreValue = 0;
        TeleportBar = GetComponentsInChildren<Image>();
        teleportBarTime = teleportCooldown;
            
    }
    public override void Update()
    {
        base.Update();
        if (teleportBarTime != teleportCooldown)
            teleportBarTime += Time.deltaTime;
       // Debug.Log(TeleportBar[3].fillAmount);
        TeleportBar[3].fillAmount = teleportBarTime / teleportCooldown;
        if (animator.GetBool("Hurt"))
        {
            cameraEffect.Shake(100, 1);
            GamePad.SetVibration(0, 2f, 2f);
            StartCoroutine(timerVibrate());
        }
        Die();
        if (Input.GetButtonDown("Space"))
        {
            
            if (Input.GetButtonDown("Space") && (CheckRaycast(1f,Vector2.down,groundLayer).collider == null) && cnt == 0 )
            {
                if (canDoubleJump == true)
                {
                    cnt++;
                    rb.velocity = new Vector2(0, 0);
                    rb.AddForce(new Vector2(0, jumpheight - 3), ForceMode2D.Impulse);
                    canDoubleJump = false;
                    soundManager.clip = jump;
                    soundManager.Play();

                }
                //Jump();

            }
            //  

            Jump();

        }
        if (Input.GetButtonDown("Teleport") && Time.time > teleportTime)
        {
            gameObject.GetComponentInChildren<TrailRenderer>().emitting = true;
            Teleport();
            teleportTime = Time.time + teleportCooldown;
            teleportBarTime = 0;
           // Debug.Log(rb.velocity.y);
            //if (rb.velocity.y == 0)
            //StartCoroutine(teleportBarTimer());
            StartCoroutine(timer());
            
        }
        if (CheckRaycast(1f,Vector2.down,groundLayer).collider != null)
        {
            canDoubleJump = true;
            cnt = 0;
        }
        if (Input.GetAxis("Horizontal")  > 0.2f || Input.GetAxis("Horizontal") < -0.2f)
        { Move(Input.GetAxis("Horizontal"));
            //animator.SetBool("Run", true);
        }
        else
            animator.SetBool("Run", false);
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + timeBetweenAttack;
            
            Attack();
        }
       /* if (Input.GetMouseButtonDown(1))
        {
          //  Debug.Log("da ba");
            //Rigidbody2D rangedBullet;
            
            //rangedBullet = Instantiate(Enemy);
            GameObject instantiere = Instantiate(rangedBullet, new Vector3(tr.position.x + 1, tr.position.y,tr.position.z) , tr.rotation) as GameObject;
            //instantiere.AddComponent<Rigidbody2D>();
            instantiere.GetComponent<Rigidbody2D>().AddForce(new Vector2(100f, 0f),ForceMode2D.Impulse);




        } To do ranged attack*/
        /*   if (Input.GetButtonDown("Special Attack"))
           {
               if (Input.GetButtonUp("Special Attack") )
               {
                   specialStart = Time.time + specialCooldown;
                   AttackDetectHit();
               }
           } to do */
    }
    IEnumerator timer()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponentInChildren<TrailRenderer>().emitting = false;
    }
    IEnumerator timerVibrate()
    {
        yield return new WaitForSeconds(0.5f);
        GamePad.SetVibration(0, 0f, 0f);
    }
    public override void Die()
    {
        base.Die();
        if (health <= 0)
        {
            GamePad.SetVibration(0, 0f, 0f);
            Debug.Log("da");
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "EndlessMode")
                SceneManager.LoadScene("DeathMenuNorma");
            else
                SceneManager.LoadScene("DeathMenu");

        }
    }
}
