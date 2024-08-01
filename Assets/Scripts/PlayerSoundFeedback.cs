using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundFeedback : MonoBehaviour
{
    [SerializeField]
    private AudioClip shootSound, explodeSound, damageSound, defeadSound;

    [SerializeField]
    private AudioSource audioSource;

    public void PlaySound(PlayerSoundType soundType)
    {
        switch (soundType)
        {
            case PlayerSoundType.shootSound:
                audioSource.PlayOneShot(shootSound);
                break;
            case PlayerSoundType.explodeSound:
                audioSource.PlayOneShot(explodeSound);
                break;
            case PlayerSoundType.damageSound:
                audioSource.PlayOneShot(damageSound);
                break;
            case PlayerSoundType.defeadSound:
                audioSource.PlayOneShot(defeadSound);
                break;
            default:
                break;
        }
    }
}

public enum PlayerSoundType
{
    shootSound,
    explodeSound,
    damageSound,
    defeadSound
}
