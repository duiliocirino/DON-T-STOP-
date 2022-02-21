using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DontDestroy : MonoBehaviour
{
    public static bool created = false;

    // Start is called before the first frame update
    void Start()
    {
        created = true;
    }

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject.transform);
        }
        else
        {
            Destroy(gameObject);
        }

    }


    // Update is called once per frame
    void Update()
    {

    }
}