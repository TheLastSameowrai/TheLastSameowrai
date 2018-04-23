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

    public LevelData[] levels;
    List<LevelData> levelList = new List<LevelData>();
   

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
    
    public void levelComplete(string level, string result)
    {
        LevelData newLevel = new LevelData();
        newLevel.levelName = level;
        newLevel.completeTime = Time.time - level_time;
        newLevel.result = result;
        levelList.Add(newLevel);
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

    public void storeData()
    {
        levelComplete("testLevel", "win");
        levelComplete("newTest", "lose");
        levels = levelList.ToArray();
        string filepath =  Application.dataPath + "/Data/" + id + ".json";
        File.WriteAllText(filepath, JsonUtility.ToJson(this, true));
    }

}

[Serializable]
public class LevelData
{
    public string levelName;
    public float completeTime;
    public string result;
}

// JsonHelper code taken from https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
