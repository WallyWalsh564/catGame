using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int health;
    private int maxHealth = 5;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject dockSpawn;    
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

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
        transform.position = dockSpawn.transform.position;
    }
}
