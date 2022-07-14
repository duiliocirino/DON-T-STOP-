using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralRTE : MonoBehaviour
{
    public static GeneralRTE instance;
    
    public float lightningProb = 0.5f;
    public GameObject lightningPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LightningEvent(GameObject newPlatform)
    {
        GameObject lightning;
        float sample = Random.Range(0f, 1f);
        if (sample < lightningProb && lightningPrefab != null)
        {
            //yield return new WaitForSecondsRealtime(2f);
            lightning = Instantiate(lightningPrefab, newPlatform.transform.position, Quaternion.identity);
            PlaneHandler.instance.PopPlatform();
            yield return new WaitForSecondsRealtime(1.5f);
            Destroy(lightning);
        }
    }
    
}
