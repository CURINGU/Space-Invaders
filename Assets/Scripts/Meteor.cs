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
    private int rotationVelocity;

    private EnemySoundFeedback soundFeedback;

    private void Start()
    {
        soundFeedback = FindObjectOfType<EnemySoundFeedback>();
        rotationVelocity = Random.Range(10, 50);
    }

    void Update()
    {
        MoveDown();
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        transform.GetChild(0).Rotate(Vector3.forward, rotationVelocity * Time.deltaTime);
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
            FindObjectOfType<RoundManager>().EnemyDestroyed();
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "playerLaser" || collision.gameObject.tag == "enemyLaser")
        {
            collision.GetComponent<Laser>().Destroy();
            Instantiate(particlesPf, transform.position, transform.rotation);
            TryDropPowerUp();

            if (isDestroiyng == false)
            {
                isDestroiyng = true;
                soundFeedback.PlaySound(EnemySoundType.meteorExplodeSound);
                FindObjectOfType<RoundManager>().EnemyDestroyed();
            }
            
            Destroy(gameObject);
        }
    }
}
