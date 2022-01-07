using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class LifeBar : MonoBehaviour
{
    public static LifeBar instance { get; private set; }

    public float depletionRate;
    private float depletionSpeed;

    public float perfectHitBonus;
    private float perfectHitDistanceGained;

    public float barelyHitBonus;
    private float barelyHitDistanceGained;

    public float worstMissPenality;
    private float worstMissDistanceLoss;

    public float betterMissPenality;
    private float betterMissDistanceLoss;

    private RectTransform rectTransform;
    private float startingWidth;
    private float widthLimit;

    private List<Action> onLimitReached = new List<Action>();

    public bool deplitionHasStarted {get; private set; }

    private void Awake()
    {
        instance = this;

        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        depletionSpeed = depletionRate * rectTransform.sizeDelta.x;
        perfectHitDistanceGained = perfectHitBonus * rectTransform.sizeDelta.x;
        barelyHitDistanceGained = barelyHitBonus * rectTransform.sizeDelta.x;
        worstMissDistanceLoss = worstMissPenality * rectTransform.sizeDelta.x;
        betterMissDistanceLoss = betterMissPenality * rectTransform.sizeDelta.x;

        startingWidth = rectTransform.sizeDelta.x;
        widthLimit = 0;

        deplitionHasStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (deplitionHasStarted)
        {
            float newWidth = rectTransform.sizeDelta.x - depletionSpeed * Time.deltaTime;
            if (newWidth < widthLimit)
            {
                newWidth = widthLimit;
            }
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
            if (newWidth == widthLimit) OnLimitReached();
        }
    }

    private void OnLimitReached()
    {
        foreach (Action a in onLimitReached)
            a.Invoke();
    }

    public void PerfectHit()
    {
        if (gameObject.activeInHierarchy == false) return;

        float newWidth = rectTransform.sizeDelta.x + perfectHitDistanceGained;
        if (newWidth > startingWidth)
        {
            newWidth = startingWidth;
        }
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
    }

    public void BarelyHit()
    {
        if (gameObject.activeInHierarchy == false) return;

        float newWidth = rectTransform.sizeDelta.x + barelyHitDistanceGained;
        if (newWidth > startingWidth)
        {
            newWidth = startingWidth;
        }
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
    }

    public void WorstMiss()
    {
        if (gameObject.activeInHierarchy == false) return;

        float newWidth = rectTransform.sizeDelta.x - worstMissDistanceLoss;
        if (newWidth < widthLimit)
        {
            newWidth = widthLimit;
        }
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
        if (newWidth == widthLimit) OnLimitReached();
    }

    public void BetterMiss()
    {
        if (gameObject.activeInHierarchy == false) return;

        float newWidth = rectTransform.sizeDelta.x - betterMissDistanceLoss;
        if (newWidth < widthLimit)
        {
            newWidth = widthLimit;
        }
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
        if (newWidth == widthLimit) OnLimitReached();
    }

    public void RegisterLimitReachedBehaviour(Action a)
    {
        onLimitReached.Add(a);
    }

    public void UnregisterLimitReachedBehaviour(Action a)
    {
        onLimitReached.Remove(a);
    }

    public void StartDeplition()
    {
        deplitionHasStarted = true;
    }

    public void StopDeplition()
    {
        deplitionHasStarted = false;
    }
}
