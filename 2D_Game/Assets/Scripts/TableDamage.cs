using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableDamage : MonoBehaviour
{
    [SerializeField] private GameObject damaged;
    [SerializeField] private GameObject table;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cactus") || collision.gameObject.CompareTag("VFT") || collision.gameObject.CompareTag("Ivy") || collision.gameObject.CompareTag("AloeVera"))
        {
            Debug.Log("table");
            damaged.SetActive(true);
            table.SetActive(false);
        }
    }
}
