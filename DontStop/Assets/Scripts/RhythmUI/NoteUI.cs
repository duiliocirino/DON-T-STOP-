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

        if (speed * rectTransform.anchoredPosition.x > 0)
            StartCoroutine(StopNote());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "HitArea")
        {
            RhythmControllerUI.instance.noteInHitArea = true;
        }
    }

    /*private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "HitArea")
        {
            RhythmControllerUI.instance.noteInHitArea = false;
            speed = 0f;
            rectTransform.anchoredPosition = RhythmControllerUI.instance.noteBufferPosition;
        }
    }*/

    private IEnumerator StopNote()
    {
        float tmpSpeed = speed;
        speed = 0f;

        float exededLength = rectTransform.anchoredPosition.x;
        rectTransform.anchoredPosition -= new Vector2(exededLength, 0);

        yield return new WaitForSeconds(RhythmControllerUI.instance.noteDespawnDelay - exededLength/tmpSpeed);

        RhythmControllerUI.instance.noteInHitArea = false;

        rectTransform.anchoredPosition = RhythmControllerUI.instance.noteBufferPosition;
    }
}
