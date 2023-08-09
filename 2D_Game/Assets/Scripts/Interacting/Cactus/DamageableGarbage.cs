using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableGarbage : MonoBehaviour
{
    [SerializeField] private GameObject notDamaged;
    [SerializeField] private GameObject damaged;

    public void TakeDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Arm touch");
            notDamaged.SetActive(false);
            damaged.SetActive(true);

        }
    }
}
