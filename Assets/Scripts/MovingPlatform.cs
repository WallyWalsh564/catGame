using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private bool waitForPlayer;
    [SerializeField] private bool DontStopAtWayPoint;
    [SerializeField] private GameObject platform;

    private CharacterController cc;

    public float speed = 3.0f;
    private int currWaypointIndex = 0;

    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (!waitForPlayer && paused == false)
        {
            MovePlatform();
            if (transform.position == waypoints[currWaypointIndex].position)
            {
                DetermineNextWaypoint();
            }
        }
        else
        {
            if (cc && paused == false)
            {
                MovePlatform();
                if (transform.position == waypoints[currWaypointIndex].position)
                {
                    DetermineNextWaypoint();
                }
            }
        }
    }
    void DetermineNextWaypoint()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (transform.position == waypoints[i].position)
            {

                StartCoroutine(changeDirection());

                //var damping:int = 2;

                if (currWaypointIndex == waypoints.Length - 1)
                {
                    currWaypointIndex = 0;
                    break;
                }
                else
                {
                    currWaypointIndex++;
                    break;
                }
            }
        }
    }
    void MovePlatform()
    {
        float step = speed * Time.deltaTime;
        Vector3 newPos = Vector3.MoveTowards(transform.position, waypoints[currWaypointIndex].position, step);
        if (paused == false)
        {
            rb.MovePosition(newPos);
            if (cc)
            {
                cc.Move(rb.velocity * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cc = other.GetComponent<CharacterController>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cc = null;
        }
    }
    private IEnumerator changeDirection()
    {
        lookAt();
        if (!DontStopAtWayPoint)
        {
            paused = true;

            yield return new WaitForSeconds(1);
            paused = false;
        }
    }
    private void lookAt()
    {
        
           // var rotation = Quaternion.LookRotation(waypoints[currWaypointIndex+1].position - transform.position);
           // platform.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
       // transform.Rotate(Vector3.up * 100);
       if(currWaypointIndex == waypoints.Length - 1)
        {

            transform.LookAt(waypoints[0].position);
            transform.Rotate(Vector3.up * 90);

        }
        else
        {
            transform.LookAt(waypoints[currWaypointIndex + 1].position);
            transform.Rotate(Vector3.up * -90);
   
        }
        //Debug.Log(currWaypointIndex);
//        Debug.Log(waypoints[currWaypointIndex + 1].position);

    }
}
