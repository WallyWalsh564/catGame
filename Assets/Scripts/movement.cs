using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float originSpeed = 6f;
    public float sprintSpeed = 15f;
    public float speed;
    
    private CharacterController cc;
    //spin variable
    public float sensitivityHoriz = 9.0f;
    //gravity variables
    private float gravity = -9.81f;
    private float yVelocity = 0.0f;
    private float yVelocityWhenGrounded = -4.0f; //Y velocity when grounded
    //underwater gravity 
    private float underWaterGravity = -.01f;
    //Jump variables
    private float jumpHeight = 3.0f;
    private float jumpTime = 0.5f;
    private float initialJumpVelocity;
    private bool dblJump = false;
    //some bs with this character model not detecting ground, use this ray casting technique to determine grounded instead
    private float distToGround;

    //attach animator for  state change variables
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        float timeToApex = jumpTime / 2.0f;
        gravity = (-2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);
        distToGround = cc.bounds.extents.y;
        
    }

    bool isTouchGround()
    {
        return cc.isGrounded;
        //return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    // Update is called once per frame
    void Update()
    {
        speed = originSpeed;

        bool isGrounded = isTouchGround();
       // Debug.Log("isgrounded:" + isGrounded);
        anim.SetBool("isGrounded", isGrounded);

        if (isGrounded)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("doubleJump", false);
        }
        //move
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //transform.Translate(movement * Time.deltaTime * speed);
        movement = transform.TransformDirection(movement);
       
        //movement = Vector3.ClampMagnitude(movement, 1.0f);
        
        anim.SetFloat("velocity", movement.magnitude);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            
            StartCoroutine(Attack());
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement = sprint(movement);
        }
        else
        {
            movement *= speed;
            anim.SetBool("sprint", false);
        }
        //gravity
        if (cc.transform.position.y < 0)
        {
            yVelocity = underWaterGravity * Time.deltaTime;
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }
        if (cc.isGrounded && yVelocity < 0.0)
        {
            yVelocity = yVelocityWhenGrounded;
        }
        movement.y = yVelocity;
       // Debug.Log("y vel:" + yVelocity);
        //jump
        
      
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            yVelocity = initialJumpVelocity;
            dblJump = true;
            //anim.SetBool("jumping", true);
            anim.SetTrigger("jump");
        }
        else
        {
            if (Input.GetButtonDown("Jump") && dblJump == true)
            {
                anim.SetTrigger("jump");
                //anim.SetBool("doubleJump", true);
                yVelocity = initialJumpVelocity;
                dblJump = false;
            }
        }
        
        movement.y = yVelocity;

        cc.Move(movement * Time.deltaTime);
        
        //rotate
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivityHoriz);

    }
    private Vector3 sprint(Vector3 movement)
    {
        movement *= sprintSpeed;
        anim.SetBool("sprint", true);
        return movement;
    }
    IEnumerator Attack()
    {
        anim.SetTrigger("attacking");
        anim.SetBool("AttackMode", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("AttackMode", false);
    }
}
