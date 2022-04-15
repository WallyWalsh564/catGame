using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private float deathTime = 1;
    [SerializeField] private ParticleSystem blood;
    [SerializeField] private PlayerManager player;

    // Start is called before the first frame update
    void Start()
    {
//        StartCoroutine(DeathExplostion());
    }
    private void OnTriggerStay(Collider other)
    {
 
        PlayerManager player = other.GetComponent<PlayerManager>();
        if (player != null)
        {
            Debug.Log("ots");
            if (player.IsAttacking() == true)
            {
                StartCoroutine(DeathExplostion());
            }
        }
    }
    //private void OnTriggerEnter2(Collider other)
    //{
    //    PlayerManager player = other.GetComponent<PlayerManager>();
    //    if (player != null)
    //    {
    //        if(player.IsAttacking() == true)
    //        {
    //            StartCoroutine(DeathExplostion());
    //        }
    //    }
    //}
    

    //private void OnCollisionEnter(Collision collision)
    //{

    //    if (collision.gameObject.tag == "Player")
    //    {
            
    //        if(player.IsAttacking() == true)
    //        {
    //            StartCoroutine(DeathExplostion());
    //        }
            
    //    }
    //}

    IEnumerator DeathExplostion()
    {
        yield return new WaitForSeconds(0);
        Instantiate(blood, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
