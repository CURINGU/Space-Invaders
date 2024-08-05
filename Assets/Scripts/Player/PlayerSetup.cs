using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public PlayerData playerData;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private PlayerAim playerAim;

    [Header("Parts")]
    public GameObject cockPit;
    public GameObject wingLeft;
    public GameObject wingRight;
    public GameObject gunLeft;
    public GameObject gunRight;
    public GameObject trail;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAim = GetComponent<PlayerAim>();
        playerHealth = GetComponent<PlayerHealth>();

        ApplyPlayerData();
    }

    private void ApplyPlayerData()
    {
        // Encontra a nave escolhida pelo jogador
        int selectedShipt = PlayerPrefs.GetInt("SpaceShip");
        playerVariables selectedVariables = playerData.playerVariables[selectedShipt]; 

        // Trocar as partes da nave
        ChangeRenderer(cockPit, selectedVariables.cockPitPrefab);
        ChangeRenderer(wingLeft, selectedVariables.wingLeftPrefab);
        ChangeRenderer(wingRight, selectedVariables.wingRightPrefab);
        ChangeRenderer(gunLeft, selectedVariables.gunLeftPrefab);
        ChangeRenderer(gunRight, selectedVariables.gunRightPrefab);
        ChangeRenderer(trail, selectedVariables.trailPrefab);

        // Aplicar habilidades
        playerMovement.moveSpeed = selectedVariables.speed;
        playerAim.maxReloadTime = selectedVariables.reloadTime;
        playerHealth.maxLifes = selectedVariables.lives;
        playerHealth.currentLifes = selectedVariables.lives;
    }

    private void ChangeRenderer(GameObject existingPart, Sprite newPartPrefab)
    {
        if (existingPart != null && newPartPrefab != null)
        {
            existingPart.SetActive(true);

            existingPart.GetComponent<SpriteRenderer>().sprite = newPartPrefab;
        }
    }
}
