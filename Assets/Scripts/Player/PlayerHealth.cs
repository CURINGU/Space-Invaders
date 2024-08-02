using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private Animator anim;
    public int currentLifes;
    public int maxLifes;

    public TMP_Text lifeTxt;

    private PlayerSoundFeedback soundFeedback;
    private PlayerPowerUps playerPowerUps;

    void Start()
    {
        anim = GetComponent<Animator>();
        soundFeedback = GetComponentInChildren<PlayerSoundFeedback>();
        playerPowerUps = GetComponent<PlayerPowerUps>();
        currentLifes = maxLifes;
        lifeTxt.text = currentLifes.ToString();
    }

    private void Update()
    {
        lifeTxt.text = currentLifes.ToString();
    }

    //Retira uma vida ao jogador ser atingido, ou o destroi
    public void DecreaseLife()
    {
        if(!playerPowerUps.isShieldActive && currentLifes > 0)
        {
            currentLifes--;
            anim.SetTrigger("Damage");
            soundFeedback.PlaySound(PlayerSoundType.damageSound);

            if (currentLifes == 0)
            {
                anim.SetTrigger("Explode");
                soundFeedback.PlaySound(PlayerSoundType.defeadSound);
            }
        }
    }

    public void AddLife()
    {
        currentLifes++;
    }

    public void AddLifeAmount(int amount)
    {
        currentLifes += amount;
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
