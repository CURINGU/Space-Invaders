using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float moveSpeed;
    public GameObject particlesPf;
    public bool isDestroiyng = false;

    public GameObject[] powerUps;
    public float dropChance = 0.1f;

    private EnemySoundFeedback soundFeedback;

    private void Start()
    {
        soundFeedback = FindObjectOfType<EnemySoundFeedback>();
    }

    void Update()
    {
        MoveDown();
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    private void TryDropPowerUp()
    {
        float randomValue = Random.value; // Valor aleatório entre 0 e 1
        if (randomValue <= dropChance)
        {
            // Escolher um power-up aleatoriamente
            int randomIndex = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[randomIndex], transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "limit")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "playerLaser" || collision.gameObject.tag == "enemyLaser")
        {
            collision.GetComponent<Laser>().Destroy();
            Instantiate(particlesPf, transform.position, transform.rotation);
            FindObjectOfType<RoundManager>().EnemyDestroyed();
            TryDropPowerUp();

            if (isDestroiyng == false)
            {
                isDestroiyng = true;
                soundFeedback.PlaySound(EnemySoundType.meteorExplodeSound);
            }
            
            Destroy(gameObject);
        }
    }
}
