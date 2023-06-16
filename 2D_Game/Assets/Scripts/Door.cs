using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private KeyFollowPoint vft;
    public GameObject door;
    public GameObject ivyInteract;

    // Start is called before the first frame update
    void Start()
    {
        vft = FindAnyObjectByType<KeyFollowPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "VFT")
        {
            if (vft.followingKey != null)
            {
                vft.followingKey.followTarget = transform; // door will now be the followTarget
                door.SetActive(false);
                ivyInteract.SetActive(true);
            }
        }
    }
}
