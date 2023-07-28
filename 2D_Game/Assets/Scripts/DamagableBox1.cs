using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableBox1 : MonoBehaviour
{
    [SerializeField] private GameObject damaged;
    [SerializeField] private GameObject notDamaged;

    public void TakeDamage()
    {
        //spriteRenderer.sprite = damageSprite;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Arm touch");
            notDamaged.SetActive(false);
            damaged.SetActive(true);

        }
    }
}
