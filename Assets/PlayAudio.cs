using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour {

    public void playAudio() {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play(0);
    }
}

