using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedStage : MonoBehaviour
{
    public static SelectedStage istance { private set; get; } = null;

    public string selectedStage;
    public int stageNumber;

    private void Awake()
    {
        if (istance != null)
            Destroy(istance.gameObject);

        istance = this;
        DontDestroyOnLoad(gameObject);
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
