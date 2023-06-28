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

    public Transform hitTransform;

    bool movingClockwise;

    public HingeJoint2D playerJoint;
    public float jumpForce;

    private bool isPlayerOn = false;
    public float waitTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        waitTimer += Time.deltaTime;

        if (isMoving && isPlayerOn)
        {
            Move();
        }
        if (isPlayerOn)
        {
            player.transform.position = hitTransform.position;
        }
        else
        {
            moveSpeed = 0f;
            Move();
        } 
    }

    private void Update()
    {
        if (playerJoint != null && Input.GetKeyDown(KeyCode.Space))
        {
            player.transform.SetParent(null);
            Destroy(playerJoint);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isMoving = false;
            isPlayerOn = false;
            waitTimer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cactus") && !isMoving && waitTimer > 0.5f)
        {
            Debug.Log("Yeet");
            hitTransform.position = player.transform.position;
            player = collision.transform;
            isPlayerOn = true;
            playerJoint = player.gameObject.AddComponent<HingeJoint2D>();
            playerJoint.connectedBody = rb;
            isMoving = true;
        }
    }

    public void ChangeMoveDirection()
    {
        if (transform.rotation.eulerAngles.z > rightAngle)
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
