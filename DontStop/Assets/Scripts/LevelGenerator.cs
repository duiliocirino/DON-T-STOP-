using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] levelSections;
    public bool isCreating = false;
    public int currentZ = 645;

    // Update is called once per frame
    void Update()
    {
        if(!isCreating){
            isCreating = true;
            StartCoroutine(GenerateLevel());
        }
    }

    IEnumerator GenerateLevel(){
        int sectionNumber = Random.Range(0,levelSections.Length);
        Instantiate(levelSections[sectionNumber], new Vector3(20,-12.6f,currentZ), Quaternion.identity);
        currentZ += 200;
        yield return new WaitForSeconds(4);
        isCreating = false;
    }
}
