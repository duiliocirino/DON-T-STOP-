using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    public static readonly string leaderboardStageSelectedKey = "SelectedLeaderboard";

    public GameObject entryContainer;
    public float entrySpacing = 50;
    public GameObject entryPrefab;

    public Text noDataText;

    private List<GameObject> entries = new List<GameObject>();
    private List<LeaderboardEntry> entryScripts = new List<LeaderboardEntry>();

    public List<Button> selectionButtons = new List<Button>();

    private void Awake()
    {
        int stage;
        if (PlayerPrefs.HasKey(leaderboardStageSelectedKey))
        {
            stage = PlayerPrefs.GetInt(leaderboardStageSelectedKey);
        }
        else
        {
            stage = 1;
        }

        InitializeEntries();

        ViewRecords(stage);
    }

    private void InitializeEntries()
    {
        for(int i=0; i<LevelData.maxNumRecordsSaved; i++)
        {
            GameObject o = Instantiate(entryPrefab);
            o.transform.SetParent(entryContainer.transform, false);
            o.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * entrySpacing);
            o.SetActive(false);
            entries.Add(o);
            entryScripts.Add(o.GetComponent<LeaderboardEntry>());
        }
    }

    public void ViewRecords(int stage)
    {
        for (int i = 0; i < selectionButtons.Count; i++)
        {
            selectionButtons[i].interactable = i+1 != stage;
        }
        PlayerPrefs.SetInt(leaderboardStageSelectedKey, stage);

        foreach (GameObject o in entries)
        {
            o.SetActive(false);
        }
        noDataText.gameObject.SetActive(false);

        List<LevelRecord> records = SaveController.istance.GetRecords(stage);
        if(records == null || records.Count <= 0)
        {
            noDataText.gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < entries.Count && i < records.Count; i++)
            {
                entryScripts[i].UpdateContent(i + 1, records[i]);
                entries[i].SetActive(true);
            }
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
