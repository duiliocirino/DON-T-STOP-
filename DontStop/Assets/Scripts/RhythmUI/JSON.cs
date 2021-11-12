using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class JSON : MonoBehaviour
{
    public Text outputText;

    public TextAsset printedJSON;

    public void printPatternJSON()
    {
        PatternMap pm = new PatternMap("default120");
        string json = JsonUtility.ToJson(pm);
        print(json);
        outputText.text = json;

        string filePath = "Assets/PatternMaps/PatternMapDefault120BPM.json";
        File.WriteAllText(filePath, json);
    }

    public void checkJSON()
    {
        string json = printedJSON.text;
        PatternMap pm = JsonUtility.FromJson<PatternMap>(json);
        print(pm.MyToString());
        outputText.text = pm.MyToString();
    }
}
