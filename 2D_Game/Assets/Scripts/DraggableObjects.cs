using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObjects : MonoBehaviour
{
    public Rigidbody2D _rigidBody;
    public Vector2 _currentPositionObject;
    public GameObject _venusFlyTrap;
    public bool _isDragging = false;
    public Vector3 _offsetDrag = new Vector3(-1, 0, 0); //Distance from Object to VFT
    public float _distance;
    public PlayerMovement _PlayerMovementScript;
    

    // Start is called before the first frame update
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _PlayerMovementScript = GameObject.FindGameObjectWithTag("VFT").GetComponent<PlayerMovement>();
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
        if (Physics2D.Raycast(_rigidBody.position, Vector2.right, 0.5f, LayerMask.GetMask("VFT")))
            return true;
        else
            return false;
    }
    bool CheckLeft()
    {
        if (Physics2D.Raycast(_rigidBody.position, Vector2.left, 0.5f, LayerMask.GetMask("VFT")))
            return true;
        else
            return false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("collision of watering can detected");
        //if object colliding with Venus Fly Trap it is draggable
        if (collision.gameObject.CompareTag("VFT") && Input.GetKey(KeyCode.C))
        {
            Debug.Log("collision with VFT detected");
            _isDragging = true;
        }
    }
    private void MeasuringPlayerPosition()
    {
        //measuring distance between player and object
        _distance = Vector3.Distance(_rigidBody.position, _venusFlyTrap.transform.position);
        Debug.Log(_distance);
        //checking if player is left or right with positive/negative distance
        //raycast left and right 
        if(CheckRight() == true)
            _offsetDrag = new Vector3(1, 0, 0);
        if(CheckLeft() == true)
            _offsetDrag = new Vector3(-1, 0, 0);

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
        if (_distance > 1.5f || Input.GetKeyDown(KeyCode.C))
        {
            _isDragging = false;
            _currentPositionObject = _rigidBody.position;
        }

        if (_isDragging == true)
        {
            _currentPositionObject = _venusFlyTrap.transform.position - _offsetDrag; //Operator needs bugfix
            _rigidBody.MovePosition(_currentPositionObject);
        }
        //if distance too big from draggable object to VFT then _isDragging set to false
        //1. get distance from two objects with 
    }
}
