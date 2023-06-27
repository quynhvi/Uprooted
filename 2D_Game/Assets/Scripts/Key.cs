using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool isFollowing;
    public float followSpeed;
    public Transform followTarget;

    private ResourceManagement rm;
    private LightSource ls;

    public GameObject interactButton;
    private bool interactable;

    // Start is called before the first frame update
    void Start()
    {
        rm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<ResourceManagement>();
        ls = FindObjectOfType<LightSource>();
        interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.I))
        {
            CollectKey();
        }
    }

    private void CollectKey()
    {
        if (!isFollowing)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("VFT"))
                {
                    ls.chargedLight = 0.03f;

                    KeyFollowPoint vft = FindAnyObjectByType<KeyFollowPoint>();
                    if (vft != null)
                    {
                        Debug.Log("Colliding with key");
                        followTarget = vft.keyFollowPoint;
                        isFollowing = true;
                        vft.followingKey = this;

                        // Decrease resource levels
                        if (rm != null && ls != null)
                        {
                            rm.lightLevelNumber -= ls.chargedLight;
                            rm.lightBarFill.fillAmount -= ls.chargedLight;
                            rm.waterLevelNumber -= ls.chargedLight;
                            rm.waterBarFill.fillAmount -= ls.chargedLight;
                        }
                        interactable = false;
                        break; // Exit the loop after finding the first VFT
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactable)
        interactButton.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactButton.SetActive(false);
    }
}
