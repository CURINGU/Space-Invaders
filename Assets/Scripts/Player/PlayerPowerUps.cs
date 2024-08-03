using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    [Header("Shield")]
    public Animator shieldAnim;
    public GameObject shieldObject;
    public float shieldDuration = 5f;
    public int shieldLvl = 1;
    public int shieldToAdd;
    public bool isShieldActive;

    [Header("MovementSpeed")]
    public int speedLvl = 1;
    public float speedToAdd;
    private PlayerMovement playerMovement;

    [Header("ReloadTime")]
    public int reloadLvl = 1;
    public float timeToDecrease;
    private PlayerAim playerAim;

    [Header("Lives")]
    public int livesAdded = 1;
    public int livesToAdd;
    private PlayerHealth playerHealth;

    private PlayerSoundFeedback soundFeedback;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAim = GetComponent<PlayerAim>();
        playerHealth = GetComponent<PlayerHealth>();
        soundFeedback = GetComponentInChildren<PlayerSoundFeedback>();

        if (shieldObject != null)
        {
            shieldObject.SetActive(false);
        }
    }

    public void AddSpeed()
    {
        if(speedLvl <= 3)
        {
            playerMovement.moveSpeed += speedToAdd;
            speedLvl++;
        }
    }

    public void DecreaseReloadTime()
    {
        if(reloadLvl <= 3)
        {
            playerAim.maxReloadTime -= timeToDecrease;
            reloadLvl++;
        }
    }

    public void AddLives()
    {
        if(livesAdded <= 3)
        {
            playerHealth.AddLifeAmount(livesToAdd);
            livesAdded++;
        }
    }

    public void AddShieldDuration()
    {
        if (shieldLvl <= 3)
        {
            shieldDuration += shieldToAdd;
            shieldLvl++;
        }
    }

    //Ativa o escudo e executa a coroutine para desativa-lo
    public void ActivateShield()
    {
        if (shieldObject != null && !isShieldActive)
        {
            shieldObject.SetActive(true);
            isShieldActive = true;
            StartCoroutine(DeactivateShieldAfterTime(shieldDuration));
        }
    }

    private IEnumerator DeactivateShieldAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        shieldAnim.SetTrigger("Deactivate");
        yield return new WaitForSeconds(1f);
        isShieldActive = false;
        shieldObject.SetActive(false);
        soundFeedback.PlaySound(PlayerSoundType.shieldDown);
    }
}
