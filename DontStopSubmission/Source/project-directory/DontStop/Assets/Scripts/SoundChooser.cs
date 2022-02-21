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

    public void PlayRandWithExclusion()
    {
        int numClip = UnityEngine.Random.Range(0, clips.Length + 9);
        if (numClip < clips.Length)
        {
            var clip = clips[numClip];
            source.PlayOneShot(clip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
