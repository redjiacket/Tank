using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public AudioClip hit_Audio;

    public void Play_Audio()
    {
        AudioSource.PlayClipAtPoint(hit_Audio, transform.position);
    }
}
