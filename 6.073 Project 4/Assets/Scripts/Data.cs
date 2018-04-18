using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Data : MonoBehaviour {

    public string id = "";
    public int plays = 0;
    public int score = 0;
    public string currentLevel = "";
    public string status = "ongoing";
    float start_time = 0;
    float level_time = 0;

    // individual levels would have to be manually set as parameters as of now
    public float level1 = 0;
    public float level2 = 0;
    
   

    // Use this for initialization
    void Start () {
        id = generateId();
        start_time = Time.time;
	}

    // Change status (options are "onging" and "game_over")
    public void changeStatus(string s)
    {
        status = s;
    }

    // Move on to next level and reset timer
    public void nextLevel(string level)
    {
        currentLevel = level;
        level_time = 0;
    }
    
    public void levelComplete(string level)
    {
        if (level == "level1")
        {
            level1 = Time.time - level_time;
        }
        if (level == "level2")
        {
            level2 = Time.time - level_time;
        }
        level_time = Time.time;
    }


    // Generate a 16 character long session ID
    // Code taken from "https://madskristensen.net/blog/generate-unique-strings-and-numbers-in-c/"
    private string generateId()
    {
        long i = 1;
        foreach (byte b in Guid.NewGuid().ToByteArray())
        {
            i *= ((int)b + 1);
        } 
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }

    public void incPlays()
    {
        plays += 1;
    }

    private string dictionaryToString(Dictionary<string, float> dict)
    {
        string[] entries = new string[dict.Count];
        int index = 0;
        foreach (KeyValuePair<string, float> entry in dict){
            entries[index] = string.Format("\"{0}\": {1}", entry.Key, entry.Value);
            index += 1;
        }
        return "{" + string.Join(",", entries) + "}";
    }

    public void storeData()
    {
        string filepath =  Application.dataPath + "/Data/" + id + ".json";
        File.WriteAllText(filepath, JsonUtility.ToJson(this, true));
    }

}
