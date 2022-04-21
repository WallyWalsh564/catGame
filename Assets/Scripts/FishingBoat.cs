using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBoat : MonoBehaviour
{

    //[SerializeField] private Rigidbody rb;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] Rigidbody rb;
    [SerializeField] CharacterController cc;
    public float speed;
    private Vector3 newPos;
    private int currWaypointIndex = 0;
    private bool moveit = false;

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
        moveit = true;
    }
    void Start()
    {
       // rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveit)
        {
            //rb.MovePosition(newPos);
            MoveBoat();
        }
    }
    void MoveBoat()
    {
        float step = speed * Time.deltaTime;
        //Vector3 newPos = new Vector3(transform.position, waypoints[currWaypointIndex].position, step);
        //rb.MovePosition(Vector3.MoveTowards(transform.position, waypoints[currWaypointIndex].position, step));
        //rb.Move(waypoints[currWaypointIndex].position);
        //Debug.Log("why doesnt it move");



       
        Vector3 newPos = Vector3.MoveTowards(transform.position, waypoints[currWaypointIndex].position, step);

        //moveit = true;
        //  rb.MovePosition(newPos);
        rb.MovePosition(newPos);
        cc.Move(rb.velocity * Time.deltaTime);



    }
}
