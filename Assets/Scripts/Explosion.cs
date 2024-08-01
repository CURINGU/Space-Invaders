using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private bool takedDamage = false;

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!takedDamage)
            {
                //collision.GetComponent<EnemyHealthBar>().TakeDamage(damage);
                takedDamage = true;
            }

        }
    }
}
