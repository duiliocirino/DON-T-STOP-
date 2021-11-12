using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    public float speed = 0f;
    public RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(RhythmControllerUI.instance.hasStarted)
            rectTransform.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "HitArea")
        {
            RhythmControllerUI.instance.noteInHitArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "HitArea")
        {
            RhythmControllerUI.instance.noteInHitArea = false;
            stopNote();
        }
    }

    private void stopNote()
    {
        speed = 0f;
        rectTransform.anchoredPosition = RhythmControllerUI.instance.noteBufferPosition;
    }
}
