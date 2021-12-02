using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChooser : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource source;

    public void PlayRand()
    {
        var clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        source.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
