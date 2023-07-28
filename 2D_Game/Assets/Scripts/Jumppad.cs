using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumppad : MonoBehaviour
{
    [SerializeField] private GameObject bedDamaged;
    [SerializeField] private GameObject bed;
    public float bounce = 0f;
    public PlatformEffector2D platformEffector;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cactus") || collision.gameObject.CompareTag("VFT") || collision.gameObject.CompareTag("Ivy") || collision.gameObject.CompareTag("AloeVera"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Reset the vertical velocity to zero before applying the upward force
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            }
            if(bedDamaged != null)
                bedDamaged.SetActive(true);
            if (bed != null)
                bed.SetActive(false);

            // Disable the platform effector to allow the character to pass through
            if (platformEffector != null)
            {
                platformEffector.enabled = false;
                Invoke("EnablePlatformEffector", 0.5f); // Re-enable the platform effector after a delay
            }
        }
    }

    private void EnablePlatformEffector()
    {
        if (platformEffector != null)
        {
            platformEffector.enabled = true;
        }
    }
}