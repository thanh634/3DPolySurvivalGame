using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; set; }

    public AudioSource dropItemSound;
    public AudioSource toolSwingSound;
    public AudioSource craftItemSound;
    public AudioSource pickupItemSound;
    public AudioSource grassWalkSound;
    public AudioSource treeChoppedSound;

    //Music
    public AudioSource startingZoneBGM;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
    }

    public void PlaySound(AudioSource soundToPlay)
    {
        if (!soundToPlay.isPlaying)
        {
            soundToPlay.Play();
        }
    }

    public void PlayDropSound() => PlaySound(dropItemSound);

    public void PlayToolSwingSound() => PlaySound(toolSwingSound);

    public void PlayCraftSound() => PlaySound(craftItemSound);

    public void PlayPickUpItemSound() => PlaySound(pickupItemSound);

    public void PlayGrassWalkSound() => PlaySound(grassWalkSound);

    public void PlayTreeChoppedSound() => PlaySound(treeChoppedSound);

}
