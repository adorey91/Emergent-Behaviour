using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip sfxClip;
    [SerializeField] private AudioClip removeClip;
    [SerializeField] private AudioClip addClip;

    private void OnEnable()
    {
        Actions.OnPlaySFX += PlaySFX;
    }

    private void OnDisable()
    {
        Actions.OnPlaySFX -= PlaySFX;
    }

    private void PlaySFX(string clip)
    {
        switch (clip)
        {
            case "Tile": sfxSource.clip = sfxClip; break;
            case "RemoveFish": sfxSource.clip = removeClip; break;
            case "AddFish": sfxSource.clip = addClip; break;
        }

        sfxSource.PlayOneShot(sfxSource.clip);
    }
}