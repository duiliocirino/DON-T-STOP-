using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    public float speed = 0f;
    public float timeToReachCenter = 0f;
    public Vector2 centerPosition = new Vector2(0, 0);
    public RectTransform rectTransform;
    public AudioSource musicPlayer;
    //public bool firstNote = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (speed != 0) print(musicPlayer.time);
        /*if(!firstNote && speed != 0f && timeToReachCenter > musicPlayer.time)
        {
            print("speed recalculation");
            speed = - (rectTransform.anchoredPosition.x - centerPosition.x) / (timeToReachCenter - musicPlayer.time);
        }*/

        if (RhythmControllerUI.instance.hasStarted)
            rectTransform.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);

        if (speed * rectTransform.anchoredPosition.x > 0)
        {
            //float time = musicPlayer.time;
            //print(timeToReachCenter + "(T) vs (A)" + time + "; (D)" + (time-timeToReachCenter));
            StartCoroutine(StopNote());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "HitArea")
        {
            RhythmControllerUI.instance.noteInHitArea = true;
        }
    }

    private IEnumerator StopNote()
    {
        float tmpSpeed = speed;
        speed = 0f;

        float exededLength = rectTransform.anchoredPosition.x;
        rectTransform.anchoredPosition -= new Vector2(exededLength, 0);

        yield return new WaitForSeconds(RhythmControllerUI.instance.noteDespawnDelay - exededLength/tmpSpeed);

        //RhythmControllerUI.instance.NextHitTime();

        RhythmControllerUI.instance.noteInHitArea = false;

        rectTransform.anchoredPosition = RhythmControllerUI.instance.noteBufferPosition;
    }
}
