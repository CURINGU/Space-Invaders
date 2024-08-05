using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject explosionPf;
    private EnemySoundFeedback soundFeedback;
    private bool isDestroiyng = false;

    [Header("Movement")]
    public Transform renderObject;
    private Transform player;
    public float moveSpeed = 2f;
    public bool isOnScreen;

    [Header("Weapons")]
    public Transform[] weaponTransforms;
    public float shootInterval = 0.5f;
    public float stopShootingDistance = 2f;
    public GameObject bulletPrefab;
    public bool canShoot = false;

    [Space(20)]
    public bool useOffset;
    public float offset;

    [Header("Health")]
    public int lives;
    public bool isUsingShield;
    public GameObject shield; //Apenas usar se existir shield no objeto

    [Space(20)]
    public int pointsToAdd;
    private Pontuation pontuation;

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

                // Se usa o offset, o diciona dependendo de qual arma esta lidando (esquerda ou direita)
                if (useOffset && weapon.name == "LeftGun")
                {
                    angle -= offset;
                }
                else if (useOffset && weapon.name == "RightGun")
                {
                    angle += offset;
                }

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
            if(lives > 0)
            {
                lives--;

                if(lives <= 1 && isUsingShield)
                {
                    isUsingShield = false;
                    shield.SetActive(false);
                    soundFeedback.PlaySound(EnemySoundType.shieldDown);
                }

                if(lives == 0)
                {
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
            }
        }

        if (other.tag == "limit")
        {
            if(player != null)
            {
                player.GetComponent<PlayerHealth>().DecreaseLife();
                pontuation.DecreasePoints(pointsToAdd / 2); //Desconta a pontuação total do jogador com a metade da pontuação concedida pelo inimigo 
                FindObjectOfType<RoundManager>().EnemyDestroyed();
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
