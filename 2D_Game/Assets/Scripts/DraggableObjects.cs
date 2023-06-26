using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObjects : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 currentPositionObject;
    public GameObject vft;
    public bool isDragging = false;
    public Vector3 offsetDrag = new Vector3(-1, 0, 0); //Distance from Object to VFT
    public float distance;
    public PlayerMovement PlayerMovementScript;
    

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerMovementScript = GameObject.FindGameObjectWithTag("VFT").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        MeasuringPlayerPosition();
        //CheckForPlayerFlip();
        CheckIfDragging();
    }

    bool CheckRight()
    {
        if (Physics2D.Raycast(rb.position, Vector2.right, 0.5f, LayerMask.GetMask("VFT")))
            return true;
        else
            return false;
    }
    bool CheckLeft()
    {
        if (Physics2D.Raycast(rb.position, Vector2.left, 0.5f, LayerMask.GetMask("VFT")))
            return true;
        else
            return false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Debug.Log("collision of watering can detected");
        //if object colliding with Venus Fly Trap it is draggable
        if (collision.gameObject.CompareTag("VFT") && Input.GetKey(KeyCode.C))
        {
            Debug.Log("collision with VFT detected");
            isDragging = true;
        }
    }
    private void MeasuringPlayerPosition()
    {
        //measuring distance between player and object
        distance = Vector3.Distance(rb.position, vft.transform.position);
        //Debug.Log(_distance);
        //checking if player is left or right with positive/negative distance
        //raycast left and right 
        if(CheckRight() == true)
            offsetDrag = new Vector3(1, 0, 0);
        if(CheckLeft() == true)
            offsetDrag = new Vector3(-1, 0, 0);

    }
    private void CheckForPlayerFlip()
    {
        //if (_PlayerMovementScript.isFacingRight == true)
        //    _offsetDrag = new Vector3(-1, 0, 0);
        //if(_PlayerMovementScript.isFacingRight == false)
        //    _offsetDrag = new Vector3(1, 0, 0);
    }
    private void CheckIfDragging()
    {
        if (distance > 1.5f || Input.GetKeyDown(KeyCode.C))
        {
            isDragging = false;
            currentPositionObject = rb.position;
        }

        if (isDragging == true)
        {
            currentPositionObject = vft.transform.position - offsetDrag; //Operator needs bugfix
            rb.MovePosition(currentPositionObject);
        }
        //if distance too big from draggable object to VFT then _isDragging set to false
        //1. get distance from two objects with 
    }
}
