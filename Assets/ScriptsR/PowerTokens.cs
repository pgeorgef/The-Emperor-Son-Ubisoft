using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTokens : MonoBehaviour
{
    public bool healToken = false;
    public bool damageUpToken = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == GameObject.Find("Player 1").gameObject)
        {
            if (healToken)
            {
                if (GameObject.Find("Player 1").GetComponent<Player>().health < 20)
                {
                    GameObject.Find("Player 1").GetComponent<Player>().health = 20;
                    Destroy(gameObject);
                }
            }
            if (damageUpToken)
            {
                GameObject.Find("Player 1").GetComponent<Player>().damage += 1;
                Destroy(gameObject);
            }


        }
    }
}

