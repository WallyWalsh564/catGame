using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallRespawn : MonoBehaviour
{
    private Vector3 respawnPoint;
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            rb.transform.position = respawnPoint;
        }
    }
}
