using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    // Start is called before the first frame update
    private int health;
    private int maxHealth = 5;
    [SerializeField] private Animator anim;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Chicken")
        {
            Debug.Log("Collision with enemy0");
        }
    }
}

