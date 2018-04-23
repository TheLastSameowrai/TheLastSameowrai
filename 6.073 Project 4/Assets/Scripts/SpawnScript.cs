using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    private float spawnRate;
    private float start_time;
    public GameObject EnemyPrefab;
    public GameObject Player;

	// Use this for initialization
	void Start () {
        spawnRate = 2f;
        start_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - start_time > spawnRate)
        {
            spawnEnemey();
            start_time = Time.time;
        }
	}

    void spawnEnemey()
    {
        Vector3 spawnLocation = new Vector3(5.0f, 0, 0);
        GameObject enemy = (GameObject)Instantiate(EnemyPrefab, spawnLocation, new Quaternion());
        DemoEnemyController enemyController = enemy.GetComponent("DemoEnemyController") as DemoEnemyController;
        enemyController.target = Player;
    }
}
