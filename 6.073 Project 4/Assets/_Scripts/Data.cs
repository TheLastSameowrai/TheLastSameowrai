using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Data : MonoBehaviour {

    public string id;
    public int plays;
    public string currentLevel;
    public string status;
    float start_time;
    float level_time;
	public int times_paused;
	public int keys_pressed;
	public int invalid_keys_pressed;
	public int enemies_spawned;
	public int enemies_killed;
	public String enemy_lost;

    public LevelData[] levels;
    List<LevelData> levelList;
   

    // Use this for initialization
    public void Start () {
        id = generateId();
        plays = 1;
        currentLevel = "1";
        status = "ongoing";
        start_time = Time.time;
        level_time = start_time;
        levelList = new List<LevelData>();
    }


    // Move on to next level and reset timer
    public void nextLevel(string level)
    {
        currentLevel = level;
        level_time = Time.time;
    }
    
    public void levelComplete(string result)
    {
        if (result == "game_over")
        {
            status = "game_over";
        }
        LevelData newLevel = new LevelData();
        newLevel.levelName = currentLevel;
        newLevel.completeTime = Time.time - level_time;
        newLevel.result = result;
        levelList.Add(newLevel);
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
        levels = levelList.ToArray();
        string filepath =  Application.dataPath + "/Data/" + id + ".json";
        // For now, doesn't actually store the data
        // File.WriteAllText(filepath, JsonUtility.ToJson(this, true));
        Debug.Log("Stored Data");
        print(JsonUtility.ToJson(this, false));
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