using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsTunnel : MonoBehaviour {
    public AudioSource Near;

    public AudioSource Far;
    public Direction Direction;

    public void Fire(Distance distance) {
        if (distance == Distance.Near) {
            Fire(Near); 
        } else if (distance == Distance.Far) {
            Fire(Far);
        }
    }


    private static void Fire(AudioSource audioSource) {
        audioSource.Play();
    }
}

public enum Distance {
    Near,
    Far
}

public enum Direction {
    N,
    E,
    S,
    W
}
