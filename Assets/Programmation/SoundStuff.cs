using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundStuff : object {

    public static void PlayRandomOneShot(AudioSource source, AudioClip[] sounds, float volume)
    {
        if(sounds.Length > 0) {
            int chosenOne = Random.Range (0, sounds.Length);
            source.PlayOneShot(sounds[chosenOne], volume);
        }
    }
}
