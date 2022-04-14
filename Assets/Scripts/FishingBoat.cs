using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBoat : MonoBehaviour
{

    //[SerializeField] private Rigidbody rb;
    [SerializeField] private Transform[] waypoints;
    public float speed;
    private int currWaypointIndex = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        Messenger.AddListener(GameEvent.FISHER_DIED, OnFisherDied);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.FISHER_DIED, OnFisherDied);
    }

    void OnFisherDied()
    {
        Debug.Log("THE FISHER DIED FROM BOAT SCRIPT");
        MoveBoat();
    }
    void Start()
    {
       // rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MoveBoat()
    {
        float step = speed * Time.deltaTime;
        Vector3 newPos = Vector3.MoveTowards(transform.position, waypoints[currWaypointIndex].position, step);
    }
}
