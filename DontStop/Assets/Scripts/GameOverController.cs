using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        StartCoroutine(Reload());
    }
    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(0);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    public void LoadAndUnload()
    {
        SceneManager.UnloadSceneAsync(sceneName);
        SceneManager.LoadScene(sceneName);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
