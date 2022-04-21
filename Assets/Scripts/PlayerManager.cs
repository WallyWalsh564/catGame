using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int health;
    private int maxHealth = 5;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject dockSpawn;
    [SerializeField] private ParticleSystem blood;
    [SerializeField] private GameObject player;
    private Vector3 respawnLocation;


    private void Awake()
    {
        Messenger.AddListener(GameEvent.FISHER_DIED, OnFisherDied);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.FISHER_DIED, OnFisherDied);
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
    }

    void OnFisherDied()
    {
        updateRespawn(dockSpawn.transform.position);
    }

    void OnPlayerDead()
    {
        StartCoroutine(DeathExplostion());
    }

    void Start()
    {
        health = maxHealth;
        respawnLocation = dockSpawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -4.9) {
            respawn();
        }
        
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Chicken" || hit.gameObject.tag == "Cow" || hit.gameObject.tag == "Sheep")
        {
 
            Debug.Log("Collision with enemy0");
            if(anim.GetBool("AttackMode") == true){
                Debug.Log("attacked the animal");
            }
        }
        if (hit.gameObject.tag == "Water")
        {
            respawn();
        }
    }
    public bool IsAttacking()
    {
        if (anim.GetBool("AttackMode") == true)
        {
            return true;
        }
        return false;
    }
    private void OnControllerColliderHit(Collision collision)
    {
        
    }
    public void respawn()
    {
        transform.position = respawnLocation;
    }

    public void updateRespawn(Vector3 newLocation)
    {
        respawnLocation = newLocation;

    }

    IEnumerator DeathExplostion()
    {
        yield return new WaitForSeconds(0);
        Instantiate(blood, transform.position, Quaternion.identity);
        Messenger.Broadcast(GameEvent.ENEMY_DEAD);
        player.SetActive(false);
    }
}
