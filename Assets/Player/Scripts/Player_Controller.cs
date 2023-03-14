using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    public bool debug;
    public bool isdead;//for animations

    [Header("Movment Vars")]
    public float moveSpeed;
    public float input_x;
    public int facing_dir;
    public bool isWalking;//for animations

    [Header("jumping Vars")]
    public float jumpForce;
    public int max_jumps;
    public int jump_count;
    public bool jumping;

    [Header("Components")]
    [SerializeField]
    private Rigidbody2D rig;
    [SerializeField]
    private Animator animator;



    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        facing_dir = 1;
        max_jumps = 2;
        
    }

    private void FixedUpdate()
    {
        // Movment left right
        move();


    }



    // Update is called once per frame
    void Update()
    {
        //animation states
        animator.SetBool("jumping", jumping);
        animator.SetBool("walking", isWalking);
        animator.SetBool("die", isdead);


        //reseting jumps
        if (rig.velocity.y <= 0 )
        {
            if(isGrounded())
            {
                jump_reset();
            }
        }

        //checking if jump key pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
            jump();
        }


        if (debug)
        {
            Debug.DrawRay(transform.position, Vector2.down*10f, Color.red);
            Debug.DrawRay(new Vector2(transform.position.x+.20f,transform.position.y), Vector2.down * 10f, Color.red);
            Debug.DrawRay(new Vector2(transform.position.x - .20f, transform.position.y), Vector2.down * 10f, Color.red);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    //action functions
    public void jump()
    {
        if(jump_count > 0)
        {
            jumping = true;
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump_count--;
        }
    }
    public void move()
    {
        input_x = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(moveSpeed * input_x, rig.velocity.y);

        if(input_x != 0 && !jumping)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }


        //flipping sprite
        if (input_x >= 0)
        {
            facing_dir = 1;
        }

        else
        {
            facing_dir = -1;
        }
        transform.localScale = new Vector3(facing_dir, transform.localScale.y, transform.localScale.z);
    }
    public void crouch()
    { }
    public void shoot()
    { }
    public void die()
    {
        isdead = true;
        Destroy(gameObject, 1);
    }





    //utility functions
    public bool isGrounded()
    {
        RaycastHit2D hit_C = Physics2D.Raycast(transform.position,Vector2.down,.05f);
        RaycastHit2D hit_L = Physics2D.Raycast(new Vector2(transform.position.x - .20f, transform.position.y), Vector2.down, .05f);
        RaycastHit2D hit_R = Physics2D.Raycast(new Vector2(transform.position.x - .20f, transform.position.y), Vector2.down, .05f);
        if (hit_C || hit_R||hit_L)
        {
            jump_count = max_jumps;
            return true;
        }
        return false;
        
    }
    public void jump_reset()
    {
        jump_count = max_jumps;
        jumping = false;
    }

}
