using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStageButton : MonoBehaviour
{
    private SceneController sceneController;

    private void Awake()
    {
        sceneController = GetComponent<SceneController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress()
    {
        string scene = SelectedStage.istance.selectedStage;
        Destroy(SelectedStage.istance.gameObject);
        sceneController.ChangeScene(scene);
    }
}
