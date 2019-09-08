using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnter : MonoBehaviour
{
    public Canvas bossImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D( Collider2D col)
    {
        if (col.gameObject == GameObject.Find("Player 1").gameObject)
        {
            Debug.Log("a intrat in boss");
            // GetComponentInChildren<Canvas>().gameObject.SetActive(true);
            bossImage.enabled = true;
            Time.timeScale = 0.0001f;
            StartCoroutine(timer());
        }
    }
    IEnumerator timer()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameObject.Find("Player 1").gameObject.transform.position += new Vector3(5, 0, 0); 
        Time.timeScale = 1f;
        bossImage.enabled = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
