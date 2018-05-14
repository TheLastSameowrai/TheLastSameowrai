using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Data : MonoBehaviour {

    public string id;
    public int plays;
	public int pauses;
    float start_time;
    float level_time;

    public LevelData[] levels;
    List<LevelData> levelList;

	public EnemyData[] enemiesHit;
	List<EnemyData> enemyList;

	public KeyData[] keysHit;
	List<KeyData> keysList;
   

    // Use this for initialization
    public void Start () {
        id = generateId();
        plays = 1;
		pauses = 0;
        start_time = Time.time;
        level_time = start_time;
        levelList = new List<LevelData>();
		enemyList = new List<EnemyData> ();
		keysList = new List<KeyData> ();
    }
    
	public void levelComplete(int level, float time, int enemies_killed, int enemies_spawned, string result)
    {
		LevelData newLevel = new LevelData ();
		newLevel.levelNumber = level;
		newLevel.playNumber = plays;
		newLevel.completeTime = time - level_time;
		newLevel.currentTime = Time.time - start_time;
		newLevel.enemies_killed = enemies_killed;
		newLevel.enemies_spawned = enemies_spawned;
		newLevel.result = result;
		level_time = Time.time;
		levelList.Add (newLevel);
		storeData ();
    }

	public void keyPressed(string key, bool valid, float time)
	{
		KeyData newKey = new KeyData ();
		newKey.keyPressed = key;
		newKey.valid = valid;
		newKey.hitTime = time - start_time;
		keysList.Add (newKey);
	}

	public void enemyHit(string enemyType, int level, float time){
		EnemyData enemy = new EnemyData ();
		enemy.enemyType = enemyType;
		enemy.levelNumber = level;
		enemy.playNumber = plays;
		enemy.hitTime = time - start_time;
		enemyList.Add (enemy);
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
		
    public void storeData()
    {
        levels = levelList.ToArray();
		enemiesHit = enemyList.ToArray ();
		keysHit = keysList.ToArray ();
        string filepath =  Application.dataPath + "/Data/" + id + ".json";
        // For now, doesn't actually store the data
        File.WriteAllText(filepath, JsonUtility.ToJson(this, true));
        //Debug.Log("Stored Data");
        //print(JsonUtility.ToJson(this, false));
    }

}

[Serializable]
public class LevelData
{
    public int levelNumber;
	public int playNumber;
	public float completeTime;
	public float currentTime;
	public int enemies_killed;
	public int enemies_spawned;
    public string result;
}

[Serializable]
public class EnemyData
{
	public string enemyType;
	public int levelNumber;
	public int playNumber;
	public float hitTime;
}

[Serializable]
public class KeyData
{
	public string keyPressed;
	public bool valid;
	public float hitTime;
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