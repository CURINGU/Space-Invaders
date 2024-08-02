using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject explosionPf;
    private EnemySoundFeedback soundFeedback;
    private bool isDestroiyng = false;

    [Header("Movement")]
    private Transform player;
    public Transform renderObject;
    public float moveSpeed = 2f;
    public bool isOnScreen;

    [Header("Weapons")]
    public Transform[] weaponTransforms;
    public float shootInterval = 0.5f;
    public float stopShootingDistance = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public bool canShoot = false;

    [Space(20)]
    private Pontuation pontuation;
    public int pointsToAdd;

    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").transform;

        soundFeedback = FindObjectOfType<EnemySoundFeedback>();
        pontuation = FindObjectOfType<Pontuation>();

        StartCoroutine(Shoot());
    }

    void Update()
    {
        //Verifica se o jogador existe
        if(player == null)
        {
            canShoot = false;
        }
        else
        {
            
            LookAtPlayer();
            CheckIfInScreen();
        }

        MoveDown();
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }


    //Define a rotação de cada arma
    void LookAtPlayer()
    {
        if (canShoot)
        {
            foreach (Transform weapon in weaponTransforms)
            {
                Vector3 direction = player.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                weapon.rotation = Quaternion.Slerp(weapon.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
        else
        {
            foreach(Transform weapon in weaponTransforms)
            {
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                weapon.rotation = Quaternion.Slerp(weapon.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (canShoot)
            {
                foreach (Transform weapon in weaponTransforms)
                {
                    Vector3 shootDirection = weapon.up;
                    Instantiate(bulletPrefab, weapon.position, Quaternion.LookRotation(Vector3.forward, shootDirection));
                }
                soundFeedback.PlaySound(EnemySoundType.shootSound);
            }
            yield return new WaitForSeconds(shootInterval);
        }
    }

    void CheckIfInScreen()
    {
        // Verifica se o inimigo está fora da tela
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPos.y < 0 || screenPos.y > 1)
        {
            isOnScreen = false;
        }
        else
        {
            isOnScreen = true;

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            
            if (Mathf.Abs(transform.position.y - player.position.y) < stopShootingDistance && playerHealth.currentLifes == 0)
            {
                canShoot = false;
            }
            else
            {
                canShoot = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "playerLaser")
        {
            other.GetComponent<Laser>().Destroy();
            if (isDestroiyng == false)
            {
                isDestroiyng = true;
                soundFeedback.PlaySound(EnemySoundType.explodeSound);
                FindObjectOfType<RoundManager>().EnemyDestroyed();
                pontuation.AddPoints(pointsToAdd);
                Instantiate(explosionPf, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        if (other.tag == "limit")
        {
            if(player != null)
                player.GetComponent<PlayerHealth>().DecreaseLife();

            pontuation.DecreasePoints(pointsToAdd / 2); //Desconta a pontuação total do jogador com a metade da pontuação concedida pelo inimigo 
            FindObjectOfType<RoundManager>().EnemyDestroyed();
            Destroy(gameObject);
        }
    }
}
