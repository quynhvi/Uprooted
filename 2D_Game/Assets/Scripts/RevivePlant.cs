using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivePlant : MonoBehaviour
{
    public bool _ivyJustRevived = false;
    private PlayerMovement _IvyMovementScript;
    private PlayerSwap _PlayerSwapScript;
    // Start is called before the first frame update
    void Awake()
    {
        _IvyMovementScript = this.gameObject.GetComponent<PlayerMovement>();
        _PlayerSwapScript = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSwap>();
        _IvyMovementScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckforRevival();
    }
    
    private void OnCollisionStay2D(Collision2D contact)
    {
        // Debug.Log("Collision detected with Ivy");
        if(contact.gameObject.CompareTag("dragable")) // Exchange player characters with dragable object// Input.GetKey(KeyCode.C) && 
        {
            if(Input.GetKey(KeyCode.C))
            {
                // Debug.Log("Interaction and Collision with Ivy detected");
                _ivyJustRevived = true;
            }
            
        }
    }
    public void CheckforRevival()
    {
        //if Ivy got interacted with then Revive Ivy --> Movement possible
        if (_ivyJustRevived == true && !_PlayerSwapScript.possibleCharacters.Contains(this.gameObject.transform))
        {
            _PlayerSwapScript.possibleCharacters.Add(this.gameObject.transform);
            _IvyMovementScript.enabled = true;
        }
    }
}
