using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] levelSections;
    public bool isCreating = false;
    public float sectionSize;
    public int currentZ = 645;

    public float placingDistance;

    public Transform playerTransform;
    public Transform cameraTransform;

    public GameObject[] preplacedLevelSections;
    private Queue<GameObject> placedSections;

    private void Awake()
    {
        DynamicGI.UpdateEnvironment();
    }
    void Start()
    {
        placedSections = new Queue<GameObject>(preplacedLevelSections);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(!isCreating){
            isCreating = true;
            StartCoroutine(GenerateLevel());
        }*/

        TryPlaceLevel();
        TryRemoveLevel();
    }

    private void TryPlaceLevel()
    {
        if(Math.Abs(currentZ - playerTransform.position.z) < placingDistance)
        {
            int sectionNumber = UnityEngine.Random.Range(0, levelSections.Length);
            placedSections.Enqueue(Instantiate(levelSections[sectionNumber], new Vector3(8, -12.6f, currentZ), Quaternion.identity));
            currentZ += (int)sectionSize;
        }
    }

    private void TryRemoveLevel()
    {
        if (placedSections.Count != 0 && placedSections.Peek().transform.position.z < cameraTransform.position.z - sectionSize / 2)
        {
            PlaneHandler.instance.RemoveOldPlanes(cameraTransform.position.z);
            Destroy(placedSections.Dequeue());
        }
    }

    /*IEnumerator GenerateLevel(){
        int sectionNumber = UnityEngine.Random.Range(0,levelSections.Length);
        Instantiate(levelSections[sectionNumber], new Vector3(20,-12.6f,currentZ), Quaternion.identity);
        currentZ += 200;
        yield return new WaitForSeconds(4);
        isCreating = false;
    }*/
}
