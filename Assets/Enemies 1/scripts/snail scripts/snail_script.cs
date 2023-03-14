using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snail_script : MonoBehaviour
{
    public float moveSpeed;
    public int move_dir;

    private Rigidbody2D rig;
    private Animator animator;


    public bool isDead;
    public bool isActive;
    



    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isActive = false;
        isDead = false;
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void move()
    {
        rig.velocity = new Vector2(moveSpeed * move_dir, rig.velocity.y);
    }

    private void FixedUpdate()
    {
        if (isActive)
        {

            move();
            flip_dir();
        }
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("die", isDead);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.transform.position.y > transform.position.y)
            {
                die();
            }
            else
            {
                collision.gameObject.GetComponent<Player_Controller>().die();
            }
        }
    }
    private void OnBecameVisible()
    {
        isActive = true;
    }

    private void OnBecameInvisible()
    {
        isActive = false;
    }

    public void die()
    {

        isDead = true;
        Destroy(gameObject, .5f);
    }

    private void flip_dir()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * move_dir, .5f);
        if (hit)
        {
            
            if(!hit.collider.gameObject.CompareTag("Player")&& !gameObject)
            {
                move_dir *= -1;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            
        }
        
    }
}
