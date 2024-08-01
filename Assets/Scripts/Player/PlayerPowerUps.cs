using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    public Animator shieldAnim;
    public GameObject shieldObject;
    public float shieldDuration = 5f;
    public bool isShieldActive;

    void Start()
    {
        if (shieldObject != null)
        {
            shieldObject.SetActive(false);
        }
    }

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
    }
}
