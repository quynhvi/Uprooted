using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingObject : MonoBehaviour
{
    Rigidbody2D rb;

    public Transform player;
    
    

    bool isMoving = false;

    public float moveSpeed;
    public float leftAngle;
    public float rightAngle;

    bool movingClockwise;

    public HingeJoint2D playerJoint;
    public float jumpForce;

    private bool isPlayerOn = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && isPlayerOn)
        {
            Move();
        }

        if (playerJoint != null && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cactus") && !isMoving)
        {
            player = collision.transform;
            isPlayerOn = true;
            player.SetParent(transform);
            playerJoint = player.gameObject.AddComponent<HingeJoint2D>();
            playerJoint.connectedBody = rb;
            isMoving = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cactus") && isMoving)
        {
            isPlayerOn = false;
            player.SetParent(null);
            Destroy(playerJoint);
        }
    }

    public void ChangeMoveDirection()
    {
        if(transform.rotation.eulerAngles.z>rightAngle)
        {
            movingClockwise = false;
        }
        else if (transform.rotation.eulerAngles.z < leftAngle)
        {
            movingClockwise = true;
        }
    }

    public void Move()
    {
        ChangeMoveDirection();

        if (movingClockwise)
        {
            rb.angularVelocity = moveSpeed;
        }
        else
        {
            rb.angularVelocity = -moveSpeed;
        }
    }

    

    
}
