using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundFeedback : MonoBehaviour
{
    [SerializeField]
    private AudioClip shootSound, explodeSound, meteorExplodeSound;

    [SerializeField]
    private AudioSource audioSource;

    public void PlaySound(EnemySoundType soundType)
    {
        switch (soundType)
        {
            case EnemySoundType.shootSound:
                audioSource.PlayOneShot(shootSound);
                break;
            case EnemySoundType.explodeSound:
                audioSource.PlayOneShot(explodeSound);
                break;
            case EnemySoundType.meteorExplodeSound:
                audioSource.PlayOneShot(meteorExplodeSound);
                break;
            default:
                break;
        }
    }
}

public enum EnemySoundType
{
    shootSound,
    explodeSound,
    meteorExplodeSound,
}
