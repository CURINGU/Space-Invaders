using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSoundFeedback : MonoBehaviour
{
    [SerializeField]
    private AudioClip selection, selected, textTyping;

    [SerializeField]
    private AudioSource audioSource;

    public void PlaySound(UiSoundType soundType)
    {
        switch (soundType)
        {
            case UiSoundType.selection:
                audioSource.PlayOneShot(selection);
                break;
            case UiSoundType.selected:
                audioSource.PlayOneShot(selected);
                break;
            case UiSoundType.textTyping:
                audioSource.PlayOneShot(textTyping);
                break;
            default:
                break;
        }
    }
}

public enum UiSoundType
{
    selection,
    selected,
    textTyping
}

