using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private float deathTime = 1;
    [SerializeField] private ParticleSystem blood;
    [SerializeField] private PlayerManager player;
    private bool damageCoolDown = false;


    //private void Awake()
    //{
    //    Messenger.AddListener(GameEvent.ENEMY_DEAD, OnEnemyDead);

    //}
    //private void OnDestroy()
    //{

    //    Messenger.RemoveListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
    //}
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
            else
            {
                StartCoroutine(hurtPlayer());
                

            }
        }
    }
    IEnumerator hurtPlayer()
    {
        if (!damageCoolDown)
        {
            Messenger.Broadcast(GameEvent.PLAYER_HURT);
            damageCoolDown = true;
            yield return new WaitForSeconds(1);
            
            damageCoolDown = false;
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
        Messenger.Broadcast(GameEvent.ENEMY_DEAD);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
