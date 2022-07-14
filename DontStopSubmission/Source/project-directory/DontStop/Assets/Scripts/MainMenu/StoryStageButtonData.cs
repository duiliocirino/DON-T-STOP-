using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryStageButtonData : MonoBehaviour
{
    public int stageNumber;
    public string sceneName;

    public GameObject starContainer;
    public Image firstStarImage;
    public Image secondStarImage;
    public Image thirdStarImage;

    public Color starColor;

    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        bool unlocked = SaveController.istance.IsStoryStageUnlocked(stageNumber);
        thisButton.interactable = unlocked;
        if (stageNumber!=0 && SaveController.istance.IsStoryStageCompleted(stageNumber))
        {
            starContainer.SetActive(true);

            int score = SaveController.istance.GetHighestStoryScoreRecord(stageNumber);
            if(score >= StarThresholds.instance.getThreshold(stageNumber, 1))
            {
                firstStarImage.color = starColor;
            }
            if (score >= StarThresholds.instance.getThreshold(stageNumber, 2))
            {
                secondStarImage.color = starColor;
            }
            if (score >= StarThresholds.instance.getThreshold(stageNumber, 3))
            {
                thirdStarImage.color = starColor;
            }
        }
        else
        {
            starContainer.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
