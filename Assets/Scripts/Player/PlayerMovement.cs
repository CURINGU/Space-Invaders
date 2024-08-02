using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed;
    public Transform renderObject; 
    public bool canMove = true;

    [Header("Weapons Rotation")]
    public Transform[] weaponTransforms;

    private PlayerPowerUps playerPowerUps;
    private PlayerHealth playerHealth;
    private PlayerSoundFeedback soundFeedback;

    private void Start()
    {
        playerPowerUps = GetComponent<PlayerPowerUps>();
        playerHealth = GetComponent<PlayerHealth>();
        soundFeedback = GetComponentInChildren<PlayerSoundFeedback>();
    }

    void Update()
    {
        if(canMove)
        {
            Move();
            RotateWeaponsTowardsMouse();
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Limitar movimento lateral
        float clampedX = Mathf.Clamp(transform.position.x, -8f, 8f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    //Faz as armas olharem para o mouse
    void RotateWeaponsTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        foreach (Transform weapon in weaponTransforms)
        {
            Vector3 direction = mousePosition - weapon.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

            // Adicionar o offset dependendo de qual arma esta lidando (esquerda ou direita)
            if (weapon.name == "gunLeft")
            {
                angle += 2.5f;
            }
            else if (weapon.name == "gunRight")
            {
                angle -= 2.5f;
            }

            weapon.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemyLaser")
        {
            other.GetComponent<Laser>().Destroy();
            GetComponent<PlayerHealth>().DecreaseLife();
        }
        if (other.gameObject.tag == "PowerUpShield")
        {
            Destroy(other.gameObject);
            if(!playerPowerUps.isShieldActive)
                playerPowerUps.ActivateShield();
            soundFeedback.PlaySound(PlayerSoundType.gainPowerUp);
        }
        if (other.gameObject.tag == "PowerUpLife")
        {
            Destroy(other.gameObject);
            playerHealth.AddLife();
            soundFeedback.PlaySound(PlayerSoundType.gainPowerUp);
        }
    }
}
