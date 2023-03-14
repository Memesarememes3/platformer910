using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_plat_script : MonoBehaviour
{

    public Vector3 startPos;
    public Vector3 targetPos;

    public float moveSpeed;

    public bool movingToTarget;
    public target target;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponentInChildren<target>();
        startPos = transform.position;
        targetPos = target.transform.position;
        movingToTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    public void move()
    {
        if(movingToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            if(transform.position == targetPos)
            {
                movingToTarget = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            if (transform.position == startPos)
            {
                movingToTarget = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
