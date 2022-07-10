using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryStageButtonData : MonoBehaviour
{
    public int stageNumber;
    public string sceneName;

    public int firstStarScoreTreshold;
    public int secondStarScoreTreshold;
    public int thirdStarScoreTreshold;

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
        if (unlocked)
        {
            int score = SaveController.istance.GetHighestStoryScoreRecord(stageNumber);
            if(score >= firstStarScoreTreshold)
            {
                firstStarImage.color = starColor;
            }
            if (score >= secondStarScoreTreshold)
            {
                secondStarImage.color = starColor;
            }
            if (score >= thirdStarScoreTreshold)
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
