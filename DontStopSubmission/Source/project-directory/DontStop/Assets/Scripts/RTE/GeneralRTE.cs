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
        if (lightningPrefab != null)
        {
            lightning = Instantiate(lightningPrefab, newPlatform.transform.position, Quaternion.identity);
            yield return new WaitForSecondsRealtime(3f);
            PlaneHandler.instance.PopPlatform();
            yield return new WaitForSecondsRealtime(0.5f);
            Destroy(lightning);
        }
    }
    
}
