using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBasket : MonoBehaviour
{
    [SerializeField] private GameObject damaged;
    [SerializeField] private GameObject notDamaged;
    [SerializeField] private GameObject interactButton;
    private CactusPunch cp;

    private void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
