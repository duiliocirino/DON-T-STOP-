using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    public static LifeBar instance;

    public float depletionRate;
    private float depletionSpeed;

    public float perfectHitBonus;
    private float perfectHitDistanceGained;

    private RectTransform rectTransform;
    private Vector2 startingPosition;
    private float positionXLimit;

    private List<Action> onLimitReached = new List<Action>();

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

        startingPosition = rectTransform.anchoredPosition;
        positionXLimit = startingPosition.x - rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = rectTransform.anchoredPosition - new Vector2(depletionSpeed * Time.deltaTime, 0);
        if(newPosition.x < positionXLimit)
        {
            newPosition.x = positionXLimit;
        }
        rectTransform.anchoredPosition = newPosition;
        if (newPosition.x == positionXLimit) OnLimitReached();
    }

    private void OnLimitReached()
    {
        foreach (Action a in onLimitReached)
            a.Invoke();
    }

    public void PerfectHit()
    {
        Vector2 newPosition = rectTransform.anchoredPosition + new Vector2(perfectHitDistanceGained, 0);
        if (newPosition.x > startingPosition.x)
        {
            newPosition.x = startingPosition.x;
        }
        rectTransform.anchoredPosition = newPosition;
    }

    public void RegisterLimitReachedBehaviour(Action a)
    {
        onLimitReached.Add(a);
    }

    public void UnregisterLimitReachedBehaviour(Action a)
    {
        onLimitReached.Remove(a);
    }
}
