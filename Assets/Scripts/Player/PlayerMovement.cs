using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    public Transform renderObject;

    public Transform[] weaponTransforms;

    public bool canMove = true;

    private PlayerPowerUps playerPowerUps;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerPowerUps = GetComponent<PlayerPowerUps>();
        playerHealth = GetComponent<PlayerHealth>();
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

    //Faz o objeto olhar para o mouse
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
        }
        if (other.gameObject.tag == "PowerUpLife")
        {
            Destroy(other.gameObject);
            playerHealth.AddLife();
        }
    }
}
