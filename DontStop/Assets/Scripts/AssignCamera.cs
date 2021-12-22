using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AssignCamera : MonoBehaviour
{
    [SerializeField] Camera videoCam;
    // Start is called before the first frame update
    void Awake()
    {
        var videoPlayer = FindObjectOfType<VideoPlayer>();
        

        if (videoPlayer.targetCamera == null)
                videoPlayer.targetCamera = videoCam;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
